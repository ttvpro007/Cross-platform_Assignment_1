using System;
using UnityEngine;
using GameDevTV.Utils;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1, 100)]
        [SerializeField] int level = 1;
        [SerializeField] float damageMultiplier = 1;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression = null;
        [SerializeField] GameObject levelUpFX = null;
        [SerializeField] bool useModifier = false;

        public event Action onLevelUp;

        LazyValue<int> currentLevel;

        Experience xp;

        private void Awake()
        {
            xp = GetComponent<Experience>();
            currentLevel = new LazyValue<int>(CalculateLevel);
        }

        private void Start()
        {
            currentLevel.ForceInit();
        }

        private void OnEnable()
        {
            if (xp != null)
            {
                xp.onXPGained += UpdateLevel;
            }
        }

        private void OnDisable()
        {
            if (xp != null)
            {
                xp.onXPGained -= UpdateLevel;
            }
        }

        private void UpdateLevel()
        {
            int newLevel = CalculateLevel();
            if (newLevel > currentLevel.value)
            {
                currentLevel.value = newLevel;
                LevelUpEffect();
                onLevelUp();
            }
        }

        private void LevelUpEffect()
        {
            Instantiate(levelUpFX, transform);
        }

        public float GetStat(Stat stat)
        {
            if (stat == Stat.Damage)
                return (GetBaseStat(stat) + GetModifier(stat)) * (1 + GetPercentageModifier(stat) / 100) * damageMultiplier;

            return (GetBaseStat(stat) + GetModifier(stat)) * (1 + GetPercentageModifier(stat) / 100);
        }

        private float GetBaseStat(Stat stat)
        {
            return progression.GetStat(characterClass, stat, GetLevel());
        }

        private float GetModifier(Stat stat)
        {
            if (!useModifier) return 0;

            float total = 0;

            foreach (IModifierProvider provider in GetComponents<IModifierProvider>())
            {
                foreach (float modifier in provider.GetAdditiveModifiers(stat))
                {
                    total += modifier;
                }
            }

            return total;
        }

        private float GetPercentageModifier(Stat stat)
        {
            if (!useModifier) return 0;

            float total = 0;

            foreach (IModifierProvider provider in GetComponents<IModifierProvider>())
            {
                foreach (float modifier in provider.GetPercentageModifiers(stat))
                {
                    total += modifier;
                }
            }

            return total;
        }

        public int GetLevel()
        {
            return (currentLevel.value == 0) ? CalculateLevel() : currentLevel.value;
        }

        private int CalculateLevel()
        {
            if (gameObject.tag != "Player") return level;
            if (!xp) return 0;

            float currentXP = xp.Value;
            float xpToLevelUp = 0;
            int maxLevel = progression.GetMaxLevel(CharacterClass.Player, Stat.XPToLevelUp);

            for (int level = 1; level <= maxLevel; level++)
            {
                xpToLevelUp = progression.GetStat(CharacterClass.Player, Stat.XPToLevelUp, level);

                if (currentXP < xpToLevelUp)
                    return level;
            }

            return maxLevel + 1;
        }
    }
}