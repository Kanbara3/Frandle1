using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GachaManager : MonoBehaviour
{
    public VisitorManager visitorManager;
    public MoneyManager moneyManager;
    private GameObject visitorContent;
    public Button turnButton;

    // Start is called before the first frame update
    void Start()
    {
        visitorContent = visitorManager.visitorContent;
        GachaButtonPush();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //�{�^�����������ۂ̏���
    public void GachaButton()
    {
        buyGachaTicket();

        Visitor[] visitors = visitorContent.GetComponentsInChildren<Visitor>();
        int randomNumber = UnityEngine.Random.Range(1, visitors.Length+1);

        foreach (Visitor visitor in visitors)
        {
            if (visitor.id == randomNumber) // id��1��Visitor����������
            {
                // ������Level���X�V���鏈������������
                visitor.level++; // ��Ƃ��āALevel��1���₷

                // Level���X�V������AVisitor.cs��InitVisitor���\�b�h���Ăяo���čX�V�𔽉f����
                visitor.LevelTextUpdate();
                break;
            }
        }
    }

    //���������֐�
    public void buyGachaTicket()
    {
        moneyManager.Pay(1000);
        moneyManager.UpdateMoney();
    }

    //�{�^���������֐�
    public void GachaButtonPush()
    {
        turnButton.onClick.AddListener(() =>
        {
            GachaButton();
        });
    }
    


}
