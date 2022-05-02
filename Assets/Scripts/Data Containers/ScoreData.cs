using UnityEngine;

[System.Serializable]
public class ScoreData
{
    [Header("Components")]
    public ScoreType scoreType;
    public string bonusName;
    public int scoreAmount;

    public ScoreData(ScoreType _scoreType, int _scoreAmount, string _bonusName = null)
    {
        scoreType = _scoreType;
        bonusName = _bonusName;
        scoreAmount = _scoreAmount;
    }
}//END CLASS ScoreData

public enum ScoreType
{
    TIME,
    COLLECTABLES,
    ITEMS,
    DEDUCTIONS,
    BONUS
}
