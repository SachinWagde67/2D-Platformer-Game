using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lobby : MonoBehaviour
{
    [SerializeField] private GameObject BtnDisable;
    [SerializeField] private GameObject BtnEnable;

    private void Awake()
    {
        BtnDisable.SetActive(true);
        BtnEnable.SetActive(false);
    }

    public void NewGameBtn()
    {
        BtnDisable.SetActive(false);
        BtnEnable.SetActive(true);
    }

    public void LevelBtn(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ExitBtn()
    {
        Application.Quit();
    }
}
