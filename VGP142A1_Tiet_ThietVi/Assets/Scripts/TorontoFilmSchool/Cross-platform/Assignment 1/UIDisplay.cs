using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.Control;
using RPG.Core;

public class UIDisplay : MonoBehaviour
{
    [SerializeField] Text playerHPText;
    [SerializeField] Text enemyHPText;
    [SerializeField] Text enemyNameText;
    [SerializeField] Text youWonText;
    [SerializeField] Text godModeText;
    [SerializeField] Text speedModeText;

    Transform target;
    PlayerController controller;
    Objective objective;
    PowerUpController powerUpController;

    private void Start()
    {
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        if (FindObjectOfType<Objective>() != null)
            objective = GameObject.FindGameObjectWithTag("Objective").GetComponent<Objective>();

        powerUpController = GameObject.FindGameObjectWithTag("Player").GetComponent<PowerUpController>();
    }

    private void Update()
    {
        UpdateText();
    }

    void UpdateText()
    {
        playerHPText.text = "HP: " + controller.transform.GetComponent<Health>().HealthPoints.ToString();

        if (controller.targetTransform == null) return;
        target = controller.targetTransform;
        enemyHPText.text = "HP: " + target.GetComponent<Health>().HealthPoints.ToString();
        enemyNameText.text = target.name;
        
        if (objective && objective.completed)
            youWonText.enabled = true;

        if (powerUpController.godMode)
        {
            godModeText.text = "God Mode: ON";
        }
        else
        {
            godModeText.text = "God Mode: OFF";
        }

        if (powerUpController.speedMode)
        {
            speedModeText.text = "Speed Mode: ON";
        }
        else
        {
            speedModeText.text = "Speed Mode: OFF";
        }
    }
}
