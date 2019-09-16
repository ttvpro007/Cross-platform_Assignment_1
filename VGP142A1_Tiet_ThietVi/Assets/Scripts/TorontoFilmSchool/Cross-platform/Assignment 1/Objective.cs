﻿using System.Collections;
using UnityEngine;
using RPG.Resources;

public class Objective : MonoBehaviour
{
    [SerializeField] Health target;

    public bool completed = false;

    bool isWaiting = false;

    private void Awake()
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
        if (target.GetComponent<Health>().HP == 0)
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
