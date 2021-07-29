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
            Debug.Log("Level Completed");
            NextScene(sceneName);
        }
    }

    private void NextScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
}


