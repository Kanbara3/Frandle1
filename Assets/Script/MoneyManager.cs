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
    public int moneyIncrease = 1;�@//1�b�ő�����money
    public float moneyIncreaseBoost = 1;
    private float increaseMoneySpan = 1f;
    private float currentTimer; //1�b�𑪂�ϐ�
    private int MoneyIncreaseLimit = 5; //���Ă���Ԃɑ����邨���̏��
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

    // increaseMoneySpan�b���Ƃ�money��timeIncreaseMoney�~���₷
    private void IncreaseMoneyOverTime()
    {
        currentTimer += Time.deltaTime;

        if (currentTimer >= increaseMoneySpan)
        {
            money += timeIncreaseMoney;
            lastAddMoneyTime = (int)(DateTime.UtcNow.Subtract(new DateTime(1979, 1, 1))).TotalSeconds; //�Ŋ��ɋ�������������
            timeIncreaseMoney = 0;
            UpdateUI();
            currentTimer = 0f;
        }
    }
    // ���Ă���Ԃ�money�𑝂₷�֐�
    private void IncreaseMoney()
    {
        int currentTime;
        currentTime = (int)(DateTime.UtcNow.Subtract(new DateTime(1979, 1, 1))).TotalSeconds;
        timeIncreaseMoney = (int)((currentTime - lastAddMoneyTime) * moneyIncrease�@* moneyIncreaseBoost);
        timeIncreaseMoney = Mathf.Min(timeIncreaseMoney, MoneyIncreaseLimit); // timeIncreaseMoney��MoneyIncreaseLimit�𒴂��Ȃ��悤�ɐ�������
        IncreaseMoneyOverTime();
    }

    // ���u���ɉ҂��邨���̏���𑝂₷
    public void MoneyIncreaseLimitBoost(int upRate)
    {
        MoneyIncreaseLimit = upRate;
    }
    // 1�b���Ƃɑ����邨���𑝉�
    public void MoneyIncreaseBoost(int upRate)
    {
        moneyIncrease += upRate;
    }


    // �����̍X�V
    private void UpdateUI()
    {
        moneyText.text = money.ToString();
    }

    //money�̃Z�[�u
    public void SaveData()
    {
        PlayerPrefs.SetInt("moneyData", money);
        PlayerPrefs.SetInt("lastAddMoneyTime", lastAddMoneyTime);
    }

    //money�̃��[�h
    public void LoadData()
    {
        money = PlayerPrefs.GetInt("moneyData", 0);
        lastAddMoneyTime = PlayerPrefs.GetInt("lastAddMoneyTime", 0);
    }

    //FoodShopPrefab.cs����Ăяo��
    //�x�����֐�
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
