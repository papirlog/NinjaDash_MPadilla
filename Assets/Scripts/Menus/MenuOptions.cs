using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using Cinemachine;

public class MenuOptions : MonoBehaviour
{
    [Header("Audio Settings")]
    public AutoSavedSlider_ForAudio masterVolumeSlider;
    public AutoSavedSlider_ForAudio sfxVolumeSlider;
    public AutoSavedSlider_ForAudio musicVolumeSlider;

    [Header("Graphics Settings")]
    public Toggle fullScreenToggle;
    public ResolutionDropdownHandler resolutionDropdownHandler;

    private void Awake()
    {
        masterVolumeSlider.Initialize();
        sfxVolumeSlider.Initialize();
        musicVolumeSlider.Initialize();
    }
}
