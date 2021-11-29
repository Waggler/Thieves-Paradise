using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    [Header("Character Settings")]
    public string characterName;
    public Sprite characterImage;

    [Header("Second Character Settings")]
    public bool isSecondCharacter;
    public bool isSecondCharacterTalking;

    [Header("Choice Settings")]
    public bool isChoice;
    public int choiceId;

    [TextArea(1, 9999)]
    public string[] sentences;
    public string[] choiceOptions;

}//END Dialogue
