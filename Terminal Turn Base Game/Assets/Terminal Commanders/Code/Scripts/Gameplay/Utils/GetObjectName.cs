using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GetObjectName : MonoBehaviour
{
    [SerializeField] private TMP_Text textLabel;

    private void Awake()
    {
        if (!textLabel)
            textLabel = GetComponent<TMP_Text>();

        textLabel.text = transform.parent.name;
    }
}
