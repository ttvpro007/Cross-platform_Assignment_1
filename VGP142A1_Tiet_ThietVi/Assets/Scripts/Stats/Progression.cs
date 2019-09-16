using UnityEngine;
using RPG.Stats;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Progression", menuName = "RPG/Stats/New Stats Progression", order = 1)]
public class Progression : ScriptableObject
{
    [SerializeField] ProgressionCharacterClass[] characterClasses = null;

    Dictionary<CharacterClass, Dictionary<Stat, float[]>> lookupTable = null;

    public float GetStat(CharacterClass characterClass, Stat stat, int level)
    {
        BuildLookup();

        float[] levels = lookupTable[characterClass][stat];

        return levels.Length < level ? 0 : levels[level - 1];
    }

    public int GetMaxLevel(CharacterClass characterClass, Stat stat)
    {
        BuildLookup();

        float[] levels = lookupTable[characterClass][stat];

        return levels.Length;
    }

    private void BuildLookup()
    {
        if (lookupTable != null) return;

        lookupTable = new Dictionary<CharacterClass, Dictionary<Stat, float[]>>();
        Dictionary<Stat, float[]> statLookupTable = null;

        foreach (ProgressionCharacterClass progressionCharacterClass in characterClasses)
        {
            statLookupTable = new Dictionary<Stat, float[]>();

            foreach (ProgressionStat progressionStat in progressionCharacterClass.stats)
            {
                statLookupTable[progressionStat.stat] = progressionStat.levels;
            }

            lookupTable[progressionCharacterClass.characterClass] = statLookupTable;
        }
    }

    [System.Serializable]
    public class ProgressionCharacterClass
    {
        public CharacterClass characterClass;
        public ProgressionStat[] stats;
        
        //public float[] health;
    }

    [System.Serializable]
    public class ProgressionStat
    {
        public Stat stat;
        public float[] levels;
    }
}
