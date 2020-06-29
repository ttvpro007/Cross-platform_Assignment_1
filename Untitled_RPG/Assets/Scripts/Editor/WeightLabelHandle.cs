using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WeightHandle))]
public class WeightLabelHandle : Editor
{
    WeightHandle weightHandle;

    private void OnSceneGUI()
    {
        weightHandle = (WeightHandle)target;

        if (weightHandle == null || weightHandle.weights.Count == 0) return;

        Handles.color = Color.red;

        for (int i = 0; i < weightHandle.textPositions.Count; i++)
        {
            Handles.Label(weightHandle.textPositions[i], weightHandle.weights[i].ToString("0.00") + " meter(s)");
        }
    }
}
