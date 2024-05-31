using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FrandleLevelManager : MonoBehaviour
{
    private VisitorManager visitorManager;

    public GameObject frandleLevelText;
    public int currentLevel = 1;
    bool fullLevelFlag = false;
    long[] xpRange = {100,300,700,1300,2100,3100,4300,5700,7300,9100,11100,13300,15700,18300,21100,24100,27300,30700,34300,38200,42400,46900,51700,56800,62200,67900,73900,80200,86800,93700,100900,108400,116200,124300,132700,141400,150400,159700,169300,179300,189700,200500,211700,223300,235300,247700,260500,273700,287300,301300,315700,330500,345700,361300,377300,393700,410500,427700,445300,463400,482000,501100,520700,540800,561400,582500,604100,626200,648800,672000,695800,720200,745200,770800,797000,823800,851200,879200,907800,937200,967400,998400,1030200,1062800,1096200,1130400,1165400,1201200,1234800,1277800,1327800,1387800,1457800,1537800,1627800,1727800,1827800,1927800 };
    //long[] xpRange = {50, 125, 225, 375, 575, 825, 1125, 1525, 2025, 2725, 3625, 4725, 6025, 7525, 9225, 11125, 13225, 15525, 18025,20825,23925,27325,31025,35025,39325 };
    //long[] xpRange = { 10,50,100,200,588,1305,2194,3296,4653,6307,8300,10673,13468,16727,20491,24802,35233,41436,48354,56027,64498,73808,  };
    public Slider slider;

    //レベル管理
    public void FrandleLevelUp(long currentXP)
    {
        if (fullLevelFlag) { return; }
        CalculateLevel(currentXP);
        visitorManager.ALLSetVisitorLevel();
        SliderUpdate(currentXP);
        frandleLevelText.GetComponent<TextMeshProUGUI>().text = "Lv." + currentLevel.ToString();
    }

    // レベルの計算
    void CalculateLevel(long currentXP)
    {
        while (true)
        {
            if (currentXP < xpRange[currentLevel - 1])//現在の経験値が必要経験値より小さければbrake
            {
                break;
            }
            currentLevel++;　//そうでなければレベル+1
            slider.value = 0f;

            if (currentLevel >= xpRange.Length + 1) // 現在のレベルが設定されているレベル上限より大きければfullLevelFlagを立てる
            {
                fullLevelFlag = true;
                break;
            }
        }
    }

    // 好感度スライダーのアップデート
    void SliderUpdate(long currentXP)
    {
        if (fullLevelFlag)
        {
            slider.value = 1f;
            return;
        }
        float bunbo = 0;
        float bunshi = 0;
        if (currentLevel <= 1) slider.value = (float)currentXP / (float)xpRange[currentLevel - 1];
        else
        {
            bunbo = ((float)xpRange[currentLevel - 1] - (float)xpRange[currentLevel - 2]);
            bunshi = (float)currentXP - (float)xpRange[currentLevel - 2];
            slider.value = bunshi / bunbo;
            //Debug.Log(bunshi + " " + bunbo);
        }
    }



    private void Awake()
    {
        visitorManager = GameObject.Find("VisitorManager").GetComponent<VisitorManager>();
    }
}
