using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueManager
{
    public string characterName;

    [TextArea(1, 9999)]
    public string[] sentences;

}//END DialogueManager
