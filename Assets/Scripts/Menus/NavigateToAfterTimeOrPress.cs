using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class NavigateToAfterTimeOrPress : MonoBehaviour
{

    [SerializeField] private string nextScene;
    [SerializeField] private float waitTimer;
    [SerializeField] private InputActionReference skip;

    private bool skipActionActive = true;

    void Awake()
    {
        if(waitTimer > 1)
        {
            Invoke("NavigateToNextScreen", waitTimer);
        }
    }

    void OnEnable()
    {
        skip.action.Enable();
    }
    
    void OnDisable()
    {
        skip.action.Disable();
    }

    void Update()
    {
        if (skip.action.triggered && skipActionActive)
        {
            NavigateToNextScreen();
        }
    }

    void NavigateToNextScreen()
    {
        skipActionActive = false;
        SceneManager.LoadScene(nextScene);
    }

}
