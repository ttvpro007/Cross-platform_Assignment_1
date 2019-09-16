using RPG.Resources;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        Fighter fighter;
        Health health;
        Text healthText;

        private void Awake()
        {
            fighter = GameObject.FindGameObjectWithTag("Player").GetComponent<Fighter>();
            healthText = GetComponent<Text>();
        }

        private void Update()
        {
            if (fighter.Target == null)
            {
                healthText.text = "N/A";
                return;
            }

            health = fighter.Target;
        
            //healthText.text = String.Format("Enemy: {0:0}%", health.GetPercentage());
            healthText.text = String.Format("Health: {0:0}/{1:0}", health.HP, health.MaxHP);
        }
    }
}
