using UnityEngine;
using RPG.Movement;

public class ParticleSystemController : MonoBehaviour
{
    public ParticleSystem[] particleSystems;
    PowerUpController powerUpController;
    public bool isMoving;

    private void Awake()
    {
        particleSystems = transform.GetComponentsInChildren<ParticleSystem>();
        powerUpController = GetComponentInParent<PowerUpController>();
    }

    private void Update()
    {
        isMoving = GetComponentInParent<Mover>().isMoving;

        if (gameObject.name == "GlowingOrb")
        {
            if (powerUpController.godMode)
            {
                TurnOnParticleSystems();
            }
            else
            {
                TurnOffParticleSystems();
            }
        }

        if (gameObject.name == "RunningTrail")
        {
            if (powerUpController.speedMode && isMoving)
            {
                TurnOnParticleSystems();
            }
            else
            {
                TurnOffParticleSystems();
            }
        }

        if (gameObject.name == "RunningDust")
        {
            if (isMoving)
            {
                TurnOnParticleSystems();
            }
            else
            {
                TurnOffParticleSystems();
            }
        }
    }

    private void TurnOnParticleSystems()
    {
        foreach (ParticleSystem particleSystem in particleSystems)
        {
            if (particleSystem.isStopped)
                particleSystem.Play();
        }
    }

    private void TurnOffParticleSystems()
    {
        foreach (ParticleSystem particleSystem in particleSystems)
        {
            if (particleSystem.isPlaying)
                particleSystem.Stop();
        }
    }
}
