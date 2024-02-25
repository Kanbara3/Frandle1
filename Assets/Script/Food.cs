using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Food : MonoBehaviour
{
    private FrandleManager frandleManager;
    private FoodManager foodManager;
    private FoodShop foodShop;

    // ���͂�̐�����
    private string foodName;
    // ���͂�̌���text
    private TextMeshProUGUI foodText;
    // ���͂�̌���int
    public int numFood=0;
    // ���т̐��̗}��
    //public int limitFoodNum = 5;
    // ���͂��^����Button
    private Button foodButton;
    // ���͂��^��������tap�������̍D���x������
    public long tapFood = 1;
    // food�̉摜
    private Image foodImage;
    
    // �����x�㏸�l
    private int satietyIncreaseRate = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // �{�^�����������Ƃ��Ƀ}�C�i�X1����
    public void GiveButton()
    {
        foodButton.onClick.AddListener(() =>
        {
            if (numFood == 0) return;
            numFood -= 1;
            //frandleManager.changeOneTapIncreaseRate(tapFood);
            frandleManager.upFavourableImpression(tapFood);
            frandleManager.SatietyIncreaseRate(satietyIncreaseRate); //�����x�𑝉�
            FoodRecord();
            FoodTextUpdate();
            foodManager.CaluculateFoodStockSum();
            frandleManager.HeartTextUpdate();
        });
    }

    //Asset>Resources>FoodImage�t�H���_����摜��ǂݍ���
    public void InitFood(string imagePath, long tapFood)
    {
        this.tapFood = tapFood;
        foodImage.sprite = Resources.Load<Sprite>("FoodImage/"+imagePath);
    }
    
    // �H�ו��̐��𑝂₷�֐�
    public void AddStock(int ct)
    {
        if (foodManager.sumFoodNum < foodManager.limitFoodNum) //limitFoodNum��菭�Ȃ������瑝�₷
        {
            numFood += ct;
            FoodTextUpdate();
        }
    }

    // foodText�̍X�V
    public void FoodTextUpdate()
    {
        foodText.text = "�~" + " " + numFood.ToString();
    }

    public bool hasEaten = false;

    // ���������̂̋L�^
    public void FoodRecord()
    {
        hasEaten = true;
    }
    // haEaten�����Z�b�g����֐�
    public void ResetHasEaten()
    {
        hasEaten = false;
    }

    // Start is called before the first frame update
    void Awake()
    {
        frandleManager = GameObject.Find("Frandle").GetComponent<FrandleManager>();
        foodManager = GameObject.Find("FoodManager").GetComponent<FoodManager>();
        foodShop = GameObject.Find("FoodShop").GetComponent<FoodShop>();
        foodButton = this.transform.GetChild(0).GetComponent<Button>();
        foodText = this.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        GiveButton();
        foodImage = this.transform.GetChild(0).GetComponent<Image>();
    }

    
}
