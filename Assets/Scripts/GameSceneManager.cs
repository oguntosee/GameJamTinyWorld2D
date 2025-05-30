using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;


public class GameSceneManager : MonoBehaviour
{

    public static GameSceneManager Instance;

    //
    private Player playerInstance;

    //
    public string previousScene;
    public static EdgeDirection transitionDirection;
    //
    private Dictionary<string, ChestState> chestStates;

    //
    public SpeakerType activeSpeakerType;

    public List<List<DialogueLine>> wizardStoryDialogues;
    public int wizardStoryDialogueIndex;


    //
    public Dictionary<InventoryItemInfo, int> PlayerInventory;

    public string activeSceneName;




    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            chestStates = new Dictionary<string, ChestState>();
            PlayerInventory = new Dictionary<InventoryItemInfo, int>();
            SceneManager.sceneLoaded += OnSceneLoaded;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        wizardStoryDialogues = new List<List<DialogueLine>>();
        FillWizardStoryDialogues();
        wizardStoryDialogueIndex = 0;

        PrepareScene();


    }

    private void OnDestroy()
    {
        if(Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }        
    }


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PrepareScene();
    }

    public void PrepareScene()
    {
        activeSceneName = SceneManager.GetActiveScene().name;

    }


    public void AssignPlayer(Player player_instance)
    {
        playerInstance = player_instance;
        NotifyPlayerIsReady(playerInstance);
    }

    public void NotifyPlayerIsReady(Player player_instance)
    {
        EnemySpawner.Instance.SetPlayerInstance(player_instance);
    }

    public void NotifyEnemyCanSpawn()
    {
        EnemySpawner.Instance.SetEnemyCanSpawn(true);
    }


    public Player GetPlayerInstance()
    {
        return playerInstance;
    }

    public void LootItem(InventoryItemInfo inventoryItemInfo, int lootedAmount)
    {
        if (PlayerInventory.ContainsKey(inventoryItemInfo))
        {
            PlayerInventory[inventoryItemInfo] += lootedAmount;
        }
        else
        { 
            PlayerInventory[inventoryItemInfo] = lootedAmount;
        }

    }


    private void FillWizardStoryDialogues()
    {
        List<DialogueLine> wizardStoryDialogue_1 = new List<DialogueLine>();
        wizardStoryDialogue_1.Add( new DialogueLine("B�y�c� : \r\nHo� geldin� Kirella.\r\nY�llar sonra, bu k�rede yeniden bir k�p�rt� hissediyorum.\r\nBelki... bu kez ba�ar� m�mk�n olabilir.", SpeakerType.Wizard));
        wizardStoryDialogue_1.Add(new DialogueLine("Kirella : \r\nSiz... kimsiniz? Beni nereden tan�yorsunuz?", SpeakerType.Player));
        wizardStoryDialogue_1.Add( new DialogueLine("B�y�c� : \r\nBen... bu k�renin bilgeli�ini koruyan oyuncaklardan biriyim.\r\nEskiden bir�oklar� �zg�rl��� arad�. Ama �o�u... yoldan sapt�.\r\n"
            + "�imdi s�ra sende. K�reden ��kmak istiyorsun. Ama �nce... bedeninle ruhunu birle�tirmen gerek. Haz�r m�s�n?", SpeakerType.Wizard));
        wizardStoryDialogue_1.Add(new DialogueLine("Kirella : Haz�r�m!", SpeakerType.Player));
        wizardStoryDialogues.Add(wizardStoryDialogue_1);
    }


    private List<DialogueLine> GetRandomDialogueLines(SpeakerType speakerType)
    {

        // TO-DO
        //
        // 1. Make it "Random".


        DialogueLine randomDialogueLine = new DialogueLine("Don't you have anything else to do?..", speakerType);
        List<DialogueLine> randomDialogue = new List<DialogueLine>();
        randomDialogue.Add(randomDialogueLine);
        return randomDialogue;
    }




    public List<DialogueLine> GetDialogueLines(SpeakerType speakerType)
    {
        List<DialogueLine> dialogueLinesToSend = new List<DialogueLine>();
        if(speakerType == SpeakerType.Wizard)
        {
            // 1. Get Wizard Story Dialogue Index.
            // 2. Get that DialogueLines List
            // 3. Send it back.

            if(wizardStoryDialogues.Count == wizardStoryDialogueIndex)
            {
                return (GetRandomDialogueLines(speakerType));
            }

            dialogueLinesToSend = wizardStoryDialogues.ElementAt(wizardStoryDialogueIndex);
            wizardStoryDialogueIndex++;
        }

        return dialogueLinesToSend;
    }

    public void AssignChest(string chest_id, ChestState chestState)
    {
        chestStates.Add(chest_id, chestState);
        //Debug.Log("Chest id: " + chest_id);
        //Debug.Log("isAlreadyLooted: " + chestState.isAlreadyLooted);
    }

    public bool IsChestAlreadyAssigned(string chest_id)
    {
        return chestStates.ContainsKey(chest_id);
    }

    public bool IsChestAlreadyLooted(string chest_id)
    {
        return chestStates.ContainsKey(chest_id) && chestStates[chest_id].isAlreadyLooted;
    }

    public void UpdateChestState(string chest_id, ChestState chestState)
    {
        if(IsChestAlreadyAssigned(chest_id) == true)
        {
            chestStates[chest_id] = chestState;
            //Debug.Log(chest_id + " : " + chestStates[chest_id].isAlreadyLooted);
        }
    }


    public EdgeDirection GetTransitionDirection()
    {
        return transitionDirection;
    }

    public void SetTransitionDirection(EdgeDirection edgeDirection)
    {
        transitionDirection = edgeDirection;
    }


    
    

}
