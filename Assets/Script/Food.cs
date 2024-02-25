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

    // ごはんの数制御
    private string foodName;
    // ごはんの個数のtext
    private TextMeshProUGUI foodText;
    // ごはんの個数のint
    public int numFood=0;
    // ご飯の数の抑制
    //public int limitFoodNum = 5;
    // ごはんを与えるButton
    private Button foodButton;
    // ごはんを与えた時のtapした時の好感度増幅量
    public long tapFood = 1;
    // foodの画像
    private Image foodImage;
    
    // 満腹度上昇値
    private int satietyIncreaseRate = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ボタンを押したときにマイナス1する
    public void GiveButton()
    {
        foodButton.onClick.AddListener(() =>
        {
            if (numFood == 0) return;
            numFood -= 1;
            //frandleManager.changeOneTapIncreaseRate(tapFood);
            frandleManager.upFavourableImpression(tapFood);
            frandleManager.SatietyIncreaseRate(satietyIncreaseRate); //満腹度を増加
            FoodRecord();
            FoodTextUpdate();
            foodManager.CaluculateFoodStockSum();
            frandleManager.HeartTextUpdate();
        });
    }

    //Asset>Resources>FoodImageフォルダから画像を読み込み
    public void InitFood(string imagePath, long tapFood)
    {
        this.tapFood = tapFood;
        foodImage.sprite = Resources.Load<Sprite>("FoodImage/"+imagePath);
    }
    
    // 食べ物の数を増やす関数
    public void AddStock(int ct)
    {
        if (foodManager.sumFoodNum < foodManager.limitFoodNum) //limitFoodNumより少なかったら増やす
        {
            numFood += ct;
            FoodTextUpdate();
        }
    }

    // foodTextの更新
    public void FoodTextUpdate()
    {
        foodText.text = "×" + " " + numFood.ToString();
    }

    public bool hasEaten = false;

    // 押したものの記録
    public void FoodRecord()
    {
        hasEaten = true;
    }
    // haEatenをリセットする関数
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
