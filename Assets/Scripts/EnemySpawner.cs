using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System;

public class EnemySpawner : MonoBehaviour
{

    public static EnemySpawner Instance;
    private Player playerInstance;


    public GameObject enemyPrefab;
    public GameObject entchantPrefab;

    private bool canEnemySpawn;

    private Vector3 spawnPosition;
    private string activeSceneName;

    private List<Vector3> enemySpawnPoints;
    private float leftEdge;
    private float rightEdge;


    private float leftOffsetFromPlayer;
    private float rightOffsetFromPlayer;


    private int enemyInstanceAmount;

    private bool canSpawnLeft;
    private bool canSpawnRight;




    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            canEnemySpawn = false;
            DontDestroyOnLoad(gameObject);
            enemySpawnPoints = new List<Vector3>();
            spawnPosition = new Vector3(0, -1.9f, 0);
            rightEdge = 19f;
            leftEdge = -19.5f;
            rightOffsetFromPlayer = 12f;
            leftOffsetFromPlayer = 12f;
            canSpawnLeft = false;
            canSpawnRight = false;
            enemyInstanceAmount = 0;
            GameSceneManager.Instance.SetEnemySpawnerCreated(true);
        }
        else
        {
            Destroy(gameObject);
        }
    }



    private void FixedUpdate()
    {
        if (activeSceneName == "GameScene1" && canEnemySpawn == true)
        {
            if (enemyInstanceAmount < 2)
            {
                for (int i = 0; i < 2 - enemyInstanceAmount; i++)
                {
                    SpawnEnemy();
                    enemyInstanceAmount++;
                }
            }

        }

    }


    public void SpawnEntchantMaterial(Vector3 position)
    {
        Vector3 tempPosition = new Vector3(position.x, -3, position.z);
        GameObject entchantMaterial = Instantiate(entchantPrefab, tempPosition, Quaternion.identity);
    }


    public void SetPlayerInstance(Player player_instance)
    {
        playerInstance = player_instance;
    }

    public void SetEnemyCanSpawn(bool canSpawn, string activeSceneName)
    {
        canEnemySpawn = canSpawn;
        this.activeSceneName = activeSceneName;
        if (activeSceneName == "GameScene1")
        {
            SetEnemySpawnPoints();
        }
    }

    public void SetEnemySpawnPoints()
    {
        enemySpawnPoints.Add(spawnPosition);
        Vector3 tempVector3 = spawnPosition;
        enemySpawnPoints.Add(new Vector3(tempVector3.x - 4, tempVector3.y, tempVector3.z));
        enemySpawnPoints.Add(new Vector3(tempVector3.x - 8, tempVector3.y, tempVector3.z));
    }


    private void SpawnEnemy()
    {
        float xPlayer = playerInstance.transform.position.x;
        canSpawnLeft = true;
        canSpawnRight = true;


        if (xPlayer - leftEdge < 12)
            canSpawnLeft = false;
        if (rightEdge - xPlayer < 12)
            canSpawnRight = false;


        if (canSpawnLeft == true && canSpawnRight == true)
        {
            int randomInt = UnityEngine.Random.Range(0, 2);
            if (randomInt % 2 == 0)
            {
                //Spawn Left.
                float randomFloat = UnityEngine.Random.Range(leftEdge, xPlayer - 12);
                spawnPosition.x = randomFloat;
                GameObject enemyInstance = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            }
            else
            {
                //Spawn Right.
                float randomFloat = UnityEngine.Random.Range(xPlayer + 12, rightEdge);
                spawnPosition.x = randomFloat;
                GameObject gameObject1 = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            }
        }
        else
        {
            if (canSpawnLeft == true)
            {
                // Spawn Left.
                float randomFloat = UnityEngine.Random.Range(leftEdge, xPlayer - 12);
                spawnPosition.x = randomFloat;
                GameObject enemyInstance = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            }
            else
            {
                // Spawn Right.
                float randomFloat = UnityEngine.Random.Range(xPlayer + 12, rightEdge);
                spawnPosition.x = randomFloat;
                GameObject enemyInstance = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            }
        }


    }





    public void EnemyDied()
    {
        enemyInstanceAmount--;
    }



    public Transform GetPlayerTransform()
    {
        return playerInstance.transform;
    }


}
