using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameComplete : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private string nextSceneName;

    public void NextBtn()
    {
        SoundManager.Instance.Play(Sounds.ButtonClick);
        SceneManager.LoadScene(nextSceneName);
    }

    public void RestartBtn()
    {
        SoundManager.Instance.Play(Sounds.ButtonClick);
        SceneManager.LoadScene(sceneName);
    }
}
