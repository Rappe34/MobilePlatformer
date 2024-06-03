using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public LevelDataSO lastPlayedLevel;
    public List<int> bossesDefeated;
    public Dictionary<int, float> fastestLevelTimes;
}
