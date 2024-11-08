using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private string playScene;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private InputActionReference escape;

    private void Awake()
    {
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
        if (pauseMenu.activeSelf)
        {
            StartCoroutine(ContinueGame());
        }
        else
        {
            PauseGame();
            pauseMenu.SetActive(true);
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
    }


    public void continueButton()
    {
        pauseMenu.SetActive(false);
        StartCoroutine(ContinueGame());
    }

    public void optionsInPauseButton()
    {
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void menuButton()
    {
        SceneManager.LoadScene(playScene);
    }
}
