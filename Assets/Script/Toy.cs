using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Toy : MonoBehaviour
{
    // オブジェクト参照
    private Button timeButton;
    private TextMeshProUGUI timerText;

    private int currentTime; //現在時間
    public int timeToPlay;  //初期時間

    bool isActive = false; // タイマーフラグ

    private FoodManager foodManager;

    private DateTime lastTapTime; //最後に押した時間
    private TimeSpan elapsedTime; //経過時間

    void Start()
    {
        // ボタンを押してタイマー開始
        timeButton.onClick.AddListener(() =>
        {
            isActive = true;
            currentTime = timeToPlay;
            lastTapTime = DateTime.UtcNow;
            timeButton.interactable = false; //ボタン無効化
        });

        
    }

    void Update()
    {
        // タイマー
        if (!isActive) { return; }

        currentTime = timeToPlay - (int)elapsedTime.TotalSeconds;
        if (currentTime > 0)
        {
            elapsedTime = DateTime.UtcNow - lastTapTime;
            
        }
        else
        {
            currentTime = timeToPlay; //秒数リセット
            elapsedTime = TimeSpan.FromSeconds(0); //経過時間リセット
            foodManager.addFoodStock(0, 1);
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

    // オブジェクト取得
    void Awake()
    {
        timeButton = this.transform.GetChild(2).GetComponent<Button>();
        timerText = this.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        foodManager = GameObject.Find("FoodManager").GetComponent<FoodManager>();
    }
}
