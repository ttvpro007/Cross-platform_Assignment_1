using UnityEngine;

namespace RPG.Core
{
    public class DestroyAfterFX : MonoBehaviour
    {
        [SerializeField] GameObject targetToDestroy = null;

        private void Start()
        {
            if (targetToDestroy)
            {
                Destroy(targetToDestroy, targetToDestroy.GetComponentInChildren<ParticleSystem>().main.duration + 0.5f);
            }
            else
            {
                Destroy(gameObject, GetComponent<ParticleSystem>().main.duration + 0.5f);
            }
        }
    }
}
