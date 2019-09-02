using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

public class Objective : MonoBehaviour
{
    [SerializeField] Health target;

    public bool completed = false;

    bool isWaiting = false;

    private void Start()
    {
        if (!target)
            target = GameObject.Find("EnemyBoss").GetComponent<Health>();
    }

    private void Update()
    {
        CheckQuit();
    }

    private void CheckQuit()
    {
        if (ObjectiveCompleted() && !isWaiting)
        {
            completed = true;
            StartCoroutine(Quitting(3.0f));
        }
    }

    private bool ObjectiveCompleted()
    {
        if (target.GetComponent<Health>().HealthPoints == 0)
            return true;
        else
            return false;
    }

    IEnumerator Quitting(float time)
    {
        isWaiting = true;
        yield return new WaitForSeconds(time);
        isWaiting = false;
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
}
