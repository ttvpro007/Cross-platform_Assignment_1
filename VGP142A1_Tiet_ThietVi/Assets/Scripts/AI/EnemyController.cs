using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float viewRadius;
    public float attackDistance;
    bool isWaiting = false;

    public Transform target;
    public NavMeshAgent agent;
    public FieldOfView fow;

    void Start()
    {
        fow = GetComponent<FieldOfView>();
        viewRadius = fow.viewRadius;
        agent = GetComponent<NavMeshAgent>();
    }

    public bool TargetFound()
    {
        if (fow.visibleTargets.Count > 0)
        {
            target = fow.visibleTargets[0];
            return true;
        }
        else
            return false;
    }

    public void MoveTo(Transform _target)
    {
        agent.isStopped = false;
        agent.destination = _target.position;
    }

    public void StopMoving()
    {
        agent.isStopped = true;
    }

    public IEnumerator Attack(float interval)
    {
        if (Vector3.Distance(transform.position, target.position) <= attackDistance && !isWaiting)
        {
            DealDamage(target, 1.0f);
            isWaiting = true;
            yield return new WaitForSeconds(interval);
            isWaiting = false;
        }
    }

    public void FaceTarget()
    {
        float smooth = 5.0f;
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRoration = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRoration, Time.deltaTime * smooth);
    }

    void DealDamage(Transform _target, float amount)
    {
        _target.GetComponent<HealthManager>().health -= amount;
    }

    public void TurnInvisible()
    {
        GetComponent<Renderer>().enabled = false;
    }

    public void TurnVisible()
    {
        GetComponent<Renderer>().enabled = true;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, viewRadius);
    }
}
