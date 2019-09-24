using RPG.Attributes;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ResultTextDisplay : MonoBehaviour
{
    Objective objective;
    Health playerHealth;
    Text resultText;

    void Awake()
    {
        objective = FindObjectOfType<Objective>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        resultText = GetComponent<Text>();
    }

    private void OnEnable()
    {
        if (objective)
        {
            objective.onCompleted += UpdateText;
            objective.onFailed += UpdateText;
        }
    }

    private void OnDisable()
    {
        if (objective)
        {
            objective.onCompleted -= UpdateText;
            objective.onFailed -= UpdateText;
        }
    }

    public void UpdateText()
    {
        if (objective.completed)
            resultText.text = "YOU WON";
        else
            resultText.text = "YOU LOSE";
    }
}
