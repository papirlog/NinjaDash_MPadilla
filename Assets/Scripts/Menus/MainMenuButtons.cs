using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject levelSelectMenu;
    [SerializeField] private GameObject skinSelectMenu;
    [SerializeField] private GameObject creditsMenu;


    [SerializeField] GameObject[] virtualCameras;


    // Start is called before the first frame update
    void Awake()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
        levelSelectMenu.SetActive(false);
        skinSelectMenu.SetActive(false);

        virtualCameras[0].SetActive(true);
        virtualCameras[1].SetActive(false);
        virtualCameras[2].SetActive(false);
    }

    public void playLevel1()
    {
        SceneManager.LoadScene("Level1");
    }

    public void playLevel2()
    {
        SceneManager.LoadScene("Level2");
    }

    public void playLevel3()
    {
        SceneManager.LoadScene("Level3");
    }

    public void playButton()
    {
        mainMenu.SetActive(false);
        levelSelectMenu.SetActive(true);

        virtualCameras[0].SetActive(false);
        virtualCameras[2].SetActive(true);
    }

    public void optionsButton()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);

        virtualCameras[0].SetActive(false);
        virtualCameras[1].SetActive(true);
    }

    public void skinsButton()
    {
        mainMenu.SetActive(false);
        skinSelectMenu.SetActive(true);
    }

    public void returnButton()
    {
        optionsMenu.SetActive(false);
        levelSelectMenu.SetActive(false);
        skinSelectMenu.SetActive(false);
        creditsMenu.SetActive(false);
        mainMenu.SetActive(true);

        virtualCameras[1].SetActive(false);
        virtualCameras[2].SetActive(false);
        virtualCameras[0].SetActive(true);
    }

    public void creditsButton()
    {
        mainMenu.SetActive(false);
        creditsMenu.SetActive(true);
    }


    public void exitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
