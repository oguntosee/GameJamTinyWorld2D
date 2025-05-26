using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{



    public static InventoryManager Instance;


    //
    public GameObject inventoryCanvas;
    
    //
    public GameObject woodPanel;
    public GameObject woodImagePanel;
    public Image woodImage;
    public TextMeshProUGUI woodAmountText;

    public Sprite woodSprite;

    //
    public GameObject ironPanel;
    public GameObject ironImagePanel;
    public Image ironImage;
    public TextMeshProUGUI ironAmountText;

    public Sprite ironSprite;


    //
    public GameObject entchantPanel;
    public GameObject entchantImagePanel;
    public Image entchantImage;
    public TextMeshProUGUI entchantAmountText;

    public Sprite entchantSprite;
    








    public int WoodAmount;
    public int IronAmount;
    public int EntchantAmount;



    //

    private Vector2 woodPanelPosition;
    private Vector2 ironPanelPosition;
    private Vector2 entchantPanelPosition;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }


    void Start()
    {
        SetupInventoryUI();
    }


    private void Update()
    {
        GetItemAmounts();
        UpdateItemAmountInterface();
    }


    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SetupInventoryUI();
    }

    private void SetupInventoryUI()
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


        Vector2 tempPos2D = new Vector2(screenPos2D.x, screenPos2D.y - 100);
        ironPanelPosition = tempPos2D;

        ironPanel.transform.position = ironPanelPosition;
        ironImage.sprite = ironSprite;
        ironPanel.SetActive(true);

        Vector2 tempPos2D2 = new Vector2(tempPos2D.x, tempPos2D.y - 100);
        entchantPanelPosition = tempPos2D2;

        entchantPanel.transform.position = entchantPanelPosition;
        entchantImage.sprite = entchantSprite;
        entchantPanel.SetActive(true);


        if (GameSceneManager.Instance == null)
        {
            WoodAmount = 0;
            IronAmount = 0;
            EntchantAmount = 0;
        }
    }


    private void GetItemAmounts()
    {
        WoodAmount = GameSceneManager.Instance.GetWoodAmount();
        IronAmount = GameSceneManager.Instance.GetIronAmount();
        EntchantAmount = GameSceneManager.Instance.GetEntchantAmount();

    }

    public void UpdateItemAmountInterface()
    {

        string woodAmountStr = WoodAmount.ToString() + " / 6";
        woodAmountText.text = woodAmountStr;

        string ironAmountStr = IronAmount.ToString() + " / 6";
        ironAmountText.text = ironAmountStr;

        string entchantAmountStr = EntchantAmount.ToString() + " / 6";
        entchantAmountText.text = entchantAmountStr;

    }


}
