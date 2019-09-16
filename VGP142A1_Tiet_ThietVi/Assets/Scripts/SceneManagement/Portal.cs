using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using RPG.Core;
using RPG.Control;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        enum DestinationIdentifier
        {
            A, B, C, D
        }

        [SerializeField] int sceneToLoad = -1;
        [SerializeField] Transform spawnPoint;
        [SerializeField] DestinationIdentifier destination;
        [SerializeField] float fadeOutTime = 2.0f;
        [SerializeField] float fadeWaitTime = 2.0f;
        [SerializeField] float fadeInTime = 2.0f;

        GameObject player;

        // GameObject uiCanvas;

        // private void Start()
        // {
        //     uiCanvas = GameObject.FindWithTag("UICanvas");
        // }

        private void OnTriggerEnter(Collider c)
        {
            if (c.gameObject.tag == "Player")
            {
                Debug.Log("Entered the portal");
                StartCoroutine(SceneTransition());
            }
        }

        private IEnumerator SceneTransition()
        {
            if (sceneToLoad < 0)
            {
                Debug.LogError("Scene to load not set.");
                yield break;
            }

            DontDestroyOnLoad(gameObject);

            DisableControl();

            Fader fader = FindObjectOfType<Fader>();

            yield return fader.FadeOut(fadeOutTime);
            
            SavingWrapper savingWrapper = FindObjectOfType<SavingWrapper>();
            savingWrapper.Save();

            yield return SceneManager.LoadSceneAsync(sceneToLoad);
            
            savingWrapper.Load();

            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);

            savingWrapper.Save();

            yield return new WaitForSeconds(fadeWaitTime);
            yield return fader.FadeIn(fadeInTime);

            EnableControl();

            Destroy(gameObject);
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            player = GameObject.FindWithTag("Player");
            if (!player) return;
            player.GetComponent<NavMeshAgent>().enabled = false;
            player.transform.position = otherPortal.spawnPoint.position;
            player.transform.rotation = otherPortal.spawnPoint.rotation;
            player.GetComponent<NavMeshAgent>().enabled = true;
        }

        private Portal GetOtherPortal()
        {
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this) continue;

                if (portal.destination != destination) continue;

                return portal;
            }

            return null;
        }

        void DisableControl()
        {
            if (!player) return;

            player.GetComponent<ActionScheduler>().CancelCurrentAction();

            player.GetComponent<PlayerController>().enabled = false;

            // Cross-platform edit
            // uiCanvas.SetActive(false);
        }

        void EnableControl()
        {
            if (!player) return;

            player.GetComponent<PlayerController>().enabled = true;

            // Cross-platform edit
            // uiCanvas.SetActive(true);
        }
    }
}
