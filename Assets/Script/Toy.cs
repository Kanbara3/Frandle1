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
    private Button timeButton;
    private TextMeshProUGUI timerText;
    private long XP = 0;

    private int currentTime; //現在時間
    public int timeToPlay;  //初期時間
    public int price; //値段

    bool isActive = false; // タイマーフラグ

    private FoodManager foodManager;
    private MoneyManager moneyManager;
    private FrandleManager frandleManager;
    private AutoXpManager autoXpManager;

    private DateTime lastTapTime; //最後に押した時間
    public TimeSpan elapsedTime; //経過時間

    public long HeartIncreaseAmount = 1; // 好感度を増やす量
    public float MoneyIncreaseAmount = 1; //お金をブースト

    void Start()
    {
        //ContinueToyWhileClosed();
        // ボタンを押してタイマー開始
        timeButton.onClick.AddListener(() =>
        {
            if (price < moneyManager.money)
            {
                isActive = true;
                currentTime = timeToPlay;
                lastTapTime = DateTime.UtcNow;
                AdjustMoneyIncrease();
                moneyManager.Pay(price);
                timeButton.interactable = false; //ボタン無効化
                if (this.name == "Ehon" || this.name == "Doll")
                {
                    autoXpManager.AutoXpGainOn(timeToPlay);
                }
            }
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
            //for(int i = 0; i<elapsedTime.TotalSeconds; i++)
            //{
                IncreaseHeartsAutomatically();
            //}
            //IncreaseHeartsAutomatically();
            
        }
        else
        {
            currentTime = timeToPlay; //秒数リセット
            elapsedTime = TimeSpan.FromSeconds(0); //経過時間リセット
            AdjustMoneyIncreaseDownward();
            timeButton.interactable = true; //ボタン有効化
            isActive = false;
            if (this.name == "Ehon" || this.name == "Doll")
            {
                autoXpManager.AutoXpGainOff();
            }
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
        XP = frandleManager.XP;
        timer += Time.deltaTime;
        if(timer >= interval)
        {
            //if (this.name == "Ehon") { frandleManager.GainXP((long)(frandleManager.oneTapIncrease * (HeartIncreaseAmount * 0.1 + 1))); }
            //if (this.name == "Doll") { frandleManager.GainXP((long)(frandleManager.oneTapIncrease * (HeartIncreaseAmount * 0.1 + 1)*1.5)); }
            //frandleManager.UpdateHeartUI();
            timer = 0f;
        }
    }

    // KnifeとBonbの効果
    // 種類によりmoneyIncrease倍率を上げる
    void AdjustMoneyIncrease()
    {
        if(this.name == "Knife") { moneyManager.moneyIncreaseBoost *= MoneyIncreaseAmount*1.5f; }
        if(this.name == "Bomb") { moneyManager.moneyIncreaseBoost *= MoneyIncreaseAmount*2f; }
    }
    // 倍率を下げる
    void AdjustMoneyIncreaseDownward()
    {
        if (this.name == "Knife") { moneyManager.moneyIncreaseBoost /= MoneyIncreaseAmount * 1.5f; }
        if (this.name == "Bomb") { moneyManager.moneyIncreaseBoost /= MoneyIncreaseAmount * 2f; }
    }

    // セーブ
    public void SaveTimerFunction()
    {
        PlayerPrefs.SetString("lastTapTimeKey" + toyId, lastTapTime.ToString());
        PlayerPrefs.SetString("isActiveKey" + toyId, isActive.ToString());
        //PlayerPrefs.SetString("PauseTime" + toyId, pauseTime.ToString());
    }

    // ロード
    public void LoadTimerFunction()
    {
        string time = PlayerPrefs.GetString("lastTapTimeKey" + toyId, "");
        lastTapTime = DateTime.Parse(time);
        string flag = PlayerPrefs.GetString("isActiveKey" + toyId, "");
        isActive = Convert.ToBoolean(flag);
        if (isActive == true ) { timeButton.interactable = false; }
        string pauseTime = PlayerPrefs.GetString("PauseTime" + toyId, "");
        //this.pauseTime = DateTime.Parse(pauseTime);
    }
    
    

    // オブジェクト取得
    void Awake()
    {
        timeButton = this.transform.GetChild(2).GetComponent<Button>();
        timerText = this.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        foodManager = GameObject.Find("FoodManager").GetComponent<FoodManager>();
        moneyManager = GameObject.Find("MoneyManager").GetComponent<MoneyManager>();
        frandleManager = GameObject.Find("Frandle").GetComponent<FrandleManager>();
        if (this.name == "Ehon")
        {
            autoXpManager = GameObject.Find("AutoXpManagerEhon").GetComponent<AutoXpManager>();
            autoXpManager.SetToy(this);
        }
        else
        {
            autoXpManager = GameObject.Find("AutoXpManagerDoll").GetComponent<AutoXpManager>();
            autoXpManager.SetToy(this);
        }
        
    }
}
