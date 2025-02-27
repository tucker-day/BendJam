using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue Storage")]
public class DialogueStorage : ScriptableObject
{
    public Sprite talkerSprite;
    public string speakingVoiceKey;
    public string speakerName;
    public List<string> dialogue;
    public float speed;
}
