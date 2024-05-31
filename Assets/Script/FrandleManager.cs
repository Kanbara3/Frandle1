using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System;

public class FrandleManager : MonoBehaviour
{
    private FrandleLevelManager levelManager;
    private TapParticle tapParticle;

    public long XP=0; // 数字
    public GameObject harttext;
    public GameObject kankeitext;
    public long oneTapIncrease = 1;
    public int satiety; //満腹度
    public int satietyiMmutable; //変わらない満足度
    public int MAX_SATIETY = 100;
    private int satietyDecreaseRate = 0;　//恩恵で使用 満腹度減少レート変更
    public long sliderXP;

    // Start is called before the first frame update
    void Start()
    {
        levelManager = GameObject.Find("FrandleLevelManager").GetComponent<FrandleLevelManager>();
        sliderXP = XP;
        //GiveButton();
        InvokeRepeating("DecreaseSatietyOverTime", 0f, 1f);

        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(XP);
        //levelManager.FrandleLevelUp(tap);
        //KankeiDirector();
        DecreaseSatietyOverTime();
    }

    //満腹度をMAX_SATIETYを超えないように増やす
    public void SatietyIncreaseRate(int upSatiety)
    {
        if(satiety+upSatiety < MAX_SATIETY)
        {
            if (satiety == 0)
            {
                satietyiMmutable = 0;
            }
            satiety += upSatiety;
            satietyiMmutable += upSatiety;
        }
    }

    private DateTime lastMeal; //食後の時間
    private TimeSpan elapsedTime; //経過時間
    // 満腹度を1秒ごとに減らす
    public void DecreaseSatietyOverTime()
    {
        if (satiety > 0)
        {

            elapsedTime = DateTime.UtcNow - lastMeal;

        }
        satiety = Mathf.Max(0, satietyiMmutable - (int)elapsedTime.TotalSeconds * satietyDecreaseRate);
        if (satiety == 0)
        {
            lastMeal = DateTime.UtcNow;
        }
        
    }

    // 訪問者の恩恵でMAX_SATIETYを増加
    public void MaxSatietyIncrease(int upRate, int initialValue)
    {
        MAX_SATIETY = upRate + initialValue;
    }

    // 訪問者の恩恵でsatietyDecreaseRateを増加
    public void BoostSatietyDecreaseRate(int upRate, int initialValue)
    {
        satietyDecreaseRate = upRate + initialValue;
    }

    // XPを増やす
    public void GainXP(long gainXP)
    {
        XP += gainXP;
        UpdateHeartUI();
    }

    //満足度のセーブ
    public void SaveSatiety()
    {
        DateTime lastMeal2 = DateTime.UtcNow; //食後の時間を記録
        PlayerPrefs.SetInt("saveSatiety", satiety);
        PlayerPrefs.SetString("saveLastMeal", lastMeal2.ToString());
    }

    //満足度のロード
    public void LoadSatiety()
    {
        satiety = PlayerPrefs.GetInt("saveSatiety", 0);
        satietyiMmutable = satiety;
        string lastMealString = PlayerPrefs.GetString("saveLastMeal", "");
        lastMeal  = DateTime.Parse(lastMealString);
    }

    // 好感度(経験値)の更新 新システム
    public void upFavourableImpression(long upRate)
    { 
        XP += upRate;
        sliderXP += upRate;
        levelManager.FrandleLevelUp(XP);
        UpdateHeartUI();
    }

    // ごはんをあげて好感度と満腹度を上昇
    public void EatFood(long upXP, int upSatiety)
    {
        upFavourableImpression(upXP);
        SatietyIncreaseRate(upSatiety);
    }

    public bool CapableToEat(int satietyIncreaseRate)
    {
        if (satiety + satietyIncreaseRate >= MAX_SATIETY) return false;
        return true;
    }

    // oneTapIncreaseの更新
    public void UpdateOneTapIncrease(long upRate, long initialValue)
    {
        oneTapIncrease = upRate + initialValue;
    }

    // BackgroundとFrandleでEventTriggerにアタッチ
    public void HartDirector()
    {
        XP += oneTapIncrease;
        sliderXP += oneTapIncrease;
        levelManager.FrandleLevelUp(XP);
        UpdateHeartUI();
        tapParticle.TapHartParticle();
    }

    // 好感度セーブ
    public void SaveTap()
    {
        PlayerPrefs.SetString("saveTap", XP.ToString());
    }
    //好感度ロード
    public void LoadTap()
    {
        XP = long.Parse(PlayerPrefs.GetString("saveTap", (000).ToString()));
        levelManager.FrandleLevelUp(XP);
        UpdateHeartUI();

    }

