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

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    void Start()
    {
        subject = new Subject();

        foreach (GameObject e in enemies)
        {
            enemy = new Enemy(e);
            subject.AddObserver(enemy);
        }
    }

    void Update()
    {
        if (CheckTargetInRange() && CheckCanAttack())
        {
            subject.Notify(true);
        }
        else
        {
            subject.Notify(false);
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