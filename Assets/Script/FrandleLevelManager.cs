using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FrandleLevelManager : MonoBehaviour
{
    private FrandleManager frandleManager;

    public GameObject frandleLevelText;
    public int currentLevel = 1;
    bool fullLevelFlag = false;
    //long[] xpRange = {50, 125, 225, 375, 575, 825, 1125, 1525, 2025, 2725, 3625, 4725, 6025, 7525, 9225, 11125, 13225, 15525, 18025 };
    long[] xpRange = { 5, 10, 20, 25, 30, 40, 50 };
    public Slider slider;

    //レベル管理
    public void FrandleLevelUp(long currentXP)
    {
        if (fullLevelFlag) { return; }
        CalculateLevel(currentXP);
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
        frandleManager = GameObject.Find("Frandle").GetComponent<FrandleManager>();
    }
}
