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
    private int buyCount = 0; //翌日にリセット
    private int ticketCount = 0; //チケットの所持数
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

    //turnButtonを押した際の処理
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

    //turnButton10(10連ガチャ)
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
        ticketPriceText.text = "訪問チケット１枚／" + ticketPrice.ToString() + "円";
        ticketNumText.text = ticketCount.ToString();
    }

    //金を消費する関数
    public void buyGachaTicket()
    {
        moneyManager.Pay(ticketPrice);
    }

    // ticketBuyButtonを押したときに実行する関数
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
            ticketPriceText.text = "訪問チケット１枚／"+ticketPrice.ToString()+"円";
            buyCount++;
            ticketCount++;
            ticketNumText.text = ticketCount.ToString();
        }
    }
    


}
