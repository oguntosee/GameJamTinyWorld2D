using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;


public class GameSceneManager : MonoBehaviour
{

    public static GameSceneManager Instance;
    private bool isEnemySpawnerCreated;
    //private bool isEntchantSpawnerCreated;
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
    public Dictionary<ItemType, int> PlayerInventory;

    private string activeSceneName;




    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            chestStates = new Dictionary<string, ChestState>();
            PlayerInventory = new Dictionary<ItemType, int>();
            SceneManager.sceneLoaded += OnSceneLoaded;
            DontDestroyOnLoad(gameObject);
            Debug.Log("GameSceneManager Awake.");
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


    public int GetWoodAmount()
    {
        if (!PlayerInventory.ContainsKey(ItemType.Wood))
            return 0;
        return PlayerInventory[ItemType.Wood];
    }

    public int GetIronAmount()
    {
        if (!PlayerInventory.ContainsKey(ItemType.Iron))
            return 0;
        return PlayerInventory[ItemType.Iron];
    }


    public int GetEntchantAmount()
    {
        if (!PlayerInventory.ContainsKey(ItemType.EntchantMaterial))
            return 0;
        return PlayerInventory[ItemType.EntchantMaterial];
    }



    private void OnDestroy()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    public void SetEnemySpawnerCreated(bool isCreated)
    {
        isEnemySpawnerCreated = isCreated;
        NotifyEnemyCanSpawn(activeSceneName);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PrepareScene();
    }

    public void PrepareScene()
    {
        activeSceneName = SceneManager.GetActiveScene().name;
        Debug.Log("PrepareScene");
        if (isEnemySpawnerCreated)
        {
            Debug.Log("PrepareScene, EnemySpawnerCreated");
            NotifyEnemyCanSpawn(activeSceneName);
            NotifyPlayerIsReady(playerInstance);
        }

    }


    public void AssignPlayer(Player player_instance)
    {
        playerInstance = player_instance;
        //NotifyPlayerIsReady(playerInstance);
    }

    public void NotifyPlayerIsReady(Player player_instance)
    {
        EnemySpawner.Instance.SetPlayerInstance(player_instance);
    }

    public void NotifyEnemyCanSpawn(string activeSceneName)
    {
        Debug.Log("NotifyEnemyCanSpawn()");
        EnemySpawner.Instance.SetPlayerInstance(playerInstance);
        EnemySpawner.Instance.SetEnemyCanSpawn(true, activeSceneName);
    }


    public Player GetPlayerInstance()
    {
        return playerInstance;
    }

    public void LootItem(ItemType itemType, int lootedAmount)
    {
        if (PlayerInventory.ContainsKey(itemType))
        {
            PlayerInventory[itemType] += lootedAmount;
        }
        else
        {
            PlayerInventory[itemType] = lootedAmount;
        }
        Debug.Log(PlayerInventory[itemType]);
    }




    private void FillWizardStoryDialogues()
    {
        List<DialogueLine> wizardStoryDialogue_1 = new List<DialogueLine>();
        wizardStoryDialogue_1.Add(new DialogueLine("Büyücü : \r\nHoþ geldin… Kirella.\r\nYýllar sonra, bu kürede yeniden bir kýpýrtý hissediyorum.\r\nBelki... bu kez baþarý mümkün olabilir.", SpeakerType.Wizard));
        wizardStoryDialogue_1.Add(new DialogueLine("Kirella : \r\nSiz... kimsiniz? Beni nereden tanýyorsunuz?", SpeakerType.Player));
        wizardStoryDialogue_1.Add(new DialogueLine("Büyücü : \r\nBen... bu kürenin bilgeliðini koruyan oyuncaklardan biriyim.\r\nEskiden birçoklarý özgürlüðü aradý. Ama çoðu... yoldan saptý.\r\n"
            + "Þimdi sýra sende. Küreden çýkmak istiyorsun. Ama önce... bedeninle ruhunu birleþtirmen gerek. Hazýr mýsýn?", SpeakerType.Wizard));
        wizardStoryDialogue_1.Add(new DialogueLine("Kirella : Hazýrým!", SpeakerType.Player));
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


    public int GetEntchantMaterial()
    {
        if (!PlayerInventory.ContainsKey(ItemType.EntchantMaterial))
            return 0;
        return PlayerInventory[ItemType.EntchantMaterial];
    }

    public List<DialogueLine> GetDialogueLines(SpeakerType speakerType)
    {
        List<DialogueLine> dialogueLinesToSend = new List<DialogueLine>();
        if (speakerType == SpeakerType.Wizard)
        {
            // 1. Get Wizard Story Dialogue Index.
            // 2. Get that DialogueLines List
            // 3. Send it back.

            if (wizardStoryDialogues.Count == wizardStoryDialogueIndex)
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
        if (IsChestAlreadyAssigned(chest_id) == true)
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
