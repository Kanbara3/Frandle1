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
    public int sumFoodNum = 0; //��������
    public int limitFoodNum = 10; //�����
    public GameObject stockText;
    public GameObject satietyText;
    public Image desiredFoodImage;

    private JsonData jsonData;

    // Start is called before the first frame update
    void Start()
    {
        //readJson();

        //// json����ǂݍ���
        //foreach (var item in jsonData.foodInfos)
        //{
        //    //int p = i * 200;
        //    GameObject newFood = Instantiate(foodPrefab, new Vector3(0, 0, 0), Quaternion.identity); //Prefab����R�s�[���쐬
        //    newFood.transform.SetParent(foodContent.transform, false);
        //    newFood.GetComponent<Food>().InitFood("1-" + (item.id), long.Parse(item.like)); //�t�H���_����摜�ǂݍ��݂���֐������s
        //    newFood.GetComponent<Food>().id = item.id;
        //    foodList.Add(newFood); //Prefab�I�u�W�F�N�g�̃��X�g�쐬
        //    cuisineTypeList.Add(item.mealTime);
        //    mealTypeList.Add(item.mealTime);

        //}

        RecordSelectedFood();
        DesiredFood();
    }

    void Update()
    {
        satietyText.GetComponent<TextMeshProUGUI>().text = "�����x�F" + frandleManager.satiety.ToString() + "/" + frandleManager.MAX_SATIETY.ToString();
    }

    // food�̌��𑝂₷
    public void addFoodStock(int foodId, int ct)
    {
        if (limitFoodNum >= sumFoodNum)
        {
            foodList[foodId - 1].GetComponent<Food>().AddStock(ct);
            sumFoodNum += ct;
        }
    }

    // �ǂ�food�����������L�^
    public void RecordSelectedFood()
    {
        for (int i = 0; i < foodList.Count; i++)
        {
            bool hasEaten = foodList[i].GetComponent<Food>().hasEaten;
            hasEatenList.Add(hasEaten);
        }
    }

    //���т̍��v����shop���J�������Ɍv�Z
    public void CaluculateFoodStockSum()
    {
        sumFoodNum = 0;
        foreach (GameObject food in foodList)
        {
            sumFoodNum += food.GetComponent<Food>().numFood;
        }
        stockText.GetComponent<TextMeshProUGUI>().text = sumFoodNum.ToString() + "/" + limitFoodNum.ToString();
    }

    // �~�������Ă邲�т�\��
    public int desiredFoodId;
    private void DesiredFood()
    {
        int randomNumber = Random.Range(1, foodList.Count + 1);
        desiredFoodId = randomNumber;
        desiredFoodImage.sprite = Resources.Load<Sprite>("FoodImage/" + "1-" + randomNumber);
    }

    // �K��҂̉��b�ł��͂��^�������̍D���x������(increaseXPRate)���㏸������
    public void IncreaseXPRateIncrease(long upRate)
    {
        for (int i = 0; i < foodList.Count; i++)
        {
            foodList[i].GetComponent<Food>().increaseXPRate = upRate + foodList[i].GetComponent<Food>().increaseXPRate;
        }
    }

    // �K��҂̉��b�ŗ①�ɂ̊g��
    public void ExpandFoodLimit(int upRate, int initialValue)
    {
        limitFoodNum = upRate + initialValue;
    }

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

    private void SetPrefabInformation()
    {
        readJson();

        // json����ǂݍ���
        foreach (var item in jsonData.foodInfos)
        {
            //int p = i * 200;
            GameObject newFood = Instantiate(foodPrefab, new Vector3(0, 0, 0), Quaternion.identity); //Prefab����R�s�[���쐬
            newFood.transform.SetParent(foodContent.transform, false);
            newFood.GetComponent<Food>().InitFood("1-" + (item.id), long.Parse(item.like)); //�t�H���_����摜�ǂݍ��݂���֐������s
            newFood.GetComponent<Food>().id = item.id;
            foodList.Add(newFood); //Prefab�I�u�W�F�N�g�̃��X�g�쐬
            cuisineTypeList.Add(item.mealTime);
            mealTypeList.Add(item.mealTime);

        }
    }

    void readJson()
    {
        //Resources����document.json��ǂݍ��݁Astring�^�ɃL���X�g
        string json = Resources.Load<TextAsset>("foodJson").ToString();

        // jsonData�̏�����
        jsonData = JsonUtility.FromJson<JsonData>(json);
    }

    void Awake()
    {
        frandleManager = GameObject.Find("Frandle").GetComponent<FrandleManager>();
        SetPrefabInformation();
    }
}
