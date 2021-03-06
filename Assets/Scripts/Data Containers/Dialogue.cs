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

    [Header("Sprites")]
    public Sprite characterOneSprite;
    public Sprite characterTwoSprite;

    [Header("Choice Settings")]
    public bool isChoice;
    public Choice[] choices;

    public Response[] responses;

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
    public string[] responseSpeaker;
    public int choiceReferenceId;

    [HideInInspector]public int responseID;
}
