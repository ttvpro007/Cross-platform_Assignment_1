using System;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Stats
{
    public class LevelDisplay : MonoBehaviour
    {
        BaseStats baseStat;
        Text levelText;

        private void Awake()
        {
            baseStat = GameObject.FindGameObjectWithTag("Player").GetComponent<BaseStats>();
            levelText = GetComponent<Text>();
        }

        private void Update()
        {
            levelText.text = String.Format("Level: {0}", baseStat.GetLevel());
        }
    }
}
