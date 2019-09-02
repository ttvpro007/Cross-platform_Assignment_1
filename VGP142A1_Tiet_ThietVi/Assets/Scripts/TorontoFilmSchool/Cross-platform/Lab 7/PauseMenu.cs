using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.Core;
using RPG.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    static bool gameIsPaused = false;

    public static bool GameIsPaused { get { return gameIsPaused; } }

    [SerializeField] GameObject pauseMenuUI;

    Health player;

    SavingWrapper savingWrapper;

    Button saveButton, loadButton, continueButton;

    ContinueCounter continueCounter;

    Text continueText;

    bool hasLoaded = false;

    bool isWaiting = false;

    private void Start()
    {
        savingWrapper = FindObjectOfType<SavingWrapper>();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();

        continueCounter = FindObjectOfType<ContinueCounter>();

        saveButton = pauseMenuUI.transform.Find("SaveButton").GetComponent<Button>();
        loadButton = pauseMenuUI.transform.Find("LoadButton").GetComponent<Button>();
        continueButton = pauseMenuUI.transform.Find("ContinueButton").GetComponent<Button>();
    }

    void Update()
    {
        PauseMenuLogic();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    private void PauseMenuLogic()
    {
        if (!player) return;

        if (player.IsDead)
        {
            if (!isWaiting)
                StartCoroutine(PausingMenu());

            if (!hasLoaded)
            {
                saveButton.interactable = false;
                loadButton.interactable = false;

                if (continueCounter.timesLoadLeft > 0)
                {
                    continueButton.interactable = true;
                }
                else
                {
                    continueButton.interactable = false;
                }
            }
            else
            {
                saveButton.interactable = true;
                loadButton.GetComponent<Button>().interactable = true;
                continueButton.interactable = false;
            }

            continueText = continueButton.transform.Find("Text").GetComponent<Text>();

            if (continueCounter.timesLoadLeft > 0)
            {
                continueText.text = "CONTINUE(" + continueCounter.timesLoadLeft + ")";
            }
            else
            {
                continueText.text = "RESTART";
            }
        }
    }

    public void Pause()
    {
        gameIsPaused = true;
        Time.timeScale = 0.0f;
        pauseMenuUI.SetActive(gameIsPaused);
    }

    public void Resume()
    {
        hasLoaded = false;
        gameIsPaused = false;
        Time.timeScale = 1.0f;
        pauseMenuUI.SetActive(gameIsPaused);
    }

    public void Continue()
    {
        Load();
        Resume();
    }

    public void Save()
    {
        savingWrapper.Save();
    }

    public void Load()
    {
        if (continueCounter.timesLoadLeft <= 0)
        {
            continueCounter.timesLoadLeft = 3;
            savingWrapper.Delete();
            savingWrapper.Load(1);
            savingWrapper.Save();
        }
        else
        {
            if (!savingWrapper.SaveFileExists())
                savingWrapper.Save();

            savingWrapper.Load();
        }

        hasLoaded = true;
        isWaiting = false;

        Resume();
    }

    public void Quit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }

    private IEnumerator PausingMenu()
    {
        isWaiting = true;
        yield return new WaitForSeconds(2.0f);
        player.ResetHealth();
        continueCounter.timesLoadLeft = Mathf.Max(--continueCounter.timesLoadLeft, 0);
        Pause();
    }
}
