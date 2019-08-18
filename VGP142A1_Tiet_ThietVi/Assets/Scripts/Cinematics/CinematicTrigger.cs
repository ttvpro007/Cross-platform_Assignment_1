using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        bool isTriggered = false;

        private void OnTriggerEnter(Collider c)
        {
            if (!isTriggered && c.gameObject.tag == "Player")
            {
                GetComponent<PlayableDirector>().Play();
                isTriggered = true;
            }
        }
    }
}
