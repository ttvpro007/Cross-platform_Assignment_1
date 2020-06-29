using UnityEngine;

public class CollectibleRotation : MonoBehaviour
{
    [SerializeField] float xAngularVelocity;
    [SerializeField] float yAngularVelocity;
    [SerializeField] float zAngularVelocity;
    [SerializeField] bool rotate;
    [SerializeField] bool reset;

    private void Start()
    {
        rotate = true;
        reset = false;
        DefaultRotationalValue();
    }

    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        if (rotate)
        {
            transform.Rotate(Vector3.right, xAngularVelocity);
            transform.Rotate(Vector3.up, yAngularVelocity);
            transform.Rotate(Vector3.forward, zAngularVelocity);
        }

        if (reset)
        {
            ResetAngle();
            rotate = !reset;
        }

    }

    private void ResetAngle()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(Vector3.zero), 0.2f);
        DefaultRotationalValue();
    }

    private void DefaultRotationalValue()
    {
        xAngularVelocity = 0;
        yAngularVelocity = 5;
        zAngularVelocity = 0;
    }
}