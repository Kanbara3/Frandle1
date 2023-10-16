using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToyTime : MonoBehaviour
{
    /*
    // スクリプト参照用
    public GameObject GameManager;

    // 絵本オブジェクト参照
    public Button ehon_button;
    public TextMeshProUGUI ehon_timerText;
    public float ehon_totalTime;

    // 人形オブジェクト参照
    public Button ningyou_button;
    public TextMeshProUGUI ningyou_timerText;
    public float ningyou_totalTime;

    // おままごとオブジェクト参照
    public Button omamagoto_button;
    public TextMeshProUGUI omamagoto_timerText;
    public float omamagoto_totalTime;

    // 弾幕オブジェクト参照
    public Button danmaku_button;
    public TextMeshProUGUI danmaku_timerText;
    public float danmaku_totalTime;

    // 現在の秒数
    int seconds1;
    int seconds2;
    int seconds3;
    int seconds4;

    bool isActive1 = false;
    bool isActive2 = false;
    bool isActive3 = false;
    bool isActive4 = false;

    // 初期秒数保存
    public float ehon_firstTime;
    public float ningyou_firstTime;
    public float omamagoto_firstTime;
    public float danmaku_firstTime;

    // Start is called before the first frame update
    void Start()
    {
        // ボタンを押してタイマー開始
        ehon_button.onClick.AddListener(() =>
        {
            isActive1 = true;
        });
        ningyou_button.onClick.AddListener(() =>
        {
            isActive2 = true;
        });
        omamagoto_button.onClick.AddListener(() =>
        {
            isActive3 = true;
        });
        danmaku_button.onClick.AddListener(() =>
        {
            isActive4 = true;
        });
    }

    // Update is called once per frame
    void Update()
    {
        // 絵本タイマー
        if (isActive1)
        {
            ehon_totalTime -= Time.deltaTime;
            seconds1 = (int)ehon_totalTime;

            if (seconds1 > 60)
            {
                int hour1 = seconds1 / 3600;
                int minute1 = (int)seconds1 / 60 - (hour1 * 60);
                ehon_timerText.text = hour1.ToString("00") + ":" + minute1.ToString("00");
            }
            else
            {
                ehon_timerText.text = seconds1.ToString();
            }

            if (seconds1 <= 0)
            {
                isActive1 = false;
                ehon_totalTime = ehon_firstTime;
                seconds1 = (int)ehon_totalTime;
                int hour1 = (int)seconds1 / 3600;
                int minute1 = (int)seconds1 / 60 - (hour1 * 60);
                ehon_timerText.text = "01:00"; //hour1.ToString("00") + ":" + minute1.ToString("00");
                //GameManager.GetComponent<GohanPlus>().ehon_plus();
            }
        }

        // お人形タイマー
        if (isActive2)
        {
            ningyou_totalTime -= Time.deltaTime;
            seconds2 = (int)ningyou_totalTime;

            if (seconds2 > 60)
            {
                int hour2 = (int)seconds2 / 3600;
                int minute2 = (int)seconds2 / 60 - (hour2 * 60);
                ningyou_timerText.text = hour2.ToString("00") + ":" + minute2.ToString("00");
            }
            else
            {
                ningyou_timerText.text = seconds2.ToString();
            }

            if (seconds2 <= 0)
            {
                isActive2 = false;
                ningyou_totalTime = ningyou_firstTime;
                seconds2 = (int)ningyou_totalTime;
                int hour2 = (int)seconds2 / 3600;
                int minute2 = (int)seconds2 / 60 - (hour2 * 60);
                ningyou_timerText.text = "03:00";//hour2.ToString("00") + ":" + minute2.ToString("00");
                //GameManager.GetComponent<GohanPlus>().ningyou_plus();
            }
        }

        // おままごとタイマー
        if (isActive3)
        {
            omamagoto_totalTime -= Time.deltaTime;
            seconds3 = (int)omamagoto_totalTime;

            if (seconds3 > 60)
            {
                int hour3 = (int)seconds3 / 3600;
                int minute3 = (int)seconds3 / 60 - (hour3 * 60);
                omamagoto_timerText.text = hour3.ToString("00") + ":" + minute3.ToString("00");
            }
            else
            {
                omamagoto_timerText.text = seconds3.ToString();
            }

            if (seconds3 <= 0)
            {
                isActive3 = false;
                omamagoto_totalTime = omamagoto_firstTime;
                seconds3 = (int)omamagoto_totalTime;
                int hour3 = (int)seconds3 / 3600;
                int minute3 = (int)seconds3 / 60 - (hour3 * 60);
                omamagoto_timerText.text = "06:00";//hour3.ToString("00") + ":" + minute3.ToString("00");
                //GameManager.GetComponent<GohanPlus>().omamagoto_plus();
            }
        }

        // 弾幕タイマー
        if (isActive4)
        {
            danmaku_totalTime -= Time.deltaTime;
            seconds4 = (int)danmaku_totalTime;

            if (seconds4 > 60)
            {
                int hour4 = (int)seconds4 / 3600;
                int minute4 = (int)seconds4 / 60 - (hour4 * 60);
                danmaku_timerText.text = hour4.ToString("00") + ":" + minute4.ToString("00");
            }
            else
            {
                danmaku_timerText.text = seconds4.ToString();
            }

            if (seconds4 <= 0)
            {
                isActive4 = false;
                danmaku_totalTime = danmaku_firstTime;
                seconds4 = (int)danmaku_totalTime;
                int hour4 = (int)seconds4 / 3600;
                int minute4 = (int)seconds4 / 60 - (hour4 * 60);
                danmaku_timerText.text = "12:00"; //hour4.ToString("00") + ":" + minute4.ToString("00");
                //GameManager.GetComponent<GohanPlus>().danmaku_plus();
            }
        }
    }
    */
}