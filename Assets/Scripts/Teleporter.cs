using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private string sceneName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("player"))
        {
            NextScene(sceneName);
        }
    }

    private void NextScene(string SceneName)
    {
        LevelManager.Instance.MarkCurrentLevelComplete();
        SceneManager.LoadScene(SceneName);
    }
}


