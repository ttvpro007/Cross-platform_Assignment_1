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

    Transform target;
    PlayerController controller;
    Objective objective;

    private void Start()
    {
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        if (FindObjectOfType<Objective>() != null)
            objective = GameObject.FindGameObjectWithTag("Objective").GetComponent<Objective>();
    }

    private void Update()
    {
        UpdateText();
    }

    void UpdateText()
    {
        playerHPText.text = "HP: " + controller.transform.GetComponent<Health>().healthPoints.ToString();

        if (controller.targetTransform == null) return;
        target = controller.targetTransform;
        enemyHPText.text = "HP: " + target.GetComponent<Health>().healthPoints.ToString();
        enemyNameText.text = target.name;
        
        if (objective && objective.completed)
            youWonText.enabled = true;
    }
}
