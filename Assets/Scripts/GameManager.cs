using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject Heart1, Heart2, Heart3;
    [SerializeField] private GameObject GameOverCanvas;
    [SerializeField] private GameObject HeartCanvas;

    private void Awake()
    {
        GameOverCanvas.SetActive(false);
        HeartCanvas.SetActive(true);
        Heart1.gameObject.SetActive(true);
        Heart2.gameObject.SetActive(true);
        Heart3.gameObject.SetActive(true);
    }

    // Update is called once per frame
    public void Heart(float health)
    {
        if(health > 3)
        {
            health = 3;
        }

        switch(health)
        {
            case 3:
                Heart1.gameObject.SetActive(true);
                Heart2.gameObject.SetActive(true);
                Heart3.gameObject.SetActive(true);
                break;
            case 2:
                Heart1.gameObject.SetActive(true);
                Heart2.gameObject.SetActive(true);
                Heart3.gameObject.SetActive(false);
                break;
            case 1:
                Heart1.gameObject.SetActive(true);
                Heart2.gameObject.SetActive(false);
                Heart3.gameObject.SetActive(false);
                break;
            case 0:
                Heart1.gameObject.SetActive(false);
                Heart2.gameObject.SetActive(false);
                Heart3.gameObject.SetActive(false);
                Invoke(nameof(StopTime), 1f);
                break;
        }
    }

    public void StopTime()
    {
        HeartCanvas.SetActive(false);
        GameOverCanvas.SetActive(true);
        Time.timeScale = 0;
    }
}
