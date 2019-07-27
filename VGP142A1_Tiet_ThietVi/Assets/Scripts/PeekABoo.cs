using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeekABoo : MonoBehaviour
{
    EnemyController controller;

    public bool isSeen;
    public string myName;

    void Start()
    {
        myName = transform.name;
        controller = GetComponent<EnemyController>();
    }

    void Update()
    {
        CheckPlayerView();
    }

    void CheckPlayerView()
    {
        if (controller.TargetFound())
        {
            if(Vector3.Distance(transform.position, controller.target.position) <= controller.viewRadius)
                controller.FaceTarget();

            isSeen = controller.target.GetComponent<FieldOfView>().visibleTargets.Exists(x => x.name == myName);

            if (isSeen)
            {
                controller.StopMoving();
                controller.TurnInvisible();
            }
            else
            {
                controller.MoveTo(controller.target);
                controller.TurnVisible();
                StartCoroutine(controller.Attack(2.0f));
            }
        }
    }
}
