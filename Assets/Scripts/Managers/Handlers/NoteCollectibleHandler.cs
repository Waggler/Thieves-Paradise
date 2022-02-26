using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteCollectibleHandler : MonoBehaviour
{
    [SerializeField] private NotesMenuManager manager;
    public NoteCollectibleData data;

    public void OpenNote()
    {
        manager.InitNote(data);
    }
}
