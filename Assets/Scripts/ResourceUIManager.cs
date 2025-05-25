using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourceUIManager : MonoBehaviour
{

    public static ResourceUIManager Instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public TextMeshProUGUI ironText;
    public TextMeshProUGUI woodText;
    public TextMeshProUGUI enchantText;

    void Start()
    {
        Debug.Log("ResourceUIManager Start");   
        updateWood(4);
        updateIron(2);
        updateEnchant(4);
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void updateIron(int amount)
    {
        ironText.text = amount.ToString();
    }
    public void updateWood(int amount)
    {
        woodText.text = amount.ToString();
    }
    public void updateEnchant(int amount)
    {
        woodText.text = amount.ToString();
    }
}

