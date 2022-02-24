using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private string sceneName;

    public void RestartBtn(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }

    public void ExitBtn()
    {
        Application.Quit();
    }
}
