using UnityEngine;

public class Axe : MonoBehaviour
{
    public SpriteRenderer axe;
    public Chest chest;
    public bool isOpening => isOpening;
    void Start()
    {
        axe = GetComponent<SpriteRenderer>();
        axe.enabled = false; // Baþlangýçta görünmesin
    }

    void Update()
    {
        if (chest != null && chest.isOpening)
        {
            axe.enabled = true; // Sandýk açýlýnca görünür
            axe.transform.position = chest.transform.position + new Vector3(0, 3f, 0); // Sandýðýn üstünde konumlandýr
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            axe.enabled = false;
            Destroy(gameObject); // Oyuncu sandýðý açtýðýnda baltayý yok et
        }
    }

}
