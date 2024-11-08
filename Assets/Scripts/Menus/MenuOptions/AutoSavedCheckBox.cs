using UnityEngine;
using UnityEngine.UI;

public class AutoSavedCheckBox : MonoBehaviour
{
    public string prefKey;
    public bool defaultValue = false;
    protected Toggle toggle;

    protected virtual void InternalValueChanged(bool value) { }

    public void Initialize()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(OnToggleValueChanged);
        toggle.isOn = PlayerPrefs.GetInt(prefKey, defaultValue ? 1 : 0) == 1;
        InternalValueChanged(toggle.isOn);
    }

    private void OnToggleValueChanged(bool value)
    {
        PlayerPrefs.SetInt(prefKey, value ? 1 : 0);
        PlayerPrefs.Save();
        InternalValueChanged(value);
    }
}