using UnityEngine;
using RPG.Saving;
using RPG.Stats;
using RPG.Core;
using GameDevTV.Utils;
using UnityEngine.Events;
using System;

namespace RPG.Attributes
{
    public class Health : MonoBehaviour, ISaveable
    {
        BaseStats baseStats;

        [Range(0.0f, 1.0f)]
        [SerializeField] public float defenceRating = 0.0f;
        [SerializeField] TakeDamageEvent takeDamage;
        [SerializeField] UnityEvent onDie;

        [System.Serializable]
        public class TakeDamageEvent : UnityEvent<float>
        {
        }

        LazyValue<float> hp;

        float hpLost = 0;
        float currentMaxHP = 0;
        bool isDead = false;

        //public float MaxHP { get { return GetComponent<BaseStats>().GetStat(Stat.Health); } }
        //public float HP { get { return hp.value; } /*set { hp.value = value; }*/ }
        //public bool IsDead { get { return hp.value == 0; } }
        
        private void Awake()
        {
            baseStats = GetComponent<BaseStats>();
            hp = new LazyValue<float>(GetInitialHealth);
        }

        private float GetInitialHealth()
        {
            if (baseStats == null) return 1;

            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        private void Start()
        {
            hp.ForceInit();
            if (baseStats == null) return;
            currentMaxHP = GetMaxHealthPoints();
        }

        private void OnEnable()
        {
            if (baseStats != null)
                baseStats.onLevelUp += SetLvUpHealth;
        }

        private void OnDisable()
        {
            if (baseStats != null)
                baseStats.onLevelUp -= SetLvUpHealth;
        }

        public bool IsDead()
        {
            return isDead == true;
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            float trueDamage = GetTrueDamage(damage);

            hp.value = Mathf.Max(hp.value - trueDamage, 0);
            hpLost += trueDamage;

            takeDamage.Invoke(trueDamage);

            if (hp.value == 0)
            {
                onDie.Invoke();
                Die();
                AwardExperience(instigator);
            }
        }

        private float GetTrueDamage(float damage)
        {
            return damage * (1 - defenceRating);
        }

        public void Heal(float healthToRestore)
        {
            hp.value = Mathf.Min(hp.value + healthToRestore, GetMaxHealthPoints());
            //hp.value = Mathf.Min(hp.value + healthToRestore, MaxHP);
        }

        public float GetHealthPoints()
        {
            return hp.value;
        }

        public float GetMaxHealthPoints()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        public float GetPercentage()
        {
            return 100 * GetFraction();
        }

        public float GetFraction()
        {
            return hp.value / currentMaxHP;
        }

        private void Die()
        {
            if (isDead) return;
            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<Animator>().SetBool("dead", isDead);
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AwardExperience(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();
            if (experience == null) return;

            experience.GainXP(GetComponent<BaseStats>().GetStat(Stat.XPReward));
        }

        private void SetLvUpHealth()
        {
            hp.value = GetMaxHealthPoints() * GetFraction();
            currentMaxHP = GetMaxHealthPoints();
        }

        public object CaptureState()
        {
            return hp.value;
        }

        public void RestoreState(object state)
        {
            hp.value = (float)state;

            if (hp.value == 0)
            {
                Die();
            }
        }
    }
}
