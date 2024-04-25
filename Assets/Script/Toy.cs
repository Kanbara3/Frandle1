using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Toy : MonoBehaviour
{
    public int toyId;

    // オブジェクト参照
    public Button timeButton;
    private TextMeshProUGUI timerText;

    private int currentTime; //現在時間
    public int timeToPlay;  //初期時間

    bool isActive = false; // タイマーフラグ

    private FoodManager foodManager;
    private MoneyManager moneyManager;
    private FrandleManager frandleManager;

    private DateTime lastTapTime; //最後に押した時間
    private TimeSpan elapsedTime; //経過時間

    public long HeartIncreaseAmount = 1; // 好感度を増やす量
    public float MoneyIncreaseAmount = 1; //お金をブースト

    void Start()
    {
        // ボタンを押してタイマー開始
        timeButton.onClick.AddListener(() =>
        {
            isActive = true;
            currentTime = timeToPlay;
            lastTapTime = DateTime.UtcNow;
            AdjustMoneyIncrease();
            timeButton.interactable = false; //ボタン無効化
        });

    }

    void Update()
    {
        // タイマー
        if (!isActive) { return; }

        currentTime = timeToPlay - (int)elapsedTime.TotalSeconds;
        //Debug.Log(currentTime);
        if (currentTime > 0)
        {
            elapsedTime = DateTime.UtcNow - lastTapTime;
            //Debug.Log("経過時間：" + elapsedTime.TotalSeconds.ToString());
            IncreaseHeartsAutomatically();
        }
        else
        {

            currentTime = timeToPlay; //秒数リセット
            elapsedTime = TimeSpan.FromSeconds(0); //経過時間リセット
            AdjustMoneyIncreaseDownward();
            timeButton.interactable = true; //ボタン有効化
            isActive = false;
        }
        DisplayTimerText();
        
    }

    // タイマー表示関数
    void DisplayTimerText()
    {
        int hour;
        int minute;
        int second;
        hour = Mathf.FloorToInt(currentTime / 3600);
        minute = Mathf.FloorToInt((currentTime % 3600) / 60);
        second = Mathf.FloorToInt(currentTime % 60);
        timerText.text = hour.ToString("00") + ":" + minute.ToString("00") + ":" + second.ToString("00");
    }

    // EhonとDollの効果
    private float timer = 0f;
    private float interval = 1f;
    void IncreaseHeartsAutomatically()
    {
        timer += Time.deltaTime;
        if(timer >= interval)
        {
            if (this.name == "Ehon") { frandleManager.GainXP(HeartIncreaseAmount); }
            if (this.name == "Doll") { frandleManager.GainXP((long)(HeartIncreaseAmount * 1.5)); }
            frandleManager.UpdateHeartUI();
            timer = 0f;
        }
    }

    // KnifeとBonbの効果
    // 種類によりmoneyIncrease倍率を上げる
    void AdjustMoneyIncrease()
    {
        if(this.name == "Knife") { moneyManager.moneyIncreaseBoost *= MoneyIncreaseAmount*2f; }
        if(this.name == "Bomb") { moneyManager.moneyIncreaseBoost *= MoneyIncreaseAmount*1.5f; }
    }
    // 倍率を下げる
    void AdjustMoneyIncreaseDownward()
    {
        if (this.name == "Knife") { moneyManager.moneyIncreaseBoost /= MoneyIncreaseAmount * 2f; }
        if (this.name == "Bomb") { moneyManager.moneyIncreaseBoost /= MoneyIncreaseAmount * 1.5f; }
    }

    

    // セーブ
    public void SaveTimerFunction()
    {
        PlayerPrefs.SetString("lastTapTimeKey" + toyId, lastTapTime.ToString());
        PlayerPrefs.SetString("isActiveKey" + toyId, isActive.ToString());
}

    // ロード
    public void LoadTimerFunction()
    {
        string time = PlayerPrefs.GetString("lastTapTimeKey" + toyId, "");
        lastTapTime = DateTime.Parse(time);
        string flag = PlayerPrefs.GetString("isActiveKey" + toyId, "");
        isActive = Convert.ToBoolean(flag);
        if (isActive == true ) { timeButton.interactable = false; }
    }
    
    

    // オブジェクト取得
    void Awake()
    {
        timeButton = this.transform.GetChild(2).GetComponent<Button>();
        timerText = this.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        foodManager = GameObject.Find("FoodManager").GetComponent<FoodManager>();
        moneyManager = GameObject.Find("MoneyManager").GetComponent<MoneyManager>();
        frandleManager = GameObject.Find("Frandle").GetComponent<FrandleManager>();
    }
}
