using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Unity.VisualScripting;

public class FoodShop : MonoBehaviour
{
    public MoneyManager moneyManager;
    public FoodManager foodManager;

    public GameObject foodShopContent;
    public GameObject foodShopPrefab;
    private JsonData foodJsonData;

    public List<GameObject> foodShopList = new List<GameObject>(); //FoodShopPrefabを格納

    // Start is called before the first frame update
    void Start()
    {

        readJson();
        // jsonから読み込み
        foreach (var item in foodJsonData.foodInfos)
        {
            GameObject newFoodShop = Instantiate(foodShopPrefab, new Vector3(0, 0, 0), Quaternion.identity); //Prefabからコピーを作成
            //newFoodShop.transform.SetParent(foodShopContent.transform, false);
            newFoodShop.GetComponent<FoodShopPrefab>().LoadFoodImage("1-" + (item.id));
            newFoodShop.GetComponent<FoodShopPrefab>().foodId = item.id;
            newFoodShop.GetComponent<FoodShopPrefab>().price = item.price;
            newFoodShop.GetComponent<FoodShopPrefab>().mealTime = item.mealTime;
            foodShopList.Add(newFoodShop);
        }

        SpawnFoodShop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //時間帯によって表示する商品を変更
    void SpawnFoodShop()
    {
        int mealTime = GetMealTime();

        foreach (GameObject foodShop in foodShopList)
        {
            FoodShopPrefab shopPrefabScript = foodShop.GetComponent<FoodShopPrefab>();
            if (mealTime == shopPrefabScript.mealTime)
            {
                foodShop.transform.SetParent(foodShopContent.transform, false);
            }
            else
            {
                Destroy(foodShop);
            }
        }
    }

    // 今の時間を1,2,3に変換
    int GetMealTime()
    {
        int hour = DateTime.Now.Hour;

        if (hour >= 0 && hour < 8)
        {
            return 1;
        }
        else if (hour >= 8 && hour < 17)
        {
            return 2;
        }
        else
        {
            return 3;
        }
    }

    void readJson()
    {
        //Resourcesからdocument.jsonを読み込み、string型にキャスト
        string json = Resources.Load<TextAsset>("foodJson").ToString();

        // jsonDataの初期化
        foodJsonData = JsonUtility.FromJson<JsonData>(json);
    }
}
