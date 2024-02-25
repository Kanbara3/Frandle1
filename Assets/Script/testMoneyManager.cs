using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class testMoneyManager : MonoBehaviour
{
    public Button payButton;
    public int money = 0;
    public int moneyIncrease = 1;　//1秒で増えるmoney
    public float span = 1f;
    private float currentTimer;
    private DateTime lastPlayTime; //最期に起動した時間
    private DateTime currentTime;
    private int timeIncreaseMoney;

    // Start is called before the first frame update
    void Start()
    {

        //lastPlayTime = DateTime.Parse(PlayerPrefs.GetString("testLastPlayTimeKey", DateTime.UtcNow.ToString()));
        lastPlayTime = new DateTime(2023, 12, 22, 15, 0, 0);
        //currentTime = DateTime.Now;
        //PlayerPrefs.SetString("testLastPlayTimeKey", currentTime.ToString());
        Debug.Log((currentTime - lastPlayTime).TotalSeconds);

        //moneyをロードする
        money = PlayerPrefs.GetInt("testMoney", 0);
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = DateTime.Now;
        //money = (int)(currentTime - lastPlayTime).TotalSeconds * moneyIncrease;
        timeIncreaseMoney = (int)(currentTime - lastPlayTime).TotalSeconds * moneyIncrease;
        IncreaseMoneyOverTime();
        Debug.Log("m:"+money);
        Debug.Log("mt:"+money+timeIncreaseMoney);
    }

    private void IncreaseMoneyOverTime()
    {
        currentTimer += Time.deltaTime;

        if (currentTimer >= span)
        {
            money += timeIncreaseMoney;
            timeIncreaseMoney = 0;
            lastPlayTime = DateTime.Now;
            //earnedMoney += moneyIncrease;
            //UpdateMoney();
            currentTimer = 0f;
        }
    }

    public void PayMoney()
    {
        money -= 100;
        Debug.Log(money);

        //moneyをセーブする
        PlayerPrefs.SetInt("testMoney", money);
    }

    public void UpIncreaseRate()
    {
        moneyIncrease += 1;
        //現在
    }
}
