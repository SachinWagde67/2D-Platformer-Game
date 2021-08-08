using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private TextMeshProUGUI scoreTxt;
    private int score;

    private void Awake()
    {
        scoreTxt = GetComponent<TextMeshProUGUI>();
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateUI();
    }

    public void IncrementScore(int keyValue)
    {
        score += keyValue;
        UpdateUI();
    }

    private void UpdateUI()
    {
        scoreTxt.text = "Score: " + score;
    }

    public int WhatisScore()
    {
        return score;
    }
}
