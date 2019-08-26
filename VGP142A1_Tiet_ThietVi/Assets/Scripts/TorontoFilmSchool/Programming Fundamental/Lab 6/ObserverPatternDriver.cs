using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObserverPattern;
using RPG.Combat;
using RPG.Control;

public class ObserverPatternDriver : MonoBehaviour
{
    [SerializeField] GameObject[] enemies;
    Enemy enemy;
    GameObject target;
    Subject subject;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        subject = new Subject();

        foreach (GameObject e in enemies)
        {
            enemy = new Enemy(e);
            subject.AddObserver(enemy);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckTargetInRange() && CheckCanAttack())
        {
            subject.Notify();
        }
    }

    bool CheckTargetInRange()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].GetComponent<AIController>().TargetInAttackRange(target))
                return true;
        }

        return false;
    }

    bool CheckCanAttack()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].GetComponent<Fighter>().CanAttack(target))
                return true;
        }

        return false;
    }
}