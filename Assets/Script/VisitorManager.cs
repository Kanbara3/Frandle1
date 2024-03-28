using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
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
    public string enName;
    public string effect;
}

public class VisitorManager : MonoBehaviour
{
    public FoodManager foodManager;
    private FrandleManager frandleManager;
    private MoneyManager moneyManager;
    public ToyManager toyManager;

    public GameObject visitorPrefab;
    public GameObject visitorContent;
    public List<GameObject> visitorList = new List<GameObject>();

    public GameObject menuPrefab;
    public GameObject menuContent;
    public Button visitorButton;

    public VisitorJsonData visitorJsonData;

    void Start()
    {
        readVisiterJson();
        foreach (var visitor in visitorJsonData.visitorInfos)
        {
            //VisitorPrefeab
            GameObject newVisitor = Instantiate(visitorPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            newVisitor.transform.SetParent(visitorContent.transform, false);
            newVisitor.GetComponent<Visitor>().InitVisitor(visitor.id.ToString(), visitor.name);
            newVisitor.GetComponent<Visitor>().id = visitor.id;
            newVisitor.GetComponent<Visitor>().name = visitor.name;
            visitorList.Add(newVisitor); //Prefabオブジェクトのリスト作成
            //MenuPrefeab
            GameObject newMenu = Instantiate(menuPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            newMenu.SetActive(false);
            newMenu.transform.SetParent (menuContent.transform, false);
            newMenu.GetComponent<VisitorMenu>().id = visitor.id;
            newMenu.GetComponent<VisitorMenu>().visitorName = visitor.name;
            newMenu.GetComponent<VisitorMenu>().name = "Menu" + visitor.enName;
            newMenu.GetComponent <VisitorMenu>().effectName = visitor.effect;
            
        }
        
        LoadVisitorLevel();
        GenerateImageIfLevelAboveOne();
        ApplyBenefit();
        UpdateLevelTextInVisitorPanel();
    }

    void Update()
    {
        SaveVisitorLevel();
        
    }

    //visitorのlevelによる恩恵
    public void ApplyVisitorLevelBenefit(int id, int level)
    {
        // 霊夢：タップで上昇する数を増やす
        if (id == 1)
        {
            frandleManager.UpdateOneTapIncrease(level);
            //for ( int i=0; i<level; i++)
            //{
            //    frandleManager.UpdateOneTapIncrease(1);
            //}
        }
        // 魔理沙：放置中に溜まるmoneyの上限増加
        if(id == 2)
        {
            moneyManager.MoneyIncreaseLimitBoost(level);
            //for ( int i=0; i<level; i++)
            //{
            //    moneyManager.MoneyIncreaseLimitBoost(1);
            //}
        }
        // ルーミア：ごはんを上げた時の好感度上昇(increaseXPRate)をブースト
        if (id == 3)
        {
            foodManager.IncreaseXPRateIncrease(level);
            //for ( int i=0; i<level; i++)
            //{
            //    foodManager.IncreaseXPRateIncrease(1);
            //}
        }
        // 大妖精：胃袋拡張
        if (id == 4)
        {
            for ( int i=0; i<level; i++)
            {
                frandleManager.MaxSatietyIncrease(1);
            }
        }
        // チルノ：おもちゃの時間を短縮
        if (id == 5)
        {
            for ( int i=0; i<level; i++)
            {
                toyManager.TimeToPlayDecrease(1);
            }
        }
        // 美鈴：満腹度の減少促進
        if (id == 6)
        {
            for ( int i=0; i<level; i++)
            {
                frandleManager.BoostSatietyDecreaseRate(1);
            }
        }
        // 小悪魔：冷蔵庫拡張
        if ( id == 7)
        {
            foodManager.ExpandFoodLimit(level);
            //for ( int i=0; i<level; i++)
            //{
            //    foodManager.ExpandFoodLimit(1);
            //}
        }
        // パチュリー：ガチャ値下げ
        if (id == 8)
        {
            for ( int i=0; i<level; i++)
            {

            }
        }
        // 咲夜：ごはん値下げ
        if (id == 9)
        {
            for ( int i=0; i<level; i++)
            {

            }
        }
        // レミリア：1秒で増えるmoneyを増加
        if (id == 10)
        {
            for ( int i=0; i<level; i++)
            {
                moneyManager.MoneyIncreaseBoost(1);
            }
        }
    }

    // 起動後に前回の恩恵まで
    private void ApplyBenefit()
    {
        foreach (var visitors in visitorList)
        {
            var visitor = visitors.GetComponent<Visitor>();
            ApplyVisitorLevelBenefit(visitor.id, visitor.level);
        }
    }


    //Levelのセーブ
    void SaveVisitorLevel()
    {
        for (int i = 0; i < visitorJsonData.visitorInfos.Length; i++)
        {
            //visitorLevelのセーブ
            int visitorLevel = visitorList[i].GetComponent<Visitor>().level;
            PlayerPrefs.SetInt("saveVisitorLevel_" + i, visitorLevel);
            //virtualLevelのセーブ
            int virtualLevel = visitorList[i].GetComponent<Visitor>().virtualLevel;
            PlayerPrefs.SetInt("saveVirtualLevel_" + i, virtualLevel);
        }
    }

    //Levelのロード
    void LoadVisitorLevel()
    {
        for (int i = 0; i < visitorJsonData.visitorInfos.Length; i++)
        {
            //visitorLevelのロード
            int visitorLevel = PlayerPrefs.GetInt("saveVisitorLevel_" + i, 0);
            visitorList[i].GetComponent<Visitor>().level = visitorLevel;
            // virtualLevelのロード
            int virtualLevel = PlayerPrefs.GetInt("saveVirtualLevel_" + i, 0);
            visitorList[i].GetComponent<Visitor>().virtualLevel = virtualLevel;
            visitorList[i].GetComponent<Visitor>().LevelTextUpdate();
        }
    }

    // 訪問者パネルを開いた時にlevelを更新する
    void UpdateLevelTextInVisitorPanel()
    {
        visitorButton.onClick.AddListener(() =>
        {
            for (int i = 0; i < visitorJsonData.visitorInfos.Length; i++)
            {
                visitorList[i].GetComponent<Visitor>().SetVisitorLevel();
                Debug.Log("test");
            }
            
        });
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
        toyManager = GameObject.Find("ToyManager").GetComponent<ToyManager>();
    }
}
