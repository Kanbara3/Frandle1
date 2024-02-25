using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Antlr3.Runtime.Tree.TreeWizard;

[System.Serializable]
public class VisitorJsonData
{
    public VisitorInfo[] visitorInfos;
}
[System.Serializable]
public class VisitorInfo
{
    public int id;
    public string name;
}

public class VisitorManager : MonoBehaviour
{
    public FoodManager foodManager;
    private FrandleManager frandleManager;
    private MoneyManager moneyManager;

    public GameObject visitorPrefab;
    public GameObject visitorContent;
    public List<GameObject> visitorList = new List<GameObject>();

    public VisitorJsonData visitorJsonData;

    void Start()
    {
        readVisiterJson();
        foreach (var visitor in visitorJsonData.visitorInfos)
        {
            GameObject newVisitor = Instantiate(visitorPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            newVisitor.transform.SetParent(visitorContent.transform, false);
            newVisitor.GetComponent<Visitor>().InitVisitor(visitor.id.ToString(), visitor.name);
            newVisitor.GetComponent<Visitor>().id = visitor.id;
            newVisitor.GetComponent<Visitor>().name = visitor.name;
            visitorList.Add(newVisitor); //Prefabオブジェクトのリスト作成
        }
        LoadVisitorLevel();
        GenerateImageIfLevelAboveOne();
    }

    void Update()
    {
        SaveVisitorLevel();
    }

    //visitorのlevelによる恩恵
    public void ApplyVisitorLevelBenefit(int id, int level)
    {
        // 霊夢
        if (id == 1 && level >= 1 && level <= 100)
        {
            frandleManager.UpdateOneTapIncrease(level); //タップで上昇する数を増やす
        }
        // 魔理沙
        if (id == 2 && level >= 1 && level <= 100)
        {
            moneyManager.MoneyIncreaseLimitBoost(level); //放置中の上限増加
        }
        // ルーミア
        if (id == 3 && level >= 1 && level <= 100)
        {

        }
        // 大妖精
        // チルノ
        // 美鈴
        // 小悪魔
        // パチュリー
        // 咲夜
        // レミリア
        if (id == 10 && level >= 1 && level <= 100)
        {
            moneyManager.MoneyIncreaseBoost(level); //1秒で増える金を増加
        }

        //foreach (var visitorGameObject in visitorList)
        //{
        //    var visitor = visitorGameObject.GetComponent<Visitor>();

        //    if (visitor.id == 1)
        //    {
        //        switch (visitor.level)
        //        {
        //            case 1: frandleManager.UpdateOneTapIncrease(1); break;
        //            case 29: frandleManager.UpdateOneTapIncrease(1); break;
        //        }
        //    }


        //    //if (visitor.id == 1)
        //    //{
        //    //    if (visitor.level > 1) { frandleManager.UpdateOneTapIncrease(1); }
        //    //    if (visitor.level > 1) { frandleManager.UpdateOneTapIncrease(1); }

        //    //}
        //    //if (visitor.id == 2)
        //    //{
        //    //    if (visitor.level == 1)
        //    //    {
        //    //        foodManager.foodList[0].GetComponent<Food>().limitFoodNum = 6;
        //    //    }
        //    //}
        //}
    }

    //VisitorLevelのセーブ
    void SaveVisitorLevel()
    {
        for (int i = 0; i < visitorJsonData.visitorInfos.Length; i++)
        {
            int visitorLevel = visitorList[i].GetComponent<Visitor>().level;
            PlayerPrefs.SetInt("saveVisitorLevel_" + i, visitorLevel);
        }
    }

    //VisitorLevelのロード
    void LoadVisitorLevel()
    {
        for (int i = 0; i < visitorJsonData.visitorInfos.Length; i++)
        {

            int visitorLevel = PlayerPrefs.GetInt("saveVisitorLevel_" + i, 0);
            visitorList[i].GetComponent<Visitor>().level = visitorLevel;
            visitorList[i].GetComponent<Visitor>().LevelTextUpdate();
        }
    }

    //VisitorLevelを見て1以上の時に画像を生成
    void GenerateImageIfLevelAboveOne()
    {
        Visitor[] visitors = visitorContent.GetComponentsInChildren<Visitor>();
        foreach (Visitor visitor in visitors)
        {
            if (visitor.level >= 1)
            {
                visitor.InitVisitor(visitor.id.ToString(), visitor.name);
            }
        }
    }

    // jsonの読み込み
    void readVisiterJson()
    {
        string visiterJson = Resources.Load<TextAsset>("visitorJson").ToString();
        visitorJsonData = JsonUtility.FromJson<VisitorJsonData>(visiterJson);
    }


    void Awake()
    {
        frandleManager = GameObject.Find("Frandle").GetComponent<FrandleManager>();
        moneyManager = GameObject.Find("MoneyManager").GetComponent<MoneyManager>();
    }
}
