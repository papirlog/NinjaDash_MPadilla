using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class SkinsSelector : MonoBehaviour
{
    [SerializeField] private GameObject[] skins;

    [SerializeField] public string prefKeySkin;

    private int skinR;

    public void skinGrayFox()
    {
        setSkin(0);
    }

    public void skinBlackAlien()
    {
        setSkin(1);
    }

    private void setSkin(int index)
    {
        for (int i = 0; i < skins.Length; i++)
        {
            skins[i].SetActive(false);
        }

        if (index >= 0 && index < skins.Length)
        {
            skins[index].SetActive(true);
            saveSkin(index);
        }
    }

    public void Initialize()
    {
        skinR = PlayerPrefs.GetInt(prefKeySkin, 0);

        skins[skinR].SetActive(true);
    }

    private void saveSkin(int value)
    {
        PlayerPrefs.SetInt(prefKeySkin, value);
        PlayerPrefs.Save();
    }
}
