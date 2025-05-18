using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(menuName = "ScriptableObject/SliderSetting")]
public class SliderSettingSO : ScriptableObject
{
    public AudioMixer mixer;
    public string key;
    public float defaultValue;

    public float GetValue() => PlayerPrefs.GetFloat(key, defaultValue);

    public void SetValue(float value)
    {
        mixer.SetFloat(key, Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat(key, value);
    }
}
