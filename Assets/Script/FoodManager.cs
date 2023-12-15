using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public GameObject foodPrefab;
    public Canvas foodCanvas;
    public GameObject foodContent;
    private List<GameObject> foodList = new List<GameObject>();
    private List<int> cuisineTypeList = new List<int>();
    private List<int> mealTymeList = new List<int> ();
    private List<bool> hasEatenList = new List<bool>();

    private JsonData jsonData;

    // Start is called before the first frame update
    void Start()
    {
        readJson();
        
        // jsonから読み込み
        foreach (var item in jsonData.foodInfos)
        {
            //int p = i * 200;
            GameObject newFood = Instantiate(foodPrefab, new Vector3(0, 0, 0), Quaternion.identity); //Prefabからコピーを作成
            newFood.transform.SetParent(foodContent.transform, false);
            newFood.GetComponent<Food>().InitFood("1-"+(item.id), long.Parse(item.like)); //フォルダから画像読み込みする関数を実行
            foodList.Add(newFood); //Prefabオブジェクトのリスト作成
            cuisineTypeList.Add(item.mealTime);
            mealTymeList.Add(item.mealTime);

        }

        //addFoodStock(0, 1);
        LoadNumFoodFunction();
        RecordSelectedFood();
    }

    void Update()
    {
        SaveNumFoodFunction();
    }

    // foodの個数を増やす Toy.csで使用
    public void addFoodStock(int foodId, int ct)
    {
        foodList[foodId-1].GetComponent<Food>().AddStock(ct);
    }

    // どのfoodを押したか記録
    public void RecordSelectedFood()
    {
        for (int i=0; i < foodList.Count; i++)
        {
            bool hasEaten = foodList[i].GetComponent<Food>().hasEaten;
            hasEatenList.Add(hasEaten);
        }
    }

    //
    //public void CheckCuisineTypeList()
    //{
    //    for (int i=0; i<foodList.Count; i++)
    //    {
    //        for (int j=1; j<5; i++)
    //        {
    //            cuisineTypeList[j]
    //        }
    //    }
    //}

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

    void readJson()
    {
        //Resourcesからdocument.jsonを読み込み、string型にキャスト
        string json = Resources.Load<TextAsset>("foodJson").ToString();

        // jsonDataの初期化
        jsonData = JsonUtility.FromJson<JsonData>(json);
    }
}
