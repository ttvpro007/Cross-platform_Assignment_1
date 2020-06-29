using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed = 0;
    [SerializeField] private float tolerant = 0;
    [SerializeField] private Vector3 destination;

    public float Speed => speed;
    public float Tolerant => tolerant;
    public Vector3 Destination => destination;

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public void SetTolerant(float tolerant)
    {
        this.tolerant = tolerant;
    }

    public IEnumerator MoveTo(Vector3 position)
    {
        destination = position;
        Vector3 direction = (position - transform.position).normalized;
        while ((position - transform.position).sqrMagnitude > tolerant * tolerant)
        {
            transform.Translate(direction * speed * Time.deltaTime);
            yield return null;
        }
        transform.position = position;
    }

    public IEnumerator Move(Vector3 offset)
    {
        destination = transform.position + offset;
        yield return MoveTo(destination);
    }

    public IEnumerator MoveUp(float distance)
    {
        destination = transform.position + Vector3.up * distance;
        yield return MoveTo(destination);
    }

    public IEnumerator MoveDown(float distance)
    {
        destination = transform.position + -Vector3.up * distance;
        yield return MoveTo(destination);
    }

    public IEnumerator MoveLeft(float distance)
    {
        destination = transform.position + -Vector3.right * distance;
        yield return MoveTo(destination);
    }

    public IEnumerator MoveRight(float distance)
    {
        destination = transform.position + Vector3.right * distance;
        yield return MoveTo(destination);
    }

    public IEnumerator MoveForward(float distance)
    {
        destination = transform.position + Vector3.forward * distance;
        yield return MoveTo(destination);
    }

    public IEnumerator MoveBackward(float distance)
    {
        destination = transform.position + -Vector3.forward * distance;
        yield return MoveTo(destination);
    }
}
