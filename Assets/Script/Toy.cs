using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Toy : MonoBehaviour
{
    // �I�u�W�F�N�g�Q��
    private Button timeButton;
    private TextMeshProUGUI timerText;

    private int currentTime; //���ݎ���
    public int timeToPlay;  //��������

    // ���ԁA���A�b
    private int hour;
    private int minute;
    private int second;

    bool isActive = false;

    private FoodManager foodManager;

    private DateTime lastTapTime; //�Ō�ɉ���������
    private TimeSpan elapsedTime; //�o�ߎ���

    void Start()
    {
        // �{�^���������ă^�C�}�[�J�n
        timeButton.onClick.AddListener(() =>
        {
            Debug.Log("�^�C�}�[�J�n");
            isActive = true;
            currentTime = timeToPlay;
            lastTapTime = DateTime.UtcNow;
            timeButton.interactable = false; //�{�^��������
        });

        
    }

    void Update()
    {
        //�o�ߎ���

        


        // �^�C�}�[
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
                
                currentTime = timeToPlay; //�b�����Z�b�g
                elapsedTime = TimeSpan.FromSeconds(0); //�o�ߎ��ԃ��Z�b�g
                hour = Mathf.FloorToInt(currentTime / 3600);
                minute = Mathf.FloorToInt((currentTime % 3600) / 60);
                second = Mathf.FloorToInt(currentTime % 60);
                timerText.text = hour.ToString("00") + ":" + minute.ToString("00") + ":" + second.ToString("00");
                foodManager.addFoodStock(0, 1);
                timeButton.interactable = true; //�{�^���L����
                isActive = false;
                Debug.Log("0�ȉ�");
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
