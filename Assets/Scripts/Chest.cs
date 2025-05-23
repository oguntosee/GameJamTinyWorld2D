using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Collections;
using UnityEngine.SceneManagement;

// STATE is SAVED in GAMESCENEMANAGER.CS INSTANCE.
public class ChestState
{
    public bool isAlreadyLooted;
}

public class Chest : MonoBehaviour
{

    public InteractionManager interactionManager;
    private Animator anim;

    private bool isInteractionAvailable;
    private bool isOpening;

    private string chestID;

    private ChestState chestState;

    private void Awake()
    {
        if(string.IsNullOrEmpty(chestID) == true)
        {
            chestID = SceneManager.GetActiveScene().name + "_" + transform.position.ToString();
        }
    }

    private void Start()
    {
        anim = GetComponent<Animator>(); //anim
        isInteractionAvailable = false;
        //isItemRetrieved = false;
        isOpening = false;
        //chestState.isAlreadyLooted = false;
        chestState = new ChestState();
        

        if (GameSceneManager.Instance.IsChestAlreadyAssigned(chestID) == false)
        {
            GameSceneManager.Instance.AssignChest(chestID, chestState);
        }
        chestState.isAlreadyLooted = GameSceneManager.Instance.IsChestAlreadyLooted(chestID);
        anim.SetBool("isLootedBefore", chestState.isAlreadyLooted);

        //Debug.Log("Chest created: " + chestID);
        //Debug.Log("isAlreadyLooted: " + chestState.isAlreadyLooted);
    }

    private void Update()
    {
        if (isInteractionAvailable == true)
        {
            if (Input.GetKeyDown(KeyCode.E) && chestState.isAlreadyLooted == false && isOpening == false && isInteractionAvailable == true)
            {
                OpenChest();
            }
        }
        
    }

    public string GetChestID()
    {
        return chestID;
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && chestState.isAlreadyLooted == false)
        {
            isInteractionAvailable = true;
            interactionManager.SetInteractionAvailable(this.gameObject);
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInteractionAvailable = false;
            interactionManager.SetInteractionUnavailable(this.gameObject);
        }

    }

    private void OpenChest()
    {
        StartCoroutine(OpenChestRoutine());

    }

    private void CloseChest()
    {
        anim.SetBool("isOpened", false);
        
    }

    private IEnumerator OpenChestRoutine()
    {
        isOpening = true;
        anim.SetBool("isOpened", true);
        isInteractionAvailable = false;
        interactionManager.SetInteractionUnavailable(this.gameObject);
        yield return new WaitForSeconds(1.5f); // adjust to your animation time

        //isItemRetrieved = true;
        isOpening = false;

        CloseChest();
        chestState.isAlreadyLooted = true;
        GameSceneManager.Instance.UpdateChestState(chestID, chestState);
        anim.SetBool("isLootedBefore", chestState.isAlreadyLooted);
    }

    

    





}
