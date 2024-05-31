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
    private long XP = 0;

    private int currentTime; //���ݎ���
    public int timeToPlay;  //��������
    public int price; //�l�i

    bool isActive = false; // �^�C�}�[�t���O

    private FoodManager foodManager;
    private MoneyManager moneyManager;
    private FrandleManager frandleManager;
    private AutoXpManager autoXpManager;

    private DateTime lastTapTime; //�Ō�ɉ���������
    public TimeSpan elapsedTime; //�o�ߎ���

    public long HeartIncreaseAmount = 1; // �D���x�𑝂₷��
    public float MoneyIncreaseAmount = 1; //�������u�[�X�g

    void Start()
    {
        //ContinueToyWhileClosed();
        // �{�^���������ă^�C�}�[�J�n
        timeButton.onClick.AddListener(() =>
        {
            if (price < moneyManager.money)
            {
                isActive = true;
                currentTime = timeToPlay;
                lastTapTime = DateTime.UtcNow;
                AdjustMoneyIncrease();
                moneyManager.Pay(price);
                timeButton.interactable = false; //�{�^��������
                if (this.name == "Ehon" || this.name == "Doll")
                {
                    autoXpManager.AutoXpGainOn(timeToPlay);
                }
            }
        });

    }

    void Update()
    {
        // �^�C�}�[
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
            currentTime = timeToPlay; //�b�����Z�b�g
            elapsedTime = TimeSpan.FromSeconds(0); //�o�ߎ��ԃ��Z�b�g
            AdjustMoneyIncreaseDownward();
            timeButton.interactable = true; //�{�^���L����
            isActive = false;
            if (this.name == "Ehon" || this.name == "Doll")
            {
                autoXpManager.AutoXpGainOff();
            }
        }
        DisplayTimerText();
        
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

    // Knife��Bonb�̌���
    // ��ނɂ��moneyIncrease�{�����グ��
    void AdjustMoneyIncrease()
    {
        if(this.name == "Knife") { moneyManager.moneyIncreaseBoost *= MoneyIncreaseAmount*1.5f; }
        if(this.name == "Bomb") { moneyManager.moneyIncreaseBoost *= MoneyIncreaseAmount*2f; }
    }
    // �{����������
    void AdjustMoneyIncreaseDownward()
    {
        if (this.name == "Knife") { moneyManager.moneyIncreaseBoost /= MoneyIncreaseAmount * 1.5f; }
        if (this.name == "Bomb") { moneyManager.moneyIncreaseBoost /= MoneyIncreaseAmount * 2f; }
    }

    // �Z�[�u
    public void SaveTimerFunction()
    {
        PlayerPrefs.SetString("lastTapTimeKey" + toyId, lastTapTime.ToString());
        PlayerPrefs.SetString("isActiveKey" + toyId, isActive.ToString());
        //PlayerPrefs.SetString("PauseTime" + toyId, pauseTime.ToString());
    }

    // ���[�h
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
    
    

    // �I�u�W�F�N�g�擾
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
