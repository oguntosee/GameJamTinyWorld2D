using UnityEngine;

public class EntchantMaterial : MonoBehaviour
{
   

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Get picked up.
            GetPickedUp();
            Debug.Log("Entchant triggered.");
        }
    }


    private void GetPickedUp()
    {
        GameSceneManager.Instance.LootItem(ItemType.EntchantMaterial, 1);
        Destroy(gameObject);
    }



}
