using UnityEngine;

public class ProjectileProperty : MonoBehaviour
{
    float speed;
    float lifeSpan;

    // Start is called before the first frame update
    void Start()
    {
        speed = 5.0f;
        lifeSpan = 3.0f;
        Destroy(gameObject, lifeSpan);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * speed);
    }
}
