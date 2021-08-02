using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private GameObject GameCompleteCanvas;
    [SerializeField] private GameObject HeartCanvas;

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
        HeartCanvas.SetActive(false);
        GameCompleteCanvas.SetActive(true);
    }
}


