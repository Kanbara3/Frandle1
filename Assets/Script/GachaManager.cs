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
    public Button ticketBuyButton;
    public TextMeshProUGUI ticketPriceText;
    public TextMeshProUGUI ticketNumText;
    private int buyCount = 0; //�����Ƀ��Z�b�g
    private int ticketCount = 0; //�`�P�b�g�̏�����
    private int ticketPrice = 100;
    private List<int> ticketPriceList = new List<int> {500, 1000, 5000, 10000};

    // Start is called before the first frame update
    void Start()
    {
        LoadTicketNum();
    }

    // Update is called once per frame
    void Update()
    {
        SaveTicketNum();
    }

    //turnButton���������ۂ̏���
    public void GachaButton()
    {
        if (ticketCount > 0)
        {
            ticketCount--;
            ticketNumText.text = ticketCount.ToString();
            //visitorManager.GetComponent<VisitorManager>().ApplyVisitorLevelBenefit();
            Visitor[] visitors = visitorManager.visitorContent.GetComponentsInChildren<Visitor>();
            int randomNumber = UnityEngine.Random.Range(1, visitors.Length + 1);

            foreach (Visitor visitor in visitors)
            {
                if (visitor.id == randomNumber)
                {
                    visitor.level++;
                    visitor.InitVisitor(visitor.id.ToString(), visitor.name);
                    visitor.LevelTextUpdate();
                    visitorManager.GetComponent<VisitorManager>().ApplyVisitorLevelBenefit(visitor.id, visitor.level);
                    break;
                }
            }
        }
    }

    //turnButton10(10�A�K�`��)
    public void GachaButton10()
    {
        if (ticketCount >= 10)
        {
            ticketCount -= 10;
            ticketNumText.text = ticketCount.ToString();
            //visitorManager.GetComponent<VisitorManager>().ApplyVisitorLevelBenefit();
            Visitor[] visitors = visitorManager.visitorContent.GetComponentsInChildren<Visitor>();
            for(int i = 0; i < 10; i++)
            {
                int randomNumber = UnityEngine.Random.Range(1, visitors.Length + 1);
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
        }
    }

    void SaveTicketNum()
    {
        PlayerPrefs.SetInt("ticketNum", ticketCount);
    }

    void LoadTicketNum()
    {
        ticketCount = PlayerPrefs.GetInt("ticketNum", 0);
        ticketPriceText.text = "�K��`�P�b�g�P���^" + ticketPrice.ToString() + "�~";
        ticketNumText.text = ticketCount.ToString();
    }

    //���������֐�
    public void buyGachaTicket()
    {
        moneyManager.Pay(ticketPrice);
    }

    // ticketBuyButton���������Ƃ��Ɏ��s����֐�
    public void IncrementTicketPrice()
    {
        buyGachaTicket();
        if (buyCount >= ticketPriceList.Count)
        {
            ticketPrice = ticketPriceList[ticketPriceList.Count-1];
            buyCount++;
            ticketCount++;
            ticketNumText.text = ticketCount.ToString();
        }
        else
        {
            ticketPrice = ticketPriceList[buyCount];
            ticketPriceText.text = "�K��`�P�b�g�P���^"+ticketPrice.ToString()+"�~";
            buyCount++;
            ticketCount++;
            ticketNumText.text = ticketCount.ToString();
        }
    }
    


}
