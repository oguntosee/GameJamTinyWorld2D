using UnityEngine;
using UnityEngine.InputSystem.XInput;
using UnityEngine.SceneManagement;



public class WizardNPC : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public GameObject canvas;
    public Rigidbody2D rb;


    public float linearVelocity = -5f;

    public SpeakerType wizardSpeakerType;

    private void Start()
    {
        
        rb.linearVelocity = new Vector2(linearVelocity,0);   //rigidbody
        wizardSpeakerType = SpeakerType.Wizard;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            rb.linearVelocity = Vector2.zero;
            canvas.SetActive(true);
            dialogueManager.StartDialogue(this.gameObject, wizardSpeakerType);
        }

    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(canvas != null)
            {
                canvas.SetActive(false);
            }

        }
    }
   

}
