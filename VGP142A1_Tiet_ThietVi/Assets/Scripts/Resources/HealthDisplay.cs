using System;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Resources
{
    public class HealthDisplay : MonoBehaviour
    {
        Health health;
        Text healthText;

        private void Awake()
        {
            health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
            healthText = GetComponent<Text>();
        }

        private void Update()
        {
            // healthText.text = String.Format("Health: {0:0}%", health.GetPercentage());
            healthText.text = String.Format("Health: {0:0}/{1:0}", health.HP, health.MaxHP);
        }
    }
}
