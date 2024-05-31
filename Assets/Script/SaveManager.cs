using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private FrandleManager frandleManager;
    private VisitorManager visitorManager;
    private ToyManager toyManager;
    private MoneyManager moneyManager;
    private FoodManager foodManager;
    private GachaManager gachaManager;
    private AutoXpManager autoXpManagerEhon;
    private AutoXpManager autoXpManagerDoll;
    // Start is called before the first frame update
    void Start()
    {
        frandleManager.LoadTap();//好感度
        frandleManager.LoadSatiety();//空腹度
        visitorManager.LoadVisitorLevel();//訪問者レベル
        ToyLoad();//おもちゃタイマー
        moneyManager.LoadMoneyData();//お金
        foodManager.LoadNumFoodFunction();
        gachaManager.LoadTicketNum();
        autoXpManagerEhon.LoadAutoXpManager();
        autoXpManagerDoll.LoadAutoXpManager();
    }

    // Update is called once per frame
    void Update()
    {
        frandleManager.SaveTap();
        frandleManager.SaveSatiety();
        visitorManager.SaveVisitorLevel();
        ToySave();
        moneyManager.SaveMoneyData();
        foodManager.SaveNumFoodFunction();
        gachaManager.SaveTicketNum();
        autoXpManagerEhon.SaveAutoXpManager();
        autoXpManagerDoll.SaveAutoXpManager();
    }

    // toy
    private void ToySave()
    {
        for (int i = 0; i < toyManager.toyList.Count; i++)
        {
            toyManager.toyList[i].GetComponent<Toy>().SaveTimerFunction();
        }
    }
    private void ToyLoad()
    {
        for (int i = 0; i < toyManager.toyList.Count; i++)
        {
            toyManager.toyList[i].GetComponent<Toy>().LoadTimerFunction();
        }
    }

    private void Awake()
    {
        frandleManager = GameObject.Find("Frandle").GetComponent<FrandleManager>();
        visitorManager = GameObject.Find("VisitorManager").GetComponent<VisitorManager>();
        toyManager = GameObject.Find("ToyManager").GetComponent<ToyManager>();
        moneyManager = GameObject.Find("MoneyManager").GetComponent<MoneyManager>();
        foodManager = GameObject.Find("FoodManager").GetComponent<FoodManager>();
        gachaManager = GameObject.Find("GachaManager").GetComponent<GachaManager>();
        autoXpManagerEhon = GameObject.Find("AutoXpManagerEhon").GetComponent<AutoXpManager>();
        autoXpManagerDoll = GameObject.Find("AutoXpManagerDoll").GetComponent<AutoXpManager>();
    }
}
