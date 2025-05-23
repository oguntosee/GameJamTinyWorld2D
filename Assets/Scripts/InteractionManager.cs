using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteractionManager : MonoBehaviour
{

    public GameObject interactionCanvas;
    public GameObject interactionPanel;
    public Text interactionText;

    private GameObject activeInteractableGameObject;

    private float xCoordinatesOfGameObject;
    private float yCoordinatesOfGameObject;

    public float offset;


    private void Start()
    {
        interactionPanel.SetActive(false);
        
    }

    public void SetInteractionAvailable(GameObject interactableGameObject)
    {
        if (interactableGameObject.CompareTag("Lootable"))
        {

            activeInteractableGameObject = interactableGameObject;

            Vector2 pos2D = interactableGameObject.transform.position;

            Vector2 screenPos2D = Camera.main.WorldToScreenPoint(pos2D);
            xCoordinatesOfGameObject = screenPos2D.x;
            yCoordinatesOfGameObject = screenPos2D.y;

            Vector2 positionInteractionPanel = new Vector2(xCoordinatesOfGameObject, yCoordinatesOfGameObject + offset);
            interactionPanel.transform.position = positionInteractionPanel;

            interactionCanvas.SetActive(true);
            interactionPanel.SetActive(true);
            interactionPanel.transform.position = positionInteractionPanel;
            interactionText.text = "Press E to open";
        }
    }

    



    public void SetInteractionUnavailable(GameObject interactableGameObject)
    {
        interactionPanel.SetActive(false);
        interactionCanvas.SetActive(false);
        
    }



}
