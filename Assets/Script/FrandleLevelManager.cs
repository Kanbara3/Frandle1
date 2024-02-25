using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FrandleLevelManager : MonoBehaviour
{
    private FrandleManager frandleManager;

    public GameObject frandleLevelText;
    private int currentLevel = 1;
    bool fullLevelFlag = false;
    //long[] xpRange = {50, 125, 225, 375, 575, 825, 1125, 1525, 2025, 2725, 3625, 4725, 6025, 7525, 9225, 11125, 13225, 15525, 18025 };
    long[] xpRange = { 10, 20, 30, 40, 50 };
    public Slider slider;
    //private long sliderXP;

    //レベル管理
    public void FrandleLevelUp(long currentXP)
    {
        //sliderXP = frandleManager.sliderXP;
        if (fullLevelFlag) { return; }
        while (true)
        {
            if (currentXP < xpRange[currentLevel - 1])//現在の経験値が必要経験値より小さければbrake
            {
                if(currentLevel <= 1)slider.value = (float)frandleManager.sliderXP / (float)xpRange[currentLevel -1];
                else slider.value = (float)frandleManager.sliderXP / ((float)xpRange[currentLevel - 1] - (float)xpRange[currentLevel - 2]);
                break;
            }

            if (currentLevel >= xpRange.Length + 1) frandleManager.sliderXP = xpRange[currentLevel - 1];
            else if (currentLevel == 1) frandleManager.sliderXP -= xpRange[currentLevel - 1];
            else frandleManager.sliderXP -= xpRange[currentLevel - 1] - xpRange[currentLevel - 2];
            currentLevel++;　//そうでなければレベル+1
            slider.value = 0f;
            
            if (currentLevel >= xpRange.Length + 1) // 現在のレベルが設定されているレベル上限より大きければfullLevelFlagを立てる
            {
                fullLevelFlag = true;
                slider.value = 1;
                break;
            }
        }
        frandleLevelText.GetComponent<TextMeshProUGUI>().text = "Lv." + currentLevel.ToString();
    }

    private void Awake()
    {
        frandleManager = GameObject.Find("Frandle").GetComponent<FrandleManager>();
    }
}
