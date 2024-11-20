using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryButtons : MonoBehaviour
{
    [SerializeField] private string mainMenu;
    [SerializeField] private string retryScene;

    public void returnButtonInVictory()
    {
        SceneManager.LoadScene(mainMenu);
    }

    public void retryButtonInVictory()
    {
        SceneManager.LoadScene(retryScene);
    }
}
