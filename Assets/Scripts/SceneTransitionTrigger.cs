using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;
using UnityEditor;

public enum EdgeDirection
{
    Left,
    Right
}

public class SceneTransitionTrigger : MonoBehaviour
{

    public EdgeDirection EdgeDirection;
    private string currentSceneName;
    private Rigidbody2D rigidBodyPlayer;

    private string nextSceneName;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            currentSceneName = SceneManager.GetActiveScene().name;
            GameSceneManager.Instance.previousScene = currentSceneName;

            if (this.EdgeDirection == EdgeDirection.Left)
            {
                rigidBodyPlayer = other.GetComponent<Rigidbody2D>();
                if(rigidBodyPlayer.linearVelocityX < 0)
                {
                    char lastChar = currentSceneName[9];
                    string strLastChar = Convert.ToString(lastChar);
                    int sceneNumber = Convert.ToInt32(strLastChar);

                    if(sceneNumber != 0)
                    {
                        nextSceneName = "GameScene" + (sceneNumber - 1);
                        GameSceneManager.Instance.SetTransitionDirection(EdgeDirection.Left);
                        SceneManager.LoadScene(nextSceneName);

                    }
                }
            }
            else if(this.EdgeDirection == EdgeDirection.Right)
            {
                rigidBodyPlayer = other.GetComponent<Rigidbody2D>();
                if (rigidBodyPlayer.linearVelocityX > 0)
                {
                    char lastChar = currentSceneName[9];
                    string strLastChar = Convert.ToString(lastChar);
                    int sceneNumber = Convert.ToInt32(strLastChar);
                    //Debug.Log(sceneNumber);
                    if(sceneNumber != 4)
                    {
                        nextSceneName = "GameScene" + (sceneNumber + 1);
                        GameSceneManager.Instance.SetTransitionDirection(EdgeDirection.Right);
                        SceneManager.LoadScene(nextSceneName);
                    }
                }
            }


        }

    }

}
