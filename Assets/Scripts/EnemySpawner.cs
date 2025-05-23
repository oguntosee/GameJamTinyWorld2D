using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public static EnemySpawner Instance;
    private Player playerInstance;

    public GameObject enemyPrefab;
    public GameObject enemySpawnPoint;
    private bool canEnemySpawn;
    
    private Vector3 spawnPosition;

    

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            canEnemySpawn = false;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // SpawnEnemy();
    }


    public void SetPlayerInstance(Player player_instance)
    {
        playerInstance = player_instance;

    }

    public void SetEnemyCanSpawn(bool canSpawn)
    {
        canEnemySpawn = canSpawn;
    }

    
    public void SpawnEnemy()
    {
        GameObject enemy1 = Instantiate(enemyPrefab, enemySpawnPoint.transform.position, Quaternion.identity);
    }


    public Transform GetPlayerTransform()
    {
        return playerInstance.transform;
    }






}
