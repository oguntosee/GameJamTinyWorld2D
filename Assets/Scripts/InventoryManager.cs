using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    //
    public GameObject inventoryCanvas;
    
    //
    public GameObject woodPanel;
    public GameObject woodImagePanel;
    public Image woodImage;
    public TextMeshProUGUI woodAmountText;

    public Sprite woodSprite;

    //

    private Vector2 woodPanelPosition;




    void Start()
    {
        inventoryCanvas.SetActive(true);


        Vector2 pos2D = new Vector2(-18, 9);
        Vector2 screenPos2D = Camera.main.WorldToScreenPoint(pos2D);
        woodPanelPosition = screenPos2D;

        float xCoordWoodPanel = woodPanelPosition.x;
        float yCoordWoodPanel = woodPanelPosition.y;

        woodPanel.transform.position = woodPanelPosition;
        woodImage.sprite = woodSprite;
        woodPanel.SetActive(true);

    }










}
