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
}

public class FoodManager : MonoBehaviour
{
    public GameObject foodPrefab;
    public Canvas foodCanvas;
    public GameObject foodContent;
    private List<GameObject> foodList = new List<GameObject>();

    private JsonData jsonData;

    // Start is called before the first frame update
    void Start()
    {
        readJson();
        

        foreach (var item in jsonData.foodInfos)
        {
            //int p = i * 200;
            GameObject newFood = Instantiate(foodPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            newFood.transform.SetParent(foodContent.transform, false);
            newFood.GetComponent<Food>().InitFood("1-"+(item.id), long.Parse(item.like));
            foodList.Add(newFood);
        }

        addFoodStock(0, 1);
        LoadNumFoodFunction();
    }

    void Update()
    {
        SaveNumFoodFunction();
    }

    // foodの個数を増やす Toy.csで使用
    public void addFoodStock(int foodId, int ct)
    {
        foodList[foodId].GetComponent<Food>().AddStock(ct);
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

    void readJson()
    {
        //Resourcesからdocument.jsonを読み込み、string型にキャスト
        string json = Resources.Load<TextAsset>("foodJson").ToString();

        // jsonDataの初期化
        jsonData = JsonUtility.FromJson<JsonData>(json);
    }
}
