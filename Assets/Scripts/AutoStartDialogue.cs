using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoStartDialogue : MonoBehaviour
{
    public DialogueSystem dia;

    // Start is called before the first frame update
    void Start()
    {
        dia.StartDialogue(dia.dialogue);
    }
}
