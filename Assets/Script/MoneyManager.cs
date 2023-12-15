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
    public float span = 1f;
    private float currentTime = 0f;
    private DateTime stoppedTime;
    private TimeSpan elapsedTime; //停止中に経過した時間

    public Button buyButton;

    // Start is called before the first frame update
    void Start()
    {
        //buyFood();
        LoadMoneyFunction();
        LoadStoppedTimeFunction();
    }

    // Update is called once per frame
    void Update()
    {
        IncreaseMoneyOverTime();
        SaveMoneyFunction();
        SaveStoppedTimeFunction();
    }

    //ゲーム終了時に時刻を取得
    void OnApplicationQuit()
    {
        stoppedTime = DateTime.UtcNow;
    }

    // ゲーム一時停止時に時刻を取得,ゲーム一時停止復帰に関数実行
    void OnApplicationPause(bool pauseStatus)
    {
        if(pauseStatus)
        {
            stoppedTime = DateTime.UtcNow;
        }
        else
        {
            LoadMoneyFunction();
            //LoadStoppedTimeFunction();
        }
    }

    // stoppedTimeのセーブ
    public void SaveStoppedTimeFunction()
    {
        PlayerPrefs.SetString("stoppedTimeKey", stoppedTime.ToString());
    }

    // stoppedTimeのロード
    public void LoadStoppedTimeFunction()
    {
        string strStoppTime = PlayerPrefs.GetString("stoppedTimeKey", "");
        stoppedTime = DateTime.Parse(strStoppTime);
        elapsedTime = DateTime.UtcNow - stoppedTime;
        IncreaseMoneyForPauseOrStopTimeElapsed();
        elapsedTime = TimeSpan.FromSeconds(0);
    }

    // 停止中に経過した時間分moneyを増加
    public void IncreaseMoneyForPauseOrStopTimeElapsed()
    {
        money += moneyIncrease * (int)elapsedTime.TotalSeconds;
    }

    //1秒ごとにmoneyが増える
    public void IncreaseMoneyOverTime()
    {
        currentTime += Time.deltaTime;

        if(currentTime >= span)
        {
            money += moneyIncrease;
            moneyText.text = money.ToString();
            currentTime = 0f;
        }
    }

    //moneyのセーブ
    public void SaveMoneyFunction()
    {
        PlayerPrefs.SetInt("moneyData", money);
    }

    //moneyのロード
    public void LoadMoneyFunction()
    {
        money = PlayerPrefs.GetInt("moneyData", 0);
    }

    public bool Pay(int cost)
    {
        if (money < cost)
        {
            return false;
        }
        money -= cost;
        return true;
    }
}
