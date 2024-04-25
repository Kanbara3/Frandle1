using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Unity.VisualScripting.Antlr3.Runtime.Tree.TreeWizard;

[System.Serializable]
public class JsonData
{
    public FoodInfo[] foodInfos;
}
[System.Serializable]
public class FoodInfo
{
    public int id;
    public string like;
    public int mealTime;
    public int cuisineType;
    public int price;
}

public class FoodManager : MonoBehaviour
{
    private FrandleManager frandleManager;
    public GameObject foodPrefab;
    public Canvas foodCanvas;
    public GameObject foodContent;
    public GameObject foodShop;
    private bool foodShopActive;
    public List<GameObject> foodList = new List<GameObject>();
    private List<int> cuisineTypeList = new List<int>();
    private List<int> mealTypeList = new List<int>();
    private List<bool> hasEatenList = new List<bool>();
    public int sumFoodNum = 0; //総所持数
    public int limitFoodNum = 10; //個数上限
    public GameObject stockText;
    public GameObject satietyText;
    public Image desiredFoodImage;

    private JsonData jsonData;

    // Start is called before the first frame update
    void Start()
    {
        //readJson();

        //// jsonから読み込み
        //foreach (var item in jsonData.foodInfos)
        //{
        //    //int p = i * 200;
        //    GameObject newFood = Instantiate(foodPrefab, new Vector3(0, 0, 0), Quaternion.identity); //Prefabからコピーを作成
        //    newFood.transform.SetParent(foodContent.transform, false);
        //    newFood.GetComponent<Food>().InitFood("1-" + (item.id), long.Parse(item.like)); //フォルダから画像読み込みする関数を実行
        //    newFood.GetComponent<Food>().id = item.id;
        //    foodList.Add(newFood); //Prefabオブジェクトのリスト作成
        //    cuisineTypeList.Add(item.mealTime);
        //    mealTypeList.Add(item.mealTime);

        //}

        RecordSelectedFood();
        DesiredFood();
    }

    void Update()
    {
        satietyText.GetComponent<TextMeshProUGUI>().text = "満腹度：" + frandleManager.satiety.ToString() + "/" + frandleManager.MAX_SATIETY.ToString();
    }

    // foodの個数を増やす
    public void addFoodStock(int foodId, int ct)
    {
        if (limitFoodNum >= sumFoodNum)
        {
            foodList[foodId - 1].GetComponent<Food>().AddStock(ct);
            sumFoodNum += ct;
        }
    }

    // どのfoodを押したか記録
    public void RecordSelectedFood()
    {
        for (int i = 0; i < foodList.Count; i++)
        {
            bool hasEaten = foodList[i].GetComponent<Food>().hasEaten;
            hasEatenList.Add(hasEaten);
        }
    }

    //ご飯の合計数をshopを開いた時に計算
    public void CaluculateFoodStockSum()
    {
        sumFoodNum = 0;
        foreach (GameObject food in foodList)
        {
            sumFoodNum += food.GetComponent<Food>().numFood;
        }
        stockText.GetComponent<TextMeshProUGUI>().text = sumFoodNum.ToString() + "/" + limitFoodNum.ToString();
    }

    // 欲しがってるご飯を表示
    public int desiredFoodId;
    private void DesiredFood()
    {
        int randomNumber = Random.Range(1, foodList.Count + 1);
        desiredFoodId = randomNumber;
        desiredFoodImage.sprite = Resources.Load<Sprite>("FoodImage/" + "1-" + randomNumber);
    }

    // 訪問者の恩恵でごはんを与えた時の好感度増幅量(increaseXPRate)を上昇させる
    public void IncreaseXPRateIncrease(long upRate)
    {
        for (int i = 0; i < foodList.Count; i++)
        {
            foodList[i].GetComponent<Food>().increaseXPRate = upRate + foodList[i].GetComponent<Food>().increaseXPRate;
        }
    }

    // 訪問者の恩恵で冷蔵庫の拡張
    public void ExpandFoodLimit(int upRate, int initialValue)
    {
        limitFoodNum = upRate + initialValue;
    }

    // numFoodのセーブ
    public void SaveNumFoodFunction()
    {
        for (int i = 0; i < jsonData.foodInfos.Length; i++)
        {
            int numFood = foodList[i].GetComponent<Food>().numFood;
            PlayerPrefs.SetInt("saveNumFood_" + i, numFood);
        }
    }

    // numFoodのロード
    public void LoadNumFoodFunction()
    {
        for (int i = 0; i < jsonData.foodInfos.Length; i++)
        {
            int numFood = PlayerPrefs.GetInt("saveNumFood_" + i, 0);
            foodList[i].GetComponent<Food>().numFood = numFood;
            foodList[i].GetComponent<Food>().FoodTextUpdate();
        }
    }

    public int GetFoodNum(int id)
    {
        id -= 1;
        return foodList[id].GetComponent<Food>().numFood;
    }

    private void SetPrefabInformation()
    {
        readJson();

        // jsonから読み込み
        foreach (var item in jsonData.foodInfos)
        {
            //int p = i * 200;
            GameObject newFood = Instantiate(foodPrefab, new Vector3(0, 0, 0), Quaternion.identity); //Prefabからコピーを作成
            newFood.transform.SetParent(foodContent.transform, false);
            newFood.GetComponent<Food>().InitFood("1-" + (item.id), long.Parse(item.like)); //フォルダから画像読み込みする関数を実行
            newFood.GetComponent<Food>().id = item.id;
            foodList.Add(newFood); //Prefabオブジェクトのリスト作成
            cuisineTypeList.Add(item.mealTime);
            mealTypeList.Add(item.mealTime);

        }
    }

    void readJson()
    {
        //Resourcesからdocument.jsonを読み込み、string型にキャスト
        string json = Resources.Load<TextAsset>("foodJson").ToString();

        // jsonDataの初期化
        jsonData = JsonUtility.FromJson<JsonData>(json);
    }

    void Awake()
    {
        frandleManager = GameObject.Find("Frandle").GetComponent<FrandleManager>();
        SetPrefabInformation();
    }
}
