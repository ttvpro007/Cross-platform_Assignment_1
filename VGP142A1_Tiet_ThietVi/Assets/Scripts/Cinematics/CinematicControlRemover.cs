using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using RPG.Core;
using RPG.Control;

namespace RPG.Cinematics
{
    public class CinematicControlRemover : MonoBehaviour
    {
        GameObject player;
        GameObject uiCanvas;

        private void Start()
        {
            // Cross-platform edit
            uiCanvas = GameObject.FindGameObjectWithTag("UICanvas");

            player = GameObject.FindGameObjectWithTag("Player");
            GetComponent<PlayableDirector>().played += DisableControl;
            GetComponent<PlayableDirector>().stopped += EnableControl;
        }

        void DisableControl(PlayableDirector director)
        {
            player.GetComponent<ActionScheduler>().CancelCurrentAction();
            player.GetComponent<PlayerController>().enabled = false;

            // Cross-platform edit
            uiCanvas.SetActive(false);
        }

        void EnableControl(PlayableDirector director)
        {
            player.GetComponent<PlayerController>().enabled = true;

            // Cross-platform edit
            uiCanvas.SetActive(true);
        }
    }
}
