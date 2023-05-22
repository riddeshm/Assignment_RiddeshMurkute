using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FoodContainer
{
    public Food[] FoodItems;
}

[Serializable]
public class Food
{
    public string Color;
    public int Points;
    public GameObject foodGameObj;
}

public class FoodConfig : MonoBehaviour
{
    private TextAsset config;
    private FoodContainer foodContainer;

    public FoodContainer FoodContainer
    {
        get { return foodContainer; }
    }
    private void Start()
    {
        LoadConfig();
    }

    private void LoadConfig()
    {
        config = Resources.Load<TextAsset>("FoodConfig");
        foodContainer = JsonUtility.FromJson<FoodContainer>(config.text);
    }
}
