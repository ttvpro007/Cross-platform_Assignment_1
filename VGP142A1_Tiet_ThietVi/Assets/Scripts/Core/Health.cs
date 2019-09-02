using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;

namespace RPG.Core
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] public float startHealth = 100f;

        float healthPoints;

        public float HealthPoints { get { return healthPoints; } set { healthPoints = value; } }

        [Range(0.0f, 1.0f)]
        [SerializeField] public float defenceRating = 0.0f;

        bool isDead = false;

        public bool IsDead { get { return healthPoints == 0; } }

        private void Start()
        {
            healthPoints = startHealth;
        }

        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - (damage * (1 - defenceRating)), 0);

            Debug.Log(healthPoints);

            if (healthPoints == 0)
            {
                Die();
            }
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
            healthPoints = startHealth;
        }

        public object CaptureState()
        {
            return healthPoints;
        }

        public void RestoreState(object state)
        {
            healthPoints = (float)state;

            if (healthPoints == 0)
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
