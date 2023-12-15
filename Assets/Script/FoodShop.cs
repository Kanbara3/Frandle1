using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class FoodShop : MonoBehaviour
{
    public MoneyManager moneyManager;
    public FoodManager foodManager;

    public GameObject foodShopContent;
    public GameObject foodShopPrefab;
    private JsonData foodJsonData;

    // Start is called before the first frame update
    void Start()
    {
        //buyButton.onClick.AddListener(test);

        readJson();
        // jsonから読み込み
        foreach (var item in foodJsonData.foodInfos)
        {
            GameObject newFoodShop = Instantiate(foodShopPrefab, new Vector3(0, 0, 0), Quaternion.identity); //Prefabからコピーを作成
            newFoodShop.transform.SetParent(foodShopContent.transform, false);
            newFoodShop.GetComponent<FoodShopPrefab>().LoadFoodImage("1-" + (item.id));
            newFoodShop.GetComponent<FoodShopPrefab>().foodId = item.id;
            newFoodShop.GetComponent<FoodShopPrefab>().price = item.price;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void readJson()
    {
        //Resourcesからdocument.jsonを読み込み、string型にキャスト
        string json = Resources.Load<TextAsset>("foodJson").ToString();

        // jsonDataの初期化
        foodJsonData = JsonUtility.FromJson<JsonData>(json);
    }
}
