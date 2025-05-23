using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeController : MonoBehaviour
{
    public void PlayButtonClick()
    {
        SceneManager.LoadScene(SceneData.GameScene2);
    }
}