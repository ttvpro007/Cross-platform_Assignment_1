using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public struct DuplicateElementData
{
    public List<int> duplicateIndexes;
    public int element;
    public int count;

    public DuplicateElementData(List<int> duplicateIndexes, int element, int count)
    {
        this.duplicateIndexes = duplicateIndexes;
        this.element = element;
        this.count = count;
    }
}

[RequireComponent(typeof(UniqueIntegerList))]
public class DuplicateIntegerMonitor : MonoBehaviour
{
    [SerializeField] private List<DuplicateElementData> duplicateElementData = new List<DuplicateElementData>();
    public List<DuplicateElementData> DuplicateElementData { get { return duplicateElementData; } }

    public void SetDuplicateElementData(List<DuplicateElementData> duplicateElementData)
    {
        this.duplicateElementData = duplicateElementData;
    }

    public static List<DuplicateElementData> FindDuplicateElementData(List<int> integerList)
    {
        List<DuplicateElementData> duplicateElementData = new List<DuplicateElementData>();

        var query = integerList.GroupBy(x => x).Where(x => x.Count() > 1).Select(x => x.Key).ToList();

        foreach (var value in query)
        {
            List<int> duplicateIndexes = new List<int>();

            for (int j = 0; j < integerList.Count; j++)
            {
                if (value == integerList[j])
                {
                    duplicateIndexes.Add(j);
                }
            }

            duplicateElementData.Add(new DuplicateElementData(duplicateIndexes, value, duplicateIndexes.Count));
        }

        return duplicateElementData;
    }
}
