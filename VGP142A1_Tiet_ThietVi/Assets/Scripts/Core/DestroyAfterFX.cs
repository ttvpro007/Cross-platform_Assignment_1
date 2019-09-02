using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class DestroyAfterFX : MonoBehaviour
    {
        private void Start()
        {
            Destroy(gameObject, GetComponent<ParticleSystem>().main.duration + 0.5f);
        }
    }
}
