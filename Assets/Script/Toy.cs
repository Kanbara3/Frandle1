using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Toy : MonoBehaviour
{
    public int toyId;

    // �I�u�W�F�N�g�Q��
    private Button timeButton;
    private TextMeshProUGUI timerText;

    private int currentTime; //���ݎ���
    public int timeToPlay;  //��������

    bool isActive = false; // �^�C�}�[�t���O

    private FoodManager foodManager;
    private MoneyManager moneyManager;
    private FrandleManager frandleManager;

    private DateTime lastTapTime; //�Ō�ɉ���������
    private TimeSpan elapsedTime; //�o�ߎ���

    private long HeartIncreaseAmount = 1; // �D���x�𑝂₷��

    void Start()
    {
        // �{�^���������ă^�C�}�[�J�n
        timeButton.onClick.AddListener(() =>
        {
            isActive = true;
            currentTime = timeToPlay;
            lastTapTime = DateTime.UtcNow;
            AdjustMoneyIncrease();
            timeButton.interactable = false; //�{�^��������
        });

        LoadTimerFunction();
    }

    void Update()
    {
        // �^�C�}�[
        if (!isActive) { return; }

        currentTime = timeToPlay - (int)elapsedTime.TotalSeconds;
        //Debug.Log(elapsedTime.TotalSeconds);
        if (currentTime > 0)
        {
            elapsedTime = DateTime.UtcNow - lastTapTime;
            IncreaseHeartsAutomatically();
        }
        else
        {

            currentTime = timeToPlay; //�b�����Z�b�g
            elapsedTime = TimeSpan.FromSeconds(0); //�o�ߎ��ԃ��Z�b�g
            AdjustMoneyIncreaseDownward();
            timeButton.interactable = true; //�{�^���L����
            isActive = false;
        }
        DisplayTimerText();
        SaveTimerFunction();
        
    }

    // �^�C�}�[�\���֐�
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

    // Ehon��Doll�̌���
    private float timer = 0f;
    private float interval = 1f;
    void IncreaseHeartsAutomatically()
    {
        timer += Time.deltaTime;
        if(timer >= interval)
        {
            if (this.name == "Ehon") { frandleManager.tap += HeartIncreaseAmount; }
            if (this.name == "Doll") { frandleManager.tap += (long)(HeartIncreaseAmount*1.2); }
            frandleManager.HeartTextUpdate();
            frandleManager.UpdateSliderValue();
            timer = 0f;
        }
    }

    // Knife��Bonb�̌���
    // ��ނɂ��moneyIncrease�{�����グ��
    void AdjustMoneyIncrease()
    {
        if(this.name == "Knife") { moneyManager.moneyIncreaseBoost *= 2f; }
        if(this.name == "Bomb") { moneyManager.moneyIncreaseBoost *= 1.5f; }
    }
    // �{����������
    void AdjustMoneyIncreaseDownward()
    {
        if (this.name == "Knife") { moneyManager.moneyIncreaseBoost /= 2f; }
        if (this.name == "Bomb") { moneyManager.moneyIncreaseBoost /= 1.5f; }
    }


    // �Z�[�u
    public void SaveTimerFunction()
    {
        PlayerPrefs.SetString("lastTapTimeKey" + toyId, lastTapTime.ToString());
        PlayerPrefs.SetString("isActiveKey" + toyId, isActive.ToString());
}

    // ���[�h
    public void LoadTimerFunction()
    {
        string time = PlayerPrefs.GetString("lastTapTimeKey" + toyId, "");
        lastTapTime = DateTime.Parse(time);
        string flag = PlayerPrefs.GetString("isActiveKey" + toyId, "");
        isActive = Convert.ToBoolean(flag);
        if (isActive == true ) { timeButton.interactable = false; }
    }
    
    

    // �I�u�W�F�N�g�擾
    void Awake()
    {
        timeButton = this.transform.GetChild(2).GetComponent<Button>();
        timerText = this.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        foodManager = GameObject.Find("FoodManager").GetComponent<FoodManager>();
        moneyManager = GameObject.Find("MoneyManager").GetComponent<MoneyManager>();
        frandleManager = GameObject.Find("Frandle").GetComponent<FrandleManager>();
    }
}
