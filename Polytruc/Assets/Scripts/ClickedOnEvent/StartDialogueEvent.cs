using UnityEngine;
using Yarn.Unity; 

public class StartDialogueEvent : ClickedOnEvent
{
    public string startNodeName; // Name of the Yarn node to start

    public override void OnClick()
    {
        Debug.Log("Sphere clicked: " + gameObject.name);

        // Find the DialogueRunner (you could also assign it manually if you want)
        DialogueRunner dialogueRunner = FindAnyObjectByType<DialogueRunner>();

        if (dialogueRunner != null)
        {
            dialogueRunner.StartDialogue(startNodeName);
        }
        else
        {
            Debug.LogWarning("No DialogueRunner found in the scene!");
        }
    }
}