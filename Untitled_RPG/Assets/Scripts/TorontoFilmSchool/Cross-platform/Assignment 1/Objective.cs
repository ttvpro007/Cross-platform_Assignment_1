using System.Collections;
using UnityEngine;
using RPG.Attributes;
using System;

public class Objective : MonoBehaviour
{
    [SerializeField] Health target;
    [SerializeField] Health player;

    public event Action onCompleted;
    public event Action onFailed;

    public bool completed = false;

    bool isWaiting = false;

    private void Update()
    {
        CheckQuit();
    }

    private void CheckQuit()
    {
        if (ObjectiveCompleted() && !isWaiting)
        {
            completed = true;
            onCompleted();
            StartCoroutine(Quitting(3.0f));
        }

        if (ObjectiveFailed() && !isWaiting)
        {
            onFailed();
            StartCoroutine(Quitting(3.0f));
        }
    }

    private bool ObjectiveCompleted()
    {
        if (!target) return false;

        if (target.IsDead())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool ObjectiveFailed()
    {
        if (player.IsDead())
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public IEnumerator Quitting(float time)
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
