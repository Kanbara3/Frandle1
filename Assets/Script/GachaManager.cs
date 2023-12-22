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
    public Button turnButton;

    // Start is called before the first frame update
    void Start()
    {
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

        Visitor[] visitors = visitorManager.visitorContent.GetComponentsInChildren<Visitor>();
        int randomNumber = UnityEngine.Random.Range(1, visitors.Length+1);

        foreach (Visitor visitor in visitors)
        {
            if (visitor.id == randomNumber)
            {
                visitor.level++;
                visitor.InitVisitor(visitor.id.ToString(), visitor.name);
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
