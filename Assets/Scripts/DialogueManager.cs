using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;




public class DialogueManager : MonoBehaviour
{

    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public Button nextButton;


    public Image speakerPortraitImage;
    public Sprite wizardAvatar;
    public Sprite playerAvatar;
    

    public float offset;

    private Vector2 panelPosition;
    private Vector2 wizardPosition;


    //public List<string> activeDialogueLines;
    public List<DialogueLine> activeDialogueLines;

    private int currentLine = 0;



    void Start()
    {

        dialoguePanel.SetActive(false);
        activeDialogueLines = new List<DialogueLine>();
    }

    private void Update()
    {
        if (dialoguePanel.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            NextLine();
        }
    }
    

    public void StartDialogue(GameObject dialogueObject, SpeakerType speakerType)
    {


        wizardPosition = dialogueObject.transform.position;
        
        Vector2 wizardScreenPosition = Camera.main.WorldToScreenPoint(wizardPosition);
        float xCoordOfDialogueObject = wizardScreenPosition.x;
        float yCoordOfDialogueObject = wizardScreenPosition.y;
        panelPosition = new Vector2(xCoordOfDialogueObject, yCoordOfDialogueObject + offset);

        activeDialogueLines = GameSceneManager.Instance.GetDialogueLines(speakerType);

        currentLine = 0;
        dialoguePanel.SetActive(true);
        dialoguePanel.transform.position = panelPosition;
        dialogueText.text = activeDialogueLines[currentLine].Dialogue_Line;

        speakerPortraitImage.sprite = wizardAvatar;
    
    }

    void NextLine()
    {
        currentLine++;
        if (currentLine < activeDialogueLines.ToArray().Length)
        {
            dialogueText.text = activeDialogueLines[currentLine].Dialogue_Line;
            if (activeDialogueLines[currentLine].SpeakerType == SpeakerType.Wizard)
            {
                speakerPortraitImage.sprite = wizardAvatar;
            }
            else if (activeDialogueLines[currentLine].SpeakerType == SpeakerType.Player)
            {
                speakerPortraitImage.sprite = playerAvatar;
            }
        }
        else
        {
            dialoguePanel.SetActive(false);

        }


    }
}
