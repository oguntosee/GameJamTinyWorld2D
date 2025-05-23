using NUnit.Framework;
using UnityEngine;

public class DialogueLine
{
    public string Dialogue_Line;
    
    public SpeakerType SpeakerType;

    public DialogueLine(string dialogueLine, SpeakerType speakerType)
    {
        Dialogue_Line = dialogueLine;
        SpeakerType = speakerType;
    }
}
