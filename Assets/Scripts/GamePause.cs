using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePause : MonoBehaviour
{
    [SerializeField] private GameObject GamePauseCanvas;
    [SerializeField] private GameObject HeartCanvas;
    
    public void ResumeBtn()
    {
        Time.timeScale = 1f;
        GamePauseCanvas.SetActive(false);
        HeartCanvas.SetActive(true);
    }

    public void RestartBtn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }
    
    public void LobbyBtn()
    {
        SceneManager.LoadScene("Lobby");
        Time.timeScale = 1f;
    }

    public void PauseBtn()
    {
        Time.timeScale = 0f;
        GamePauseCanvas.SetActive(true);
        HeartCanvas.SetActive(false);
    }
}
