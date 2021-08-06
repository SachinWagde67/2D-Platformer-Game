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
        SoundManager.Instance.Play(Sounds.StartBtn);
        BtnDisable.SetActive(false);
        BtnEnable.SetActive(true);
    }

    public void LevelBtn(string sceneName)
    {
       
        LevelStatus levelStatus = LevelManager.Instance.GetLevelStatus(sceneName);
        switch (levelStatus)
        {
            case LevelStatus.Locked:
                Debug.Log("Can't Play this Level");
                break;

            case LevelStatus.Unlocked:
                SoundManager.Instance.Play(Sounds.ButtonClick);
                SceneManager.LoadScene(sceneName);
                break;

            case LevelStatus.Completed:
                SceneManager.LoadScene(sceneName);
                break;
        }
    }

    public void ExitBtn()
    {
        Application.Quit();
    }
}
