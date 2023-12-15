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
        
        // json����ǂݍ���
        foreach (var item in jsonData.foodInfos)
        {
            //int p = i * 200;
            GameObject newFood = Instantiate(foodPrefab, new Vector3(0, 0, 0), Quaternion.identity); //Prefab����R�s�[���쐬
            newFood.transform.SetParent(foodContent.transform, false);
            newFood.GetComponent<Food>().InitFood("1-"+(item.id), long.Parse(item.like)); //�t�H���_����摜�ǂݍ��݂���֐������s
            foodList.Add(newFood); //Prefab�I�u�W�F�N�g�̃��X�g�쐬
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

    // food�̌��𑝂₷ Toy.cs�Ŏg�p
    public void addFoodStock(int foodId, int ct)
    {
        foodList[foodId-1].GetComponent<Food>().AddStock(ct);
    }

    // �ǂ�food�����������L�^
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

    // numFood�̃Z�[�u
    public void SaveNumFoodFunction()
    {
        for (int i = 0; i < jsonData.foodInfos.Length; i++)
        {
            int numFood = foodList[i].GetComponent<Food>().numFood;
            PlayerPrefs.SetInt("saveNumFood_" + i, numFood);
        }
    }

    // numFood�̃��[�h
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
        //Resources����document.json��ǂݍ��݁Astring�^�ɃL���X�g
        string json = Resources.Load<TextAsset>("foodJson").ToString();

        // jsonData�̏�����
        jsonData = JsonUtility.FromJson<JsonData>(json);
    }
}
