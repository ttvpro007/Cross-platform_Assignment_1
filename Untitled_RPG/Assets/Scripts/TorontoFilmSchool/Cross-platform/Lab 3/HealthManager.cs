using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public float _health;
    public float health
    {
        get { return _health; }
        set { _health = value; }
    }

    void Start()
    {
        if (_health == 0)
            _health = 1.0f;
    }

    void Heal(float amount)
    {
        _health += amount;
    }

    void TakeDamage(float amount)
    {
        _health -= amount;
    }
}
