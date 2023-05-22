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

    // Start is called before the first frame update
    void Start()
    {
        LoadConfig();
        //LoadFoodGameObj();
    }

    private void LoadConfig()
    {
        config = Resources.Load<TextAsset>("FoodConfig");
        foodContainer = JsonUtility.FromJson<FoodContainer>(config.text);
    }

    private void LoadFoodGameObj()
    {
        for(int i = 0; i < foodContainer.FoodItems.Length; i++)
        {
            foodContainer.FoodItems[i].foodGameObj = Resources.Load<GameObject>(foodContainer.FoodItems[i].Color);
        }
    }
}
