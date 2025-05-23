using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerSpawner : MonoBehaviour
{

    public GameObject playerPrefab;
    public bool isFirstSpawn;

    public GameObject playerSpawnPoint;
    private string scenePlayerComingFrom;

    public Vector3 spawnPosition;

    private void Start()
    {

        //Debug.Log("Start of PlayerSpawner.");

        if (GameObject.FindWithTag("Player") == null)
        {

            GameObject player = Instantiate(playerPrefab, playerSpawnPoint.transform.position, Quaternion.identity);
            DontDestroyOnLoad(player);
            isFirstSpawn = true;
            //Debug.Log("First Spawn.");
            
        }
        else
        {
            GameObject player = GameObject.FindWithTag("Player");
            EdgeDirection edgeComingFrom = GameSceneManager.Instance.GetTransitionDirection();
            if(edgeComingFrom == EdgeDirection.Right)
            {
                // Spawn on left.
                spawnPosition = GameObject.FindWithTag("LeftSpawn").transform.position;
                GameObject playerObject = GameObject.FindWithTag("Player");
                playerObject.transform.position = spawnPosition;
            }
            else if(edgeComingFrom == EdgeDirection.Left)
            {
                // Spawn on right.
                spawnPosition = GameObject.FindWithTag("RightSpawn").transform.position;
                GameObject playerObject = GameObject.FindWithTag("Player");
                playerObject.transform.position = spawnPosition;
            }

            scenePlayerComingFrom = GameSceneManager.Instance.previousScene;

            isFirstSpawn = false;
        
        }


    }



    









}
