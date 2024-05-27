using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/SliderSetting")]
public class SliderSettingSO : ScriptableObject
{
    public string key;
    public float defaultValue;

    public float GetValue() => PlayerPrefs.GetFloat(key, defaultValue);

    public void SetValue(float value) => PlayerPrefs.SetFloat(key, value);
}
