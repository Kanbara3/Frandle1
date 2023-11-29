using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Food : MonoBehaviour
{
    private FrandleManager frandleManager;

    // ごはんの数制御
    private string foodName;
    // ごはんの個数のtext
    private TextMeshProUGUI foodText;
    // ごはんの個数のint
    public int numFood=0;
    // ごはんを与えるButton
    private Button foodButton;
    // ごはんを与えた時のtapした時の好感度増幅量
    public long tapFood = 1;

    private Image foodImage;

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
            Debug.Log(tapFood);
            //frandleManager.changeOneTapIncreaseRate(tapFood);
            frandleManager.upFavourableImpression(tapFood);
            FoodTextUpdate();
            frandleManager.HeartTextUpdate();
        });
    }

    //Asset>Resources>FoodImageフォルダから画像を読み込み
    public void InitFood(string imagePath, long tapFood)
    {
        this.tapFood = tapFood;
        foodImage.sprite = Resources.Load<Sprite>("FoodImage/"+imagePath);
            //GameObject.Find("test").transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
            //Resources.Load<Sprite>("ImageFoodImage/1-2");
        
    }
    
    public void AddStock(int ct)
    {
        numFood += ct;
        FoodTextUpdate();
    }

    // foodTextの更新
    public void FoodTextUpdate()
    {
        foodText.text = "×" + " " + numFood.ToString();
    }

    // Start is called before the first frame update
    void Awake()
    {
        frandleManager = GameObject.Find("Frandle").GetComponent<FrandleManager>();
        foodButton = this.transform.GetChild(0).GetComponent<Button>();
        foodText = this.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        GiveButton();
        foodImage = this.transform.GetChild(0).GetComponent<Image>();
    }

    
}
