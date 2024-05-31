using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using static Unity.VisualScripting.Antlr3.Runtime.Tree.TreeWizard;

public class GachaManager : MonoBehaviour
{
    public VisitorManager visitorManager;
    public MoneyManager moneyManager;
    public GachaAnimationManager animManager;
    public Button turnButton;
    public Button ticketBuyButton;
    public TextMeshProUGUI ticketPriceText;
    public TextMeshProUGUI ticketNumText;
    private int buyCount = 0; //�����Ƀ��Z�b�g
    public int ticketCount = 0; //�`�P�b�g�̏�����
    private int ticketPrice = 100;
    private List<int> ticketPriceList = new List<int> { 500, 1000, 5000, 10000, 20000, 40000, 60000 };

    public GameObject gachaResultPanel;
    public GameObject gachaResultPanel10;
    public GameObject newTextPrefab;
    public GameObject newTextPrefab10;


    // Start is called before the first frame update
    void Start()
    {
        LoadTicketNum();

    }

    // Update is called once per frame
    void Update()
    {
        SaveTicketNum();
        if (Input.GetMouseButtonDown(0))
        {
            gachaResultPanel.SetActive(false);
            animManager.CloseGachaAnimationObject();
            Destroy(newTextObject);
        }
        if (Input.GetMouseButtonDown(0))
        {
            gachaResultPanel10.SetActive(false);
            foreach (GameObject newTextObject in newTextObjects)
            {
                Destroy(newTextObject);
            }
        }
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
                    visitor.GachaLevelUp();
                    GachaEffect(randomNumber, visitor.virtualLevel);
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
            ticketNumText.text = ticketCount.ToString() + "��";
            //visitorManager.GetComponent<VisitorManager>().ApplyVisitorLevelBenefit();
            Visitor[] visitors = visitorManager.visitorContent.GetComponentsInChildren<Visitor>();
            for (int i = 0; i < 10; i++)
            {
                int randomNumber = UnityEngine.Random.Range(1, visitors.Length + 1);
                foreach (Visitor visitor in visitors)
                {
                    if (visitor.id == randomNumber)
                    {
                        visitor.GachaLevelUp();
                        GachaEffect10(i, randomNumber, visitor.virtualLevel);
                        break;
                    }
                }
            }
        }
    }

    public void SaveTicketNum()
    {
        PlayerPrefs.SetInt("ticketNum", ticketCount);
    }

    public void LoadTicketNum()
    {
        ticketCount = PlayerPrefs.GetInt("ticketNum", 0);
        ticketPriceText.text = "�K��`�P�b�g�P���^" + ticketPrice.ToString() + "�~";
        ticketNumText.text = ticketCount.ToString() + "��";
    }

    //���������֐�
    public void buyGachaTicket()
    {
        moneyManager.Pay(ticketPrice);
    }

    // ticketBuyButton���������Ƃ��Ɏ��s����֐�
    public void IncrementTicketPrice()
    {
        if (ticketPrice < moneyManager.money)
        {
            buyGachaTicket();
            if (buyCount >= ticketPriceList.Count)
            {
                ticketPrice = ticketPriceList[ticketPriceList.Count - 1];
                ApplyBenefitId8();
                buyCount++;
                ticketCount++;
                ticketNumText.text = ticketCount.ToString() + "��";
            }
            else
            {
                ticketPrice = ticketPriceList[buyCount];
                ApplyBenefitId8();
                ticketPriceText.text = "�K��`�P�b�g�P���^" + ticketPrice.ToString() + "�~";
                buyCount++;
                ticketCount++;
                ticketNumText.text = ticketCount.ToString() + "��";
            }
        }
    }

    // �`�P�b�g���������
    public void DiscountTicketPrice(int level)
    {
        float discountPrice_f = ticketPrice * ((0.764f * (float)level - 0.664f) * 0.0001f);
        int discountPrice = Mathf.CeilToInt(discountPrice_f);
        ticketPrice -= discountPrice;
    }

    private void ApplyBenefitId8()
    {
        Visitor id8Visitor = visitorManager.visitorList[7].GetComponent<Visitor>();
        id8Visitor.ChatchFrandleLevelUped();
    }

    // 1�A�K�`�������������̉��o
    GameObject newTextObject;
    void GachaEffect(int randomNumber, int visitorLevel)
    {
        animManager.PlayGachaAnimation(); //�A�j���[�V����
        gachaResultPanel.SetActive(true); 
        Transform childVisitorTransform = gachaResultPanel.transform.GetChild(0);
        Transform childLevelTransform = gachaResultPanel.transform.GetChild(1);
        Image visitor = childVisitorTransform.GetComponent<Image>();
        TextMeshProUGUI level = childLevelTransform.GetComponent<TextMeshProUGUI>();
        visitor.sprite = Resources.Load<Sprite>("VisitorImage/" + randomNumber);
        level.text = "Lv." + visitorLevel.ToString();
        if (visitorLevel == 1)
        {
            newTextObject = Instantiate(newTextPrefab, new Vector3(71.9f, 95.8f, 0f), Quaternion.identity);
            newTextObject.transform.SetParent(gachaResultPanel.transform, false);
        }
    }

    // 10�A�K�`�������������̉��o
    GameObject newTextObject10;
    private List<GameObject> newTextObjects = new List<GameObject>();
    void GachaEffect10(int i, int randomNumber, int visitorLevel)
    {
        animManager.PlayGachaAnimation(); //�A�j���[�V����
        gachaResultPanel10.SetActive(true);
        Transform childVisitorTransform = gachaResultPanel10.transform.GetChild(i);
        Transform childLevelTransform = childVisitorTransform.transform.GetChild(0);
        Image visitor = childVisitorTransform.GetComponent<Image>();
        TextMeshProUGUI level = childLevelTransform.GetComponent<TextMeshProUGUI>();
        visitor.sprite = Resources.Load<Sprite>("VisitorImage/" + randomNumber);
        level.text = "Lv." + visitorLevel.ToString();
        if (visitorLevel == 1)
        {
            newTextObject10 = Instantiate(newTextPrefab10, new Vector3(40f, 60f, 0f), Quaternion.identity);
            newTextObject10.transform.SetParent(visitor.transform, false);
            newTextObjects.Add(newTextObject10);
        }
        Debug.Log(visitorLevel);
    }
}
