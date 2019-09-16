using UnityEngine;

public class PeekABoo : MonoBehaviour
{
    EnemyController controller;

    public bool isSeen;
    public string myName;

    private void Awake()
    {
        controller = GetComponent<EnemyController>();
    }

    void Start()
    {
        myName = transform.name;
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

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Projectile")
        {
            Destroy(gameObject);
        }
    }
}
