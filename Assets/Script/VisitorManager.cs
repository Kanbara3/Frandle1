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
        //ApplyBenefit();
        UpdateLevelTextInVisitorPanel();
    }

    void Update()
    {

    }

    // 起動後に前回の恩恵まで
    private void ApplyBenefit()
    {
        foreach (var visitors in visitorList)
        {
            var visitor = visitors.GetComponent<Visitor>();
            visitor.ChatchFrandleLevelUped();
        }
    }


    //Levelのセーブ
    public void SaveVisitorLevel()
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
    public void LoadVisitorLevel()
    {
        for (int i = 0; i < visitorJsonData.visitorInfos.Length; i++)
        {
            //visitorLevelのロード
            int visitorLevel = PlayerPrefs.GetInt("saveVisitorLevel_" + i, 0);
            visitorList[i].GetComponent<Visitor>().level = visitorLevel;
            // virtualLevelのロード
            int virtualLevel = PlayerPrefs.GetInt("saveVirtualLevel_" + i, 0);
            visitorList[i].GetComponent<Visitor>().SetVirtualLeve(virtualLevel);
        }
        GenerateImageIfLevelAboveOne();
        ApplyBenefit();
    }

    // 訪問者パネルを開いた時にlevelを更新する
    void UpdateLevelTextInVisitorPanel()
    {
        visitorButton.onClick.AddListener(() =>
        {
            for (int i = 0; i < visitorJsonData.visitorInfos.Length; i++)
            {
                visitorList[i].GetComponent<Visitor>().SetVisitorLevel();
            }

        });
    }

    //SetVisitorLevelを全ての訪問者で実行
    public void ALLSetVisitorLevel()
    {
        foreach (var visitor in visitorList)
        {
            visitor.GetComponent<Visitor>().SetVisitorLevel();
        }
        ApplyBenefit();
    }

    // Prefabへの情報代入
    void SetPrefabInformation()
    {
        foreach (var visitor in visitorJsonData.visitorInfos)
        {
            //VisitorPrefeab
            GameObject newVisitor = Instantiate(visitorPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            newVisitor.transform.SetParent(visitorContent.transform, false);
            newVisitor.GetComponent<Visitor>().SetVisitorImage(visitor.id.ToString(), visitor.name);
            newVisitor.GetComponent<Visitor>().id = visitor.id;
            newVisitor.GetComponent<Visitor>().name = visitor.name;
            visitorList.Add(newVisitor); //Prefabオブジェクトのリスト作成
            //MenuPrefeab
            GameObject newMenu = Instantiate(menuPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            newMenu.SetActive(false);
            newMenu.transform.SetParent(menuContent.transform, false);
            newMenu.GetComponent<VisitorMenu>().id = visitor.id;
            newMenu.GetComponent<VisitorMenu>().visitorName = visitor.name;
            newMenu.GetComponent<VisitorMenu>().name = "Menu" + visitor.enName;
            newMenu.GetComponent<VisitorMenu>().effectName = visitor.effect;

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
                visitor.SetVisitorImage(visitor.id.ToString(), visitor.name);
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
        readVisiterJson();
        SetPrefabInformation();
    }
}
