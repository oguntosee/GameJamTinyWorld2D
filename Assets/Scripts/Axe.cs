using UnityEngine;

public class Axe : MonoBehaviour
{
    public SpriteRenderer axe;
    public Chest chest;
    public bool isOpening => isOpening;
    void Start()
    {
        axe = GetComponent<SpriteRenderer>();
        axe.enabled = false; // Ba�lang��ta g�r�nmesin
    }

    void Update()
    {
        if (chest != null && chest.isOpening)
        {
            axe.enabled = true; // Sand�k a��l�nca g�r�n�r
            axe.transform.position = chest.transform.position + new Vector3(0, 3f, 0); // Sand���n �st�nde konumland�r
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            axe.enabled = false;
            Destroy(gameObject); // Oyuncu sand��� a�t���nda baltay� yok et
        }
    }

}
