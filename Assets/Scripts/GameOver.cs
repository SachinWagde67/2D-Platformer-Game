using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private string sceneName;

    private void OnEnable()
    {
        SoundManager.Instance.Play(Sounds.Afterdeath);
    }

    public void RestartBtn()
    {
        SoundManager.Instance.Play(Sounds.ButtonClick);
        SceneManager.LoadScene(sceneName);
    }

    public void ExitBtn()
    {
        SoundManager.Instance.Play(Sounds.ButtonClick);
        SceneManager.LoadScene("Lobby");
    }
}
