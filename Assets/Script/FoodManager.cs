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


    private JsonData jsonData;

    // Start is called before the first frame update
    void Start()
    {
        readJson();


        foreach (var item in jsonData.foodInfos)
        {
            //int p = i * 200;
            Debug.Log(item.id);
            GameObject newFood = Instantiate(foodPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            newFood.transform.SetParent(foodContent.transform, false);
            newFood.GetComponent<Food>().InitFood("1-"+(item.id), long.Parse(item.like));
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
