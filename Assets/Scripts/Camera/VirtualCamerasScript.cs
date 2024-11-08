using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCamerasScript : MonoBehaviour
{
    public GameObject[] virtualCameras;

    private void Awake()
    {
        virtualCameras[0].SetActive(true);
        virtualCameras[1].SetActive(false);
    }

    public void changeVirtualCamera()
    {
        if (virtualCameras[0].activeInHierarchy)
        {
            virtualCameras[0].SetActive(false);
            virtualCameras[1].SetActive(true);
        }
        else
        {
            virtualCameras[0].SetActive(true);
            virtualCameras[1].SetActive(false);
        }
    }
}
