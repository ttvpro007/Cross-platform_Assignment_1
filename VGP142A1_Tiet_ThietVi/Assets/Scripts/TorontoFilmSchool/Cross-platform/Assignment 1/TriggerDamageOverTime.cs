using UnityEngine;
using RPG.Resources;

public class TriggerDamageOverTime : MonoBehaviour
{
    [SerializeField] float damage = 2.5f;
    [SerializeField] float timeBetweenDamage = 1.5f;
    float timeSinceLastDamage;

    private void Update()
    {
        timeSinceLastDamage += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player" && timeSinceLastDamage < timeBetweenDamage)
        {
            timeSinceLastDamage = Mathf.Infinity;
        }
    }

    private void OnTriggerStay(Collider c)
    {
        if (c.gameObject.tag == "Player" && timeSinceLastDamage >= timeBetweenDamage)
        {
            c.GetComponent<Health>().TakeDamage(gameObject, damage);
            timeSinceLastDamage = 0.0f;
        }
    }
}
