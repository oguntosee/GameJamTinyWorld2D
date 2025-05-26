using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Rockk : MonoBehaviour
{
    public GameObject cnv;
    public GameObject dialogPanel;
    public GameObject backgroundPanel;
    public Animator anim;
    public Image fillImage;
    public Animator playerAnim;
    public Player player;
    private bool playerInArea = false;

    public ResourceUIManager resourceUIManager;

    [Header("Rock E animation")]
    public float holdDuration = 3f;
    private float holdTime = 0f;
    private bool isActionCompleted = false;
    private bool wasHoldingE = false;
    private bool hasPlayedExplosion = false; // Patlama animasyonunun �al�p �almad���n� kontrol eder

    void Start()
    {
        player = GameSceneManager.Instance.GetPlayerInstance();
        cnv.SetActive(true);
        anim = GetComponent<Animator>();
        if (fillImage != null)
            fillImage.fillAmount = 0f;
        playerAnim = player.GetComponent<Animator>();
    }

    void Update()
    {
        bool isHoldingE = Input.GetKey(KeyCode.E);

        if (playerInArea && isHoldingE && !isActionCompleted)
        {
            backgroundPanel.SetActive(true);
            dialogPanel.SetActive(false);
            holdTime += Time.deltaTime;
            float progress = holdTime / holdDuration;

            // Fill image g�ncelle
            if (fillImage != null)
                fillImage.fillAmount = progress;

            // Player mining animasyonu (s�rekli �al)
            if (!wasHoldingE) // �lk kez E'ye bas�ld���nda
            {
                playerAnim.Play("playerAxeRock");
            }

            // 3 saniye tamamland���nda patlama
            if (holdTime >= holdDuration && !hasPlayedExplosion)
            {
                anim.Play("rockExplosion");
                hasPlayedExplosion = true;
                isActionCompleted = true;

                // K�sa bir s�re sonra destroy et (animasyonun bitmesini bekle)
                Invoke("animDestroy", 0.5f); // 0.5 saniye sonra yok et
            }

            wasHoldingE = true;
        }
        else
        {
            // E tu�u b�rak�l�rsa progress s�f�rlan�r (i�lem tamamlanmad�ysa)
            if (!isHoldingE && !isActionCompleted)
            {
                if (wasHoldingE)
                {
                    playerAnim.Play("playerIdle");
                    backgroundPanel.SetActive(false);
                }

                ResetProgress();
            }

            wasHoldingE = false;
        }
    }

    private void ResetProgress()
    {
        holdTime = 0f;
        hasPlayedExplosion = false;
        if (fillImage != null)
            fillImage.fillAmount = 0f;
    }

    public void animDestroy()
    {
        Debug.Log("Destroying rock");
        Destroy(gameObject);
        if (playerAnim != null)
            playerAnim.Play("playerIdle");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInArea = true;
            Debug.Log("Player entered the rock area");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInArea = false;
            Debug.Log("Player exited the rock area");

            // Alan d���na ��k�nca her �ey s�f�rlans�n
            ResetProgress();
            isActionCompleted = false;
            wasHoldingE = false;
            hasPlayedExplosion = false;

            playerAnim.Play("playerIdle");
            backgroundPanel.SetActive(false);
        }
    }
}