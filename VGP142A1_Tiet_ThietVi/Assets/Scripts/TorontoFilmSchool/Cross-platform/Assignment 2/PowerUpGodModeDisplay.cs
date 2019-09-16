using System;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpGodModeDisplay : MonoBehaviour
{
    PowerUpController pc;
    Text godModeText;

    void Awake()
    {
        pc = FindObjectOfType<PowerUpController>();
        godModeText = GetComponent<Text>();
    }
    
    private void Update()
    {
        godModeText.text = String.Format("God Mode: {0}", BoolToOnOffText(pc.godMode));
    }

    private string BoolToOnOffText(bool boolean)
    {
        return (boolean) ? "ON" : "OFF";
    }
}
