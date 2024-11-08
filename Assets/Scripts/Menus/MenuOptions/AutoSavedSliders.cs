using UnityEngine;
using UnityEngine.UI;

public class AutoSavedSlider : MonoBehaviour
{
    public string prefKey;
    public float defaultValue = 1.0f;
    protected Slider slider;

    protected virtual void InternalValueChanged(float value) { }

    public void Initialize()
    {
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(OnSliderValueChanged);
        slider.value = PlayerPrefs.GetFloat(prefKey, defaultValue);
        InternalValueChanged(slider.value);
    }

    private void OnSliderValueChanged(float value)
    {
        PlayerPrefs.SetFloat(prefKey, value);
        PlayerPrefs.Save();
        InternalValueChanged(value);
    }
}
