using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosedTeleporter : MonoBehaviour
{
    [SerializeField] private GameObject teleporterText;
    [SerializeField] private GameObject teleporter;
    [SerializeField] private int scoreToCompleteLevel;
    [SerializeField] private ScoreManager scoreManager;

    // Start is called before the first frame update
    void Start()
    {
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
        int s = scoreManager.WhatisScore();
        if(s >= scoreToCompleteLevel)
        {
            teleporter.SetActive(true);
            this.gameObject.SetActive(false);
            teleporterText.SetActive(false);
        }
    }
}
