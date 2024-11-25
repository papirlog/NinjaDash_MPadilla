using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.UI;

public class AutoSavedSlider_Contrast : AutoSavedSlider
{
    [SerializeField] Volume volume;

    private ColorAdjustments coloradjustments;

    //protected virtual void InternalValueChanged(float value) { }

    //public void Initialize()
    //{
    //    slider = GetComponent<Slider>();
    //    slider.onValueChanged.AddListener(OnSliderValueChanged);
    //    slider.value = PlayerPrefs.GetFloat(prefKey, defaultValue);
    //    InternalValueChanged(slider.value);
    //}

    private void Start()
    {
        if(volume.profile.TryGet(out coloradjustments))
        {
            coloradjustments.active = true;
            coloradjustments.contrast.value = PlayerPrefs.GetFloat(prefKey, defaultValue);
        }

        InternalValueChanged(slider.value);
    }

    private void OnSliderValueChanged(float value)
    {
        PlayerPrefs.SetFloat(prefKey, value);
        PlayerPrefs.Save();
        
        if (coloradjustments != null)
        {
            coloradjustments.contrast.value = value;
        }

        InternalValueChanged(value);
    }
}
