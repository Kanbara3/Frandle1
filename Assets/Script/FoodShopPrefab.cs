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

    private Image foodImage; // shop�A�C�R���̉摜
    private Button buyButton;
    private TextMeshProUGUI numFoodText;
    private TextMeshProUGUI priceText;
    private TextMeshProUGUI effectText;

    public int foodId; //FoodShop�ő����
    public int basePrice; //FoodShop�ő����
    public int price;  //FoodShop�ő����
    public int effectValue;  //FoodShop��ReferenceToIncreaseXp()�ɂđ����
    public int mealTime; //FoodShop.cs�ő����

    // Start is called before the first frame update
    void Start()
    {
        priceText.text = price + "�~";
        effectText.text = "���ʁF+" + effectValue;
        buyButton.onClick.AddListener(BuyButtonAction);
        NumFoodTextUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //�w��(monayManager�₢���킹)
    private void BuyButtonAction()
    {
        BuyFood(foodId);
    }

    //numFoodText�X�V
    public void NumFoodTextUpdate()
    {
        numFoodText.text = "�������F" + foodManager.GetFoodNum(foodId).ToString();
    }

    //�w���OmonayManager�₢���킹
    public void BuyFood(int foodId)
    {
        int sumFoodNum = foodManager.GetComponent<FoodManager>().sumFoodNum;
        int limitFoodNum = foodManager.GetComponent<FoodManager>().limitFoodNum;

        //int limitFoodNum = foodManager.GetComponent<FoodManager>().foodList[foodId - 1].GetComponent<Food>().limitFoodNum;
        //int numFood = foodManager.GetComponent<FoodManager>().foodList[foodId - 1].GetComponent<Food>().numFood;
        if (sumFoodNum < limitFoodNum)
        {
            Debug.Log(price);
            if (moneyManager.Pay(price))
            {
                foodManager.addFoodStock(foodId, 1);
                NumFoodTextUpdate();
            }
        }
    }

    //Asset>Resources>FoodImage�t�H���_����摜��ǂݍ���
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
        priceText = this.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        effectText = transform.GetChild(3).GetComponent<TextMeshProUGUI>();
    }
}
