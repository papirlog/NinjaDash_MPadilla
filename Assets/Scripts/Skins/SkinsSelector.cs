using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class SkinsSelector : MonoBehaviour
{
    [SerializeField] private GameObject[] skins;

    private string prefKey;

    private int skinR;

    public void skinDefault()
    {
        for(int i = 0; i < skins.Length; i++)
        {
            skins[i].SetActive(false);
        }

        skins[0].SetActive(true);

        saveSkin(0);
    }

    public void skinWhite()
    {
        for (int i = 0; i < skins.Length; i++)
        {
            skins[i].SetActive(false);
        }

        skins[1].SetActive(true);

        saveSkin(1);
    }

    public void Initialize()
    {
        skinR = PlayerPrefs.GetInt(prefKey, 0);

        skins[skinR].SetActive(true);
    }

    private void saveSkin(int value)
    {
        PlayerPrefs.SetFloat(prefKey, value);
        PlayerPrefs.Save();
    }

}
