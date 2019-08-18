using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Control;
using RPG.Core;

public class TriggerSlowSpeedOverTime : MonoBehaviour
{
    float currentMoveSpeedFraction;

    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            c.GetComponent<ActionScheduler>().CancelCurrentAction();
            currentMoveSpeedFraction = c.GetComponent<PlayerController>().moveSpeedFraction;
            c.GetComponent<PlayerController>().moveSpeedFraction = currentMoveSpeedFraction / 2;
        }
    }

    private void OnTriggerExit(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            c.GetComponent<PlayerController>().moveSpeedFraction = currentMoveSpeedFraction;
        }
    }
}
