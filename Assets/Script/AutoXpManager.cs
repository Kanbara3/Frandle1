using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AutoXpManager : MonoBehaviour
{
    FrandleManager frandleManager;

    private float currentTimer; // 1�b�𑪂�ϐ�
    private float increaseXpSpan = 1f; // ���b���Ƃɑ��₷��
    private int lastAddXpTime; // �Ō�Ɏ�����XP���オ��������
    private int timeIncreaseXp; // ��~���ɑ�����͂�������XPor1�b�ő�����XP
    private bool autoXpGain = false; // �{�^�������������ۂ�

    void Start()
    {
        if(lastAddXpTime == 0)
        {
            lastAddXpTime = (int)(DateTime.UtcNow.Subtract(new DateTime(1979, 1, 1))).TotalSeconds;
        }
    }

    void Update()
    {
        if (autoXpGain)
        {
            IncreaseXp();
        }
        IncreaseXPOverTime();
    }

    // increaseXpSpan�b���Ƃ�XP��timeIncreaseXp���₷
    private void IncreaseXPOverTime()
    {
        currentTimer += Time.deltaTime;
        if (currentTimer >= increaseXpSpan)
        {
            frandleManager.GainXP(timeIncreaseXp);
            lastAddXpTime = (int)(DateTime.UtcNow.Subtract(new DateTime(1979, 1, 1))).TotalSeconds; //�Ŋ���XP������������
            timeIncreaseXp = 0;
            currentTimer = 0f;
        }
    }

    // Xp�𑝂₷�֐�
    private void IncreaseXp()
    {
        int currentTime;
        currentTime = (int)(DateTime.UtcNow.Subtract(new DateTime(1979, 1, 1))).TotalSeconds;
        timeIncreaseXp = (int)(currentTime - lastAddXpTime) * 1;
        getTimeToPlay();
        timeIncreaseXp = Mathf.Min(timeIncreaseXp, 10); //�o�ߎ��Ԃƌo�߉\���Ԃ��ׂď������������
        //Debug.Log(currentTime + "-" + lastAddXpTime);
        IncreaseXPOverTime();
    }

    // ��������̎��Ԃ̎擾
    private int timeToPlay = 0;
    private int elapsedTime = 0;
    private int timeLeft = 0;
    private Toy toy;
    public void SetToy(Toy toy)
    {
        this.toy = toy;
    }
    private void getTimeToPlay()
    {
        elapsedTime = (int)toy.elapsedTime.TotalSeconds;
        timeLeft = timeToPlay - elapsedTime;
        Debug.Log(timeLeft);
    }

    // timeIncreaseXp

    public void AutoXpGainOn(int timeToPlay)
    {
        timeIncreaseXp = 0;
        this.timeToPlay = timeToPlay;
        autoXpGain = true;
    }

    public void AutoXpGainOff()
    {
        
        autoXpGain = false;
    }

    private void Awake()
    {
        frandleManager = GameObject.Find("Frandle").GetComponent<FrandleManager>();
    }

    public void SaveAutoXpManager()
    {
        PlayerPrefs.SetInt("lastAddXpTime"+this.name, lastAddXpTime);
        PlayerPrefs.SetInt("autoXp"+this.name, autoXpGain ? 1 : 0);
        PlayerPrefs.SetInt("timeLeft" + this.name, timeLeft);
    }
    public void LoadAutoXpManager()
    {
        lastAddXpTime = PlayerPrefs.GetInt("lastAddXpTime"+this.name, (int)(DateTime.UtcNow.Subtract(new DateTime(1979, 1, 1))).TotalSeconds);
        autoXpGain = PlayerPrefs.GetInt("autoXp"+this.name, 0) == 1;
        timeLeft = PlayerPrefs.GetInt("timeLeft" + this.name, 0);
    }
}
