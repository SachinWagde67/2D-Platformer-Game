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
            SoundManager.Instance.Play(Sounds.Teleporter);
            Invoke(nameof(NextScene),1f);
        }
    }

    private void NextScene()
    {
        LevelManager.Instance.MarkCurrentLevelComplete();
        SceneManager.LoadScene(sceneName);
    }
}


