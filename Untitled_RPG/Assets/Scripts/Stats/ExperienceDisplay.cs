using System;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Stats
{
    public class ExperienceDisplay : MonoBehaviour
    {
        Experience xp;
        Text xpText;

        private void Awake()
        {
            xp = GameObject.FindGameObjectWithTag("Player").GetComponent<Experience>();
            xpText = GetComponent<Text>();
        }

        private void OnEnable()
        {
            if (xp)
            {
                xp.onXPGained += UpdateText;
            }
        }

        private void OnDisable()
        {
            if (xp)
            {
                xp.onXPGained -= UpdateText;
            }
        }

        private void UpdateText()
        {
            xpText.text = String.Format("XP: {0}", xp.Value);
        }
    }
}