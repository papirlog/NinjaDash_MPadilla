using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private string playScene;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private InputActionReference escape;

    private bool paused;
    private bool pausedOptions;

    private void Awake()
    {
        paused = false;
        pausedOptions = false;

        pauseMenu.SetActive(false);
        optionsMenu.SetActive(false);
    }

    private void OnEnable()
    {
        escape.action.Enable();

        escape.action.performed += openMenu;
    }
    
    private void OnDisable()
    {
        escape.action.Disable();

        escape.action.performed -= openMenu;
    }

    private void openMenu(InputAction.CallbackContext context)
    {
        if (pauseMenu.activeSelf && paused == true)
        {
            StartCoroutine(ContinueGame());
        }
        else if(paused == false)
        {
            PauseGame();
            pauseMenu.SetActive(true);
            paused = true;
        }

        if(optionsMenu.activeSelf && pausedOptions == true)
        {
            returnFromOptionsMenu();
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
    }

    private IEnumerator ContinueGame()
    {
        pauseMenu.SetActive(false);
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 1f;
        paused = false;
    }

    private void returnFromOptionsMenu()
    {
        optionsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }


    public void continueButton()
    {
        StartCoroutine(ContinueGame());
    }

    public void optionsInPauseButton()
    {
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(true);
        pausedOptions = true;
    }

    public void menuButton()
    {
        SceneManager.LoadScene(playScene);
    }

    public void returnButton()
    {
        optionsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }
}
