using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    [Header("Character Settings")]
    public string characterName;

    [TextArea(1, 9999)]
    public string[] sentences;

    [Header("Second Character Settings")]
    public bool isSecondCharacter;
    public bool isSecondCharacterTalking;

    [Header("Choice Settings")]
    public bool isChoice;
    public Choice[] choices;

    public Response[] responses;

    public int[] characterOneSpriteIndex;
    public int[] characterTwoSpriteIndex;
    public int[] backgroundSpriteIndex;

}//END Dialogue


[System.Serializable]
public class Choice
{
    public string choiceOption;
    public int choiceId;
}

[System.Serializable]
public class Response
{
    public string[] responseOption;
    public string responseSpeaker;
    public int responseId;

    public int[] characterOneSpriteIndex;
    public int[] characterTwoSpriteIndex;
    public int[] backgroundSpriteIndex;
}
