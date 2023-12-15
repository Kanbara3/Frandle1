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
    public float span = 1f;
    private float currentTime = 0f;
    private DateTime stoppedTime;
    private TimeSpan elapsedTime; //��~���Ɍo�߂�������

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

    //�Q�[���I�����Ɏ������擾
    void OnApplicationQuit()
    {
        stoppedTime = DateTime.UtcNow;
    }

    // �Q�[���ꎞ��~���Ɏ������擾,�Q�[���ꎞ��~���A�Ɋ֐����s
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

    // stoppedTime�̃Z�[�u
    public void SaveStoppedTimeFunction()
    {
        PlayerPrefs.SetString("stoppedTimeKey", stoppedTime.ToString());
    }

    // stoppedTime�̃��[�h
    public void LoadStoppedTimeFunction()
    {
        string strStoppTime = PlayerPrefs.GetString("stoppedTimeKey", "");
        stoppedTime = DateTime.Parse(strStoppTime);
        elapsedTime = DateTime.UtcNow - stoppedTime;
        IncreaseMoneyForPauseOrStopTimeElapsed();
        elapsedTime = TimeSpan.FromSeconds(0);
    }

    // ��~���Ɍo�߂������ԕ�money�𑝉�
    public void IncreaseMoneyForPauseOrStopTimeElapsed()
    {
        money += moneyIncrease * (int)elapsedTime.TotalSeconds;
    }

    //1�b���Ƃ�money��������
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

    //money�̃Z�[�u
    public void SaveMoneyFunction()
    {
        PlayerPrefs.SetInt("moneyData", money);
    }

    //money�̃��[�h
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
