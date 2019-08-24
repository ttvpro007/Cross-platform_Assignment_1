using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StartGame : MonoBehaviour
{
    [SerializeField] int sceneToLoad = -1;

    public void LoadScene()
    {
        if (sceneToLoad < 0)
        {
            Debug.LogError("Scene to load not set.");
            return;
        }

        SceneManager.LoadScene(sceneToLoad);
    }
}
