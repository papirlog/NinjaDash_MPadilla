using UnityEngine;
using UnityEngine.UI;

public class AutoSavedCheckBox : MonoBehaviour
{
    public string prefKey = "FullscreenToggle";
    public bool defaultValue = false;
    [SerializeField] Toggle toggle;

    protected virtual void InternalValueChanged(bool value) { }

    public void Initialize()
    {
        bool isFullscreen = PlayerPrefs.GetInt(prefKey, defaultValue ? 1 : 0) == 1;

        toggle.isOn = isFullscreen;

        Screen.fullScreen = isFullscreen;

        InternalValueChanged(toggle.isOn);

        toggle.onValueChanged.AddListener(OnToggleValueChanged);
    }

    private void Start()
    {
        Initialize();
    }

    private void OnToggleValueChanged(bool value)
    {
        Screen.fullScreen = value;

        PlayerPrefs.SetInt(prefKey, value ? 1 : 0);
        PlayerPrefs.Save();

        InternalValueChanged(value);
    }
}
