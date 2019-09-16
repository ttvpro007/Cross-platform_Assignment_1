using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    void Update()
    {
        if (GetComponent<HealthManager>().health <= 0)
        {
            PlayerDead();
        }
    }

    void PlayerDead()
    {
        gameObject.SetActive(false);
    }
}
