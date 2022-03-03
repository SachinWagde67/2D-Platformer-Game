using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ClosedTeleporter : MonoBehaviour
{
    [SerializeField] private GameObject teleporterText;
    [SerializeField] private GameObject teleporter;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private CinemachineVirtualCamera teleporterCM2;
    [SerializeField] private AudioSource teleporterAudio;

    void Start()
    {
        teleporterCM2.Priority = 9;
        teleporter.SetActive(false);
        this.gameObject.SetActive(true);
        teleporterText.SetActive(false);
    }

    private void Update()
    {
        OpenTeleporter();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("player"))
        {
            teleporterText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            teleporterText.SetActive(false);
        }
    }

    private void OpenTeleporter()
    {
        int k = scoreManager.WhatIsKey();
        int w = scoreManager.WhatIsWater();
        int f = scoreManager.WhatIsFood();
        if(k >= scoreManager.KeyToCompleteLevel && w >= scoreManager.WaterDropletToCompleteLevel && f >= scoreManager.FoodToCompleteLevel)
        {
            teleporterCM2.Priority = 11;
            Invoke(nameof(DisableDoor), 2f);
            Invoke(nameof(ChangeCam), 3f);
        }
        
    }

    private void DisableDoor()
    {
        teleporter.SetActive(true);
        this.gameObject.SetActive(false);
        teleporterText.SetActive(false);
        teleporterAudio.Play();
    }

    private void ChangeCam()
    {
        teleporterCM2.Priority = 9;
    }
}
