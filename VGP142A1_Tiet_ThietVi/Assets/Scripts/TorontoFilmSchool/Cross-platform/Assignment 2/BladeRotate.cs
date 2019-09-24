using UnityEngine;

public class BladeRotate : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(Vector3.forward, 1);
    }
}
