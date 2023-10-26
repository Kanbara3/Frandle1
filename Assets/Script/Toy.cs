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

    bool isActive = false; // �^�C�}�[�t���O

    private FoodManager foodManager;

    private DateTime lastTapTime; //�Ō�ɉ���������
    private TimeSpan elapsedTime; //�o�ߎ���

    void Start()
    {
        // �{�^���������ă^�C�}�[�J�n
        timeButton.onClick.AddListener(() =>
        {
            isActive = true;
            currentTime = timeToPlay;
            lastTapTime = DateTime.UtcNow;
            timeButton.interactable = false; //�{�^��������
        });

        
    }

    void Update()
    {
        // �^�C�}�[
        if (!isActive) { return; }

        currentTime = timeToPlay - (int)elapsedTime.TotalSeconds;
        if (currentTime > 0)
        {
            elapsedTime = DateTime.UtcNow - lastTapTime;
            
        }
        else
        {
            currentTime = timeToPlay; //�b�����Z�b�g
            elapsedTime = TimeSpan.FromSeconds(0); //�o�ߎ��ԃ��Z�b�g
            foodManager.addFoodStock(0, 1);
            timeButton.interactable = true; //�{�^���L����
            isActive = false;
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

    // �I�u�W�F�N�g�擾
    void Awake()
    {
        timeButton = this.transform.GetChild(2).GetComponent<Button>();
        timerText = this.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        foodManager = GameObject.Find("FoodManager").GetComponent<FoodManager>();
    }
}