    public int GetFrandleLevel()
    {
        return levelManager.currentLevel;
    }

    //harttextの更新
    public void UpdateHeartUI()
    {
        this.harttext.GetComponent<TextMeshProUGUI>().text = XP.ToString("F0");
        levelManager.FrandleLevelUp(XP);
    }

    private void Awake()
    {
        levelManager = GameObject.Find("FrandleLevelManager").GetComponent<FrandleLevelManager>();
        tapParticle = GameObject.Find("TapParticleManager").GetComponent<TapParticle>();
    }

    // 好感度によって関係テキストが変化する
    //public void KankeiDirector()
    //{
    //    if (tap < 1000) // 1
    //    {
    //        this.kankeitext.GetComponent<TextMeshProUGUI>().text = "・・・？";
    //    }
    //    else if (tap < 2000) // 2
    //    {
    //        this.kankeitext.GetComponent<TextMeshProUGUI>().text = "咲夜が置いていったもの";
    //    }
    //    else if (tap < 5000) // 3
    //    {
    //        this.kankeitext.GetComponent<TextMeshProUGUI>().text = "おもちゃ？";
    //    }
    //    else if (tap < 10000) // 4
    //    {
    //        this.kankeitext.GetComponent<TextMeshProUGUI>().text = "おもちゃなのによく動くし喋る";
    //    }
    //    else if (tap < 20000) // 5
    //    {
    //        this.kankeitext.GetComponent<TextMeshProUGUI>().text = "かわいくない事したら壊しちゃうかも";
    //    }
    //    else if (tap < 50000) // 6
    //    {
    //        this.kankeitext.GetComponent<TextMeshProUGUI>().text = "人間？";
    //    }
    //    else if (tap < 100000) // 7
    //    {
    //        this.kankeitext.GetComponent<TextMeshProUGUI>().text = "何かずっといる人間";
    //    }
    //    else if (tap < 300000) // 8
    //    {
    //        this.kankeitext.GetComponent<TextMeshProUGUI>().text = "食べていい人間かな";
    //    }
    //    else if (tap < 600000) // 9
    //    {
    //        this.kankeitext.GetComponent<TextMeshProUGUI>().text = "咲夜に食べちゃダメなおもちゃだって言われた";
    //    }
    //    else if (tap < 1000000) // 10
    //    {
    //        this.kankeitext.GetComponent<TextMeshProUGUI>().text = "ここに入った人間はあなたで三人目よ";
    //    }
    //    else if (tap < 3000000) // 11 
    //    {
    //        this.kankeitext.GetComponent<TextMeshProUGUI>().text = "遊んでる間に壊れちゃっても仕方ないよね";
    //    }
    //    else if (tap < 6000000) // 12
    //    {
    //        this.kankeitext.GetComponent<TextMeshProUGUI>().text = "なにして遊ぶ？";
    //    }
    //    else if (tap < 10000000) // 13
    //    {
    //        this.kankeitext.GetComponent<TextMeshProUGUI>().text = "人間！これ読んで！";
    //    }
    //    else if (tap < 30000000) // 14
    //    {
    //        this.kankeitext.GetComponent<TextMeshProUGUI>().text = "人間～お人形壊れちゃった";
    //    }
    //    else if (tap < 60000000) // 15
    //    {
    //        this.kankeitext.GetComponent<TextMeshProUGUI>().text = "おやつちょーだい";
    //    }
    //    else if (tap < 100000000) // 16
    //    {
    //        this.kankeitext.GetComponent<TextMeshProUGUI>().text = "生身の人間から吸う血ってどんな味かな";
    //    }
    //    else if (tap < 300000000) // 17
    //    {
    //        this.kankeitext.GetComponent<TextMeshProUGUI>().text = "人間…じゃなくておにーさん？";
    //    }
    //    else if (tap < 500000000) // 18
    //    {
    //        this.kankeitext.GetComponent<TextMeshProUGUI>().text = "おにーさん遊んで！";
    //    }
    //    else if (tap < 800000000) // 19
    //    {
    //        this.kankeitext.GetComponent<TextMeshProUGUI>().text = "お姉様っておにーさんと違っていじわるよね";
    //    }
    //    else if (tap < 1000000000) // 20
    //    {
    //        this.kankeitext.GetComponent<TextMeshProUGUI>().text = "ねぇ、吸っていいでしょ？";
    //    }
    //    else if (tap < 2000000000) // 20
    //    {
    //        this.kankeitext.GetComponent<TextMeshProUGUI>().text = "ずーっと私のおもちゃね！";
    //    }
    //    else // 21
    //    {
    //        this.kankeitext.GetComponent<TextMeshProUGUI>().text = "ずーっと私のおもちゃね！";
    //    }
    //}
}