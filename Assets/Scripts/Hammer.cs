using UnityEngine;
using UnityEngine.SceneManagement;

public class Hammer : MonoBehaviour
{
    
    public SpriteRenderer hammer;
    public float moveSpeed = 5f;      // Yatay h�z
    public float rotationSpeed = 720f; // Derece/sn cinsinden d�n�� h�z�
    private bool isFlying = false;

    void Start()
    {
        hammer = GetComponent<SpriteRenderer>();
        hammer.enabled = false;
        hammer.sortingOrder = -1;
        InventoryManager.Instance.UpdateItemAmountInterface();
    }

    void Update()
    {
        int temp = GameSceneManager.Instance.GetEntchantAmount();

        if (temp >= 6 && !isFlying)
        {
            hammer.enabled = true;
            hammer.sortingOrder = 2;
            hammer.transform.position = new Vector3(0, 1f, 0);
            isFlying = true;
        }

        if (isFlying)
        {
            // Sa�a do�ru hareket
            hammer.transform.position += Vector3.right * moveSpeed * Time.deltaTime;

            // D�nme
            hammer.transform.Rotate(0f, 0f, -rotationSpeed * Time.deltaTime); // saat y�n�nde
        }

        if(hammer.transform.position.x == 21.2)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }



    
}
