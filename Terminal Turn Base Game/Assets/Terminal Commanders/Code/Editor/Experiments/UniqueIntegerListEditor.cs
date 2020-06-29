using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UniqueIntegerList))]
public class UniqueIntegerListEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        UniqueIntegerList test = target as UniqueIntegerList;

        if (!test.AutoAdjust)
        {
            if (GUILayout.Button("Adjust Duplicate Values"))
            {
                test.SetValueList(UniqueIntegerList.GetUniqueValueList(test.Values));
                test.SetMonitorData();
            }
        }

        if (GUILayout.Button("Reset Values"))
        {
            test.SetValueList(UniqueIntegerList.ResetUniqueValueList(test.Values));
            test.SetMonitorData();
        }
    }
}