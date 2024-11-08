using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] private string playScene;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject optionsMenu;


    [SerializeField] GameObject[] virtualCameras;


    // Start is called before the first frame update
    void Awake()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    public void playButton()
    {
        SceneManager.LoadScene(playScene);
    }

    public void optionsButton()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);

        virtualCameras[0].SetActive(false);
        virtualCameras[1].SetActive(true);
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
