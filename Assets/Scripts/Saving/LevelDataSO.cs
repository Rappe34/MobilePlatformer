using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/LevelData")]
public class LevelDataSO : ScriptableObject
{
    public int levelId;
    public string levelName;
    public string levelSceneName;
}
