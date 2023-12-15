using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class FoodShopPrefab : MonoBehaviour
{
    public FoodManager foodManager;
    public MoneyManager moneyManager;

    private Image foodImage; // shopアイコンの画像
    private Button buyButton;
    private TextMeshProUGUI numFoodText;
    private TextMeshProUGUI priceText;

    public int foodId;
    public int price;

    // Start is called before the first frame update
    void Start()
    {
        priceText.text = price + "円";
        buyButton.onClick.AddListener(BuyButtonAction);
        NumFoodTextUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void test()
    //{
    //    int foodId = 0;
    //    BuyFood(foodId);
    //    Debug.Log("test");
    //}

    private void BuyButtonAction()
    {
        BuyFood(foodId);
        moneyManager.UpdateMoney();//お金の更新
    }

    //numFoodText更新
    public void NumFoodTextUpdate()
    {
        numFoodText.text = "所持数：" + foodManager.GetFoodNum(foodId).ToString();
    }

    public void BuyFood(int foodId)
    {
        if (moneyManager.Pay(price))
        {
            foodManager.addFoodStock(foodId, 1);
            NumFoodTextUpdate();
            Debug.Log(price);
        }
    }

    //Asset>Resources>FoodImageフォルダから画像を読み込み
    public void LoadFoodImage(string imagePath)
    {
        foodImage.sprite = Resources.Load<Sprite>("FoodImage/" + imagePath);
    }

    void Awake()
    {
        foodManager = GameObject.Find("FoodManager").GetComponent<FoodManager>();
        moneyManager = GameObject.Find("MoneyManager").GetComponent<MoneyManager>();
        foodImage = this.transform.GetChild(1).GetComponent<Image>();
        buyButton = this.transform.GetChild(2).GetComponent<Button>();
        numFoodText = this.transform.GetChild(4).GetComponent<TextMeshProUGUI>();
        priceText = this.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
    }
}
