using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionDropdownHandler : MonoBehaviour
{
    public Dropdown resolutionDropdown;

    private void Start()
    {
        PopulateDropdown();
        LoadCurrentResolution();
        resolutionDropdown.onValueChanged.AddListener(OnResolutionChanged);
    }

    private void PopulateDropdown()
    {
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        Resolution[] resolutions = Screen.resolutions;

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    private void LoadCurrentResolution()
    {
        int savedIndex = PlayerPrefs.GetInt("ResolutionIndex", 0);
        resolutionDropdown.value = savedIndex;
        resolutionDropdown.RefreshShownValue();
    }

    private void OnResolutionChanged(int index)
    {
        Resolution[] resolutions = Screen.resolutions;
        Resolution selectedResolution = resolutions[index];
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);

        PlayerPrefs.SetInt("ResolutionIndex", index);
        PlayerPrefs.Save();
    }
}