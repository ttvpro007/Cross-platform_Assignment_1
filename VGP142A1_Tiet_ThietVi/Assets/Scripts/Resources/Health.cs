using UnityEngine;
using RPG.Saving;
using RPG.Stats;
using RPG.Core;
using GameDevTV.Utils;
using System;

namespace RPG.Resources
{
    public class Health : MonoBehaviour, ISaveable
    {
        BaseStats baseStats;
        Experience xp = null;

        [Range(0.0f, 1.0f)]
        [SerializeField] public float defenceRating = 0.0f;

        LazyValue<float> maxHP;
        float hp = 0;
        float hpLost = 0;
        bool isDead = false;

        public float MaxHP { get { return maxHP.value; } }
        public float HP { get { return hp; } set { hp = value; } }
        public float CurrentHPFraction { get { return (maxHP.value - hpLost) / maxHP.value; } }
        public bool IsDead { get { return hp == 0; } }

        private void Awake()
        {
            maxHP = new LazyValue<float>(GetInitialValue);
        }

        private float GetInitialValue()
        {
            baseStats = GetComponent<BaseStats>();
            return (baseStats != null) ? baseStats.GetStat(Stat.Health) : 1;
        }

        private void Start()
        {
            maxHP.ForceInit();
            hp = maxHP.value;

            if (baseStats) baseStats.onLevelUp += RegenHealth;
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            float trueDamage = GetTrueDamage(damage);

            print(gameObject + " took damage: " + trueDamage);

            hp = Mathf.Max(hp - trueDamage, 0);

            hpLost += trueDamage;

            if (hp == 0)
            {
                RewardXP(instigator);
                Die();
            }
        }

        private float GetTrueDamage(float damage)
        {
            return damage * (1 - defenceRating);
        }

        public float GetPercentage()
        {
            return hp * 100 / maxHP.value;
        }

        private void RewardXP(GameObject instigator)
        {
            xp = instigator.GetComponent<Experience>();

            if (!xp) return;

            xp.GainXP(baseStats.GetStat(Stat.XPReward));
        }

        private void Die()
        {
            if (isDead) return;

            GetComponent<Animator>().SetTrigger("die");
            GetComponent<Animator>().SetBool("dead", true);
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        public void ResetAnimationParameters()
        {
            GetComponent<Animator>().ResetTrigger("die");
            GetComponent<Animator>().SetBool("dead", false);
            GetComponent<Animator>().ResetTrigger("cancelAttack");
        }

        public void ResetHealth()
        {
            maxHP.ForceInit();
            hp = maxHP.value;
        }

        private void RegenHealth()
        {
            maxHP.value = baseStats.GetStat(Stat.Health);
            hp = maxHP.value * CurrentHPFraction;
        }

        public object CaptureState()
        {
            return hp;
        }

        public void RestoreState(object state)
        {
            hp = (float)state;

            if (hp == 0)
            {
                Die();
            }
            else
            {
                ResetAnimationParameters();
            }
        }
    }
}
