using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.UI;

public class AutoSavedSlider_Bright: AutoSavedSlider
{
    [SerializeField] Volume volume;

    private Exposure exposure;

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
        if(volume.profile.TryGet(out exposure))
        {
            exposure.active = true;
            exposure.compensation.value = PlayerPrefs.GetFloat(prefKey, defaultValue);
        }

        InternalValueChanged(slider.value);
    }

    private void OnSliderValueChanged(float value)
    {
        PlayerPrefs.SetFloat(prefKey, value);
        PlayerPrefs.Save();
        
        if (exposure != null)
        {
            exposure.compensation.value = value;
        }

        InternalValueChanged(value);
    }
}
