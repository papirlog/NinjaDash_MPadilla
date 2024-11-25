using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class AutoSavedCheckBox : MonoBehaviour
{
    public string prefKey;
    public bool defaultValue = false;
    private bool toggleChecked = true;
    [SerializeField] Toggle toggle;

    protected virtual void InternalValueChanged(bool value) { }

    public void Initialize()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(OnToggleValueChanged);
        toggle.isOn = PlayerPrefs.GetInt(prefKey, defaultValue ? 1 : 0) == 1;
        InternalValueChanged(toggle.isOn);
    }

    private void Start()
    {
        InternalValueChanged(toggleChecked);
    }

    private void OnToggleValueChanged(bool value)
    {
        if (toggleChecked == true)
        {
            toggleChecked = false;
        }
        else
        {
            toggleChecked = true;
        }

        PlayerPrefs.SetInt(prefKey, value ? 1 : 0);
        PlayerPrefs.Save();
        InternalValueChanged(value);
    }
}