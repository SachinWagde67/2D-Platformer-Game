using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] hearts;
    [SerializeField] private GameObject GameOverCanvas;
    [SerializeField] private GameObject HeartCanvas;

    private void Awake()
    {
        GameOverCanvas.SetActive(false);
        HeartCanvas.SetActive(true);
        for(int i=0;i<hearts.Length;i++)
        {
            hearts[i].SetActive(true);
        } 
    }

    public void Heart(int health)
    {
        if(health > 3)
        {
            health = 3;
        }
        if(health > 0)
        {
            hearts[(hearts.Length - health - 1)].SetActive(false);
        }
        else
        {
            Invoke(nameof(EnableGameOver), 0.5f);
        }
    }

    public void EnableGameOver()
    {
        HeartCanvas.SetActive(false);
        GameOverCanvas.SetActive(true);
    }
}
