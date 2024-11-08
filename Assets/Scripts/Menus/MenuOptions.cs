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
    public AutoSavedSlider brightnessSlider;
    public AutoSavedSlider contrastSlider;
    public Toggle fullScreenToggle;
    public ResolutionDropdownHandler resolutionDropdownHandler;

    [Header("Return Button")]
    public Button backButton;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject optionsMenu;

    [Header ("Camaras")]
    [SerializeField] GameObject[] virtualCameras;


    private void Awake()
    {
        masterVolumeSlider.Initialize();
        sfxVolumeSlider.Initialize();
        musicVolumeSlider.Initialize();

        brightnessSlider.Initialize();
        contrastSlider.Initialize();

        if (backButton != null)
        {
            backButton.onClick.AddListener(OnBackButtonPressed);
        }
    }

    private void OnBackButtonPressed()
    { 
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);

        virtualCameras[0].SetActive(true);
        virtualCameras[1].SetActive(false);

    }
}
