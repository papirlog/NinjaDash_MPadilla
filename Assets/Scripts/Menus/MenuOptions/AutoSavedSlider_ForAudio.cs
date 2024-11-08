using UnityEngine;
using UnityEngine.Audio;

public class AutoSavedSlider_ForAudio : AutoSavedSlider
{
    public AudioMixer audioMixer;
    public string parameterName;

    protected override void InternalValueChanged(float value)
    {
        float dB = LinearToDecibel(value);
        audioMixer.SetFloat(parameterName, dB);
    }

    private float LinearToDecibel(float linear)
    {
        return linear != 0 ? 20.0f * Mathf.Log10(linear) : -144.0f;
    }
}
