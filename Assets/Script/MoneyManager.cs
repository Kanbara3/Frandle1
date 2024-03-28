using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class MoneyManager : MonoBehaviour
{
    public TextMeshProUGUI moneyText;
    public int money = 0;
    public int moneyIncrease = 1;　//1秒で増えるmoney
    public float moneyIncreaseBoost = 1;
    private float increaseMoneySpan = 1f;
    private float currentTimer; //1秒を測る変数
    private int MoneyIncreaseLimit = 5; //閉じている間に増えるお金の上限
    private int timeIncreaseMoney;
    private int lastAddMoneyTime;

    public Button buyButton;

    void Start()
    {
        LoadData();
    }

    void Update()
    {
        IncreaseMoney();
        SaveData();
    }

    // increaseMoneySpan秒ごとにmoneyをtimeIncreaseMoney円増やす
    private void IncreaseMoneyOverTime()
    {
        currentTimer += Time.deltaTime;

        if (currentTimer >= increaseMoneySpan)
        {
            money += timeIncreaseMoney;
            lastAddMoneyTime = (int)(DateTime.UtcNow.Subtract(new DateTime(1979, 1, 1))).TotalSeconds; //最期に金が増えた時間
            timeIncreaseMoney = 0;
            UpdateUI();
            currentTimer = 0f;
        }
    }
    // 閉じている間のmoneyを増やす関数
    private void IncreaseMoney()
    {
        int currentTime;
        currentTime = (int)(DateTime.UtcNow.Subtract(new DateTime(1979, 1, 1))).TotalSeconds;
        timeIncreaseMoney = (int)((currentTime - lastAddMoneyTime) * moneyIncrease　* moneyIncreaseBoost);
        timeIncreaseMoney = Mathf.Min(timeIncreaseMoney, MoneyIncreaseLimit); // timeIncreaseMoneyがMoneyIncreaseLimitを超えないように制限する
        IncreaseMoneyOverTime();
    }

    // 放置中に稼げるお金の上限を増やす
    public void MoneyIncreaseLimitBoost(int upRate)
    {
        MoneyIncreaseLimit = upRate;
    }
    // 1秒ごとに増えるお金を増加
    public void MoneyIncreaseBoost(int upRate)
    {
        moneyIncrease += upRate;
    }


    // お金の更新
    private void UpdateUI()
    {
        moneyText.text = money.ToString();
    }

    //moneyのセーブ
    public void SaveData()
    {
        PlayerPrefs.SetInt("moneyData", money);
        PlayerPrefs.SetInt("lastAddMoneyTime", lastAddMoneyTime);
    }

    //moneyのロード
    public void LoadData()
    {
        money = PlayerPrefs.GetInt("moneyData", 0);
        lastAddMoneyTime = PlayerPrefs.GetInt("lastAddMoneyTime", 0);
    }

    //FoodShopPrefab.csから呼び出し
    //支払い関数
    public bool Pay(int cost)
    {
        if (money < cost)
        {
            return false;
        }
        money -= cost;
        UpdateUI();
        return true;
    }
}
