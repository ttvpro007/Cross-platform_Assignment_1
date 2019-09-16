using System;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpSpeedBoostDisplay : MonoBehaviour
{
    PowerUpController pc;
    Text speedBoostText;

    void Awake()
    {
        pc = FindObjectOfType<PowerUpController>();
        speedBoostText = GetComponent<Text>();
    }

    private void Update()
    {
        speedBoostText.text = String.Format("Speed Boost: {0}", BoolToOnOffText(pc.speedMode));
    }

    private string BoolToOnOffText(bool boolean)
    {
        return (boolean) ? "ON" : "OFF";
    }
}
