using UnityEngine;
using UnityEngine.UI;

public class Bush2 : MonoBehaviour
{
    public GameObject cnv;
    public GameObject dialogPanel;
    public GameObject backgroundPanel;
    public Animator anim;
    public Image fillImage;
    public float animDelay = 0f;
    public Animator playerAnim;
    public Player player;
    private bool playerInArea = false;
    private int treeState = 1;

    [Header("Tree E animation")]
    public float holdDuration = 3f;
    private float holdTime = 0f;
    private bool isActionCompleted = false;
    private bool wasHoldingE = false; // E tuþunun önceki durumunu takip etmek için

    void Start()
    {
        player = GameSceneManager.Instance.GetPlayerInstance();
        cnv.SetActive(true);
        anim = GetComponent<Animator>();
        if (fillImage != null)
            fillImage.fillAmount = 0f; // Bar sýfýrdan baþlasýn
        playerAnim = player.GetComponent<Animator>();
    }

    void Update()
    {
        bool isHoldingE = Input.GetKey(KeyCode.E);

        if (playerInArea && isHoldingE && treeState <= 6 && !isActionCompleted)
        {
            backgroundPanel.SetActive(true); // Arka plan paneli aktif edilsin
            dialogPanel.SetActive(false);
            holdTime += Time.deltaTime;
            float progress = holdTime / holdDuration;

            // Fill image güncelle
            if (fillImage != null)
                fillImage.fillAmount = progress;

            // Her 0.5 saniyede bir animasyon oynat
            if (animDelay <= 0f)
            {
                Debug.Log($"tree2_{treeState}");
                anim.Play($"tree2_{treeState}");
                playerAnim.Play("playerAttackWood");
                treeState++;
                animDelay = 0.5f;
            }
            else
            {
                animDelay -= Time.deltaTime;
            }

            // Tamamlandý mý kontrol
            if (holdTime >= holdDuration)
            {
                isActionCompleted = true;
                animDestroy();
                playerAnim.Play("playerIdle");
            }

            wasHoldingE = true;
        }
        else
        {
            // E tuþu býrakýlýrsa progress sýfýrlanýr (iþlem tamamlanmadýysa)
            if (!isHoldingE && !isActionCompleted)
            {
                // Eðer daha önce E tuþuna basýyorduysa ve þimdi býraktýysa
                if (wasHoldingE)
                {
                    playerAnim.Play("playerIdle"); // Idle animasyonuna geç
                    backgroundPanel.SetActive(false); // Arka plan panelini kapat
                }

                holdTime = 0f;
                animDelay = 0f;
                treeState = 1;
                if (fillImage != null)
                    fillImage.fillAmount = 0f;
            }

            wasHoldingE = false;
        }
    }

    public void animDestroy()
    {
        Debug.Log("Destroying bush1");
        Destroy(gameObject);
        playerAnim.Play("playerIdle");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInArea = true;
            Debug.Log("Player entered the bush");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInArea = false;
            Debug.Log("Player exited the bush");

            // Alan dýþýna çýkýnca her þey sýfýrlansýn
            holdTime = 0f;
            animDelay = 0f;
            treeState = 1;
            isActionCompleted = false;
            wasHoldingE = false;
            if (fillImage != null)
                fillImage.fillAmount = 0f;

            // Player animasyonunu idle'a geçir
            playerAnim.Play("playerIdle");
            backgroundPanel.SetActive(false);
        }
    }
}