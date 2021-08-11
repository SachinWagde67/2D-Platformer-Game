using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI KeyTxt;
    [SerializeField] private TextMeshProUGUI WaterDroplettxt;
    [SerializeField] private TextMeshProUGUI foodtxt;

    private TextMeshProUGUI scoreTxt;
    private int score;
    private int keyValue;
    private int waterDropletValue;
    private int foodValue;

    public int KeyToCompleteLevel;
    public int WaterDropletToCompleteLevel;
    public int FoodToCompleteLevel;

    private void Awake()
    {
        scoreTxt = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        scoreTxt.text = "Score: " + score;
        KeyTxt.text = keyValue + "/" + KeyToCompleteLevel;
        WaterDroplettxt.text = waterDropletValue + "/" + WaterDropletToCompleteLevel;
        foodtxt.text = foodValue + "/" + FoodToCompleteLevel;
    }

    public void IncrementScore(int Score)
    {
        score += Score;
        UpdateUI();
    }

    public int WhatisScore()
    {
        return score;
    }

    public void IncrementKey(int keyval)
    {
        keyValue += keyval;
        if(keyValue >= KeyToCompleteLevel)
        {
            KeyTxt.color = Color.green;
        }
        UpdateUI();
    }

    public int WhatIsKey()
    {
        return keyValue;
    }

    public void IncrementWaterDroplet(int WaterVal)
    {
        waterDropletValue += WaterVal;
        if(waterDropletValue >= WaterDropletToCompleteLevel)
        {
            WaterDroplettxt.color = Color.green;
        }
        UpdateUI();
    }

    public int WhatIsWater()
    {
        return waterDropletValue;
    }

    public void IncrementFood(int FoodVal)
    {
        foodValue += FoodVal;
        if (foodValue >= FoodToCompleteLevel)
        {
            foodtxt.color = Color.green;
        }
        UpdateUI();
    }

    public int WhatIsFood()
    {
        return foodValue;
    }
}
