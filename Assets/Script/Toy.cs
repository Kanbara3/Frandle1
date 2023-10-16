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

    // 時間、分、秒
    private int hour;
    private int minute;
    private int second;

    bool isActive = false;

    private FoodManager foodManager;

    private DateTime lastTapTime; //最後に押した時間
    private TimeSpan elapsedTime; //経過時間

    void Start()
    {
        // ボタンを押してタイマー開始
        timeButton.onClick.AddListener(() =>
        {
            Debug.Log("タイマー開始");
            isActive = true;
            currentTime = timeToPlay;
            lastTapTime = DateTime.UtcNow;
            timeButton.interactable = false; //ボタン無効化
        });

        
    }

    void Update()
    {
        //経過時間

        


        // タイマー
        if (isActive)
        {
            Debug.Log(currentTime);
            currentTime = timeToPlay - (int)elapsedTime.TotalSeconds;
            if (currentTime > 0)
            {
                elapsedTime = DateTime.UtcNow - lastTapTime;                
                hour = Mathf.FloorToInt(currentTime / 3600);
                minute = Mathf.FloorToInt((currentTime % 3600) / 60);
                second = Mathf.FloorToInt(currentTime % 60);
                timerText.text = hour.ToString("00") + ":" + minute.ToString("00") + ":" + second.ToString("00");
            }

            if (currentTime <= 0)
            {
                
                currentTime = timeToPlay; //秒数リセット
                elapsedTime = TimeSpan.FromSeconds(0); //経過時間リセット
                hour = Mathf.FloorToInt(currentTime / 3600);
                minute = Mathf.FloorToInt((currentTime % 3600) / 60);
                second = Mathf.FloorToInt(currentTime % 60);
                timerText.text = hour.ToString("00") + ":" + minute.ToString("00") + ":" + second.ToString("00");
                foodManager.addFoodStock(0, 1);
                timeButton.interactable = true; //ボタン有効化
                isActive = false;
                Debug.Log("0以下");
                Debug.Log(isActive);
            }
        }
    }

    void Awake()
    {
        timeButton = this.transform.GetChild(2).GetComponent<Button>();
        timerText = this.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        foodManager = GameObject.Find("FoodManager").GetComponent<FoodManager>();
    }
}
