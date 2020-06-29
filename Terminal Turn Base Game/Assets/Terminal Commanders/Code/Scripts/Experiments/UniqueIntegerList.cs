using System.Collections.Generic;
using UnityEngine;

public class UniqueIntegerList : MonoBehaviour
{
    [SerializeField] private bool autoAdjust = false;
    [SerializeField] private List<int> values = new List<int>();

    public bool AutoAdjust { get { return autoAdjust; } }
    public List<int> Values { get { return values; } }

    public void SetValueList(List<int> values)
    {
        this.values = values;
    }

    public static List<int> GetUniqueValueList(List<int> integerList)
    {
        for (int i = 0; i < integerList.Count; i++)
        {
            for (int j = 0; j < integerList.Count; j++)
            {
                if (i != j && integerList[i] == integerList[j])
                {
                    integerList[j]++;
                    i = 0;
                    j = 0;
                }
            }
        }

        return integerList;
    }

    public static List<int> ResetUniqueValueList(List<int> integerList)
    {
        for (int i = 0; i < integerList.Count; i++)
        {
            integerList[i] = i;
        }

        return integerList;
    }

    public void SetMonitorData()
    {
        DuplicateIntegerMonitor monitor = GetComponent<DuplicateIntegerMonitor>();

        if (!monitor)
            monitor = gameObject.AddComponent<DuplicateIntegerMonitor>();
        
        monitor.SetDuplicateElementData(DuplicateIntegerMonitor.FindDuplicateElementData(values));
    }

    private void OnValidate()
    {
        if (autoAdjust) values = GetUniqueValueList(values);
        SetMonitorData();
    }
}