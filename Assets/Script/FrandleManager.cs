using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class FrandleManager : MonoBehaviour
{
    public long tap=0; // 数字
    public GameObject harttext;
    public GameObject kankeitext;
    public long oneTapIncrease = 1;
    private int satiety; //満腹度
    private FrandleLevelManager levelManager;
    public long sliderXP;

    // Start is called before the first frame update
    void Start()
    {
        levelManager = GameObject.Find("FrandleLevelManager").GetComponent<FrandleLevelManager>();
        sliderXP = tap;
        //tap = 0;
        //GiveButton();
        //LoadTap();
    }

    // Update is called once per frame
    void Update()
    {
        //levelManager.FrandleLevelUp(tap);
        //KankeiDirector();
        SaveTap();
        
    }

    //満腹度
    public void SatietyIncreaseRate(int upSatiety)
    {
        satiety += upSatiety;
    }

    // 好感度(tap)の更新 旧システム
    public void changeOneTapIncreaseRate(long upRate)
    {
        oneTapIncrease += upRate;
    }

    // 好感度(経験値)の更新 新システム
    public void upFavourableImpression(long upRate) 
    {
        tap += upRate;
    }

    // oneTapIncreaseの更新
    public void UpdateOneTapIncrease(long upRate)
    {
        oneTapIncrease += upRate;
    }

    // BackgroundとFrandleでEventTriggerにアタッチ
    public void HartDirector()
    {
        tap += oneTapIncrease;
        sliderXP += oneTapIncrease;
        levelManager.FrandleLevelUp(tap);
        HeartTextUpdate();
    }

    // スライダーの更新
    public void UpdateSliderValue()
    {
        levelManager.FrandleLevelUp(tap);
    }

    // 好感度セーブ
    public void SaveTap()
    {
        PlayerPrefs.SetString("saveTap", tap.ToString());
    }
    //好感度ロード
    public void LoadTap()
    {
        tap = long.Parse(PlayerPrefs.GetString("saveTap", (000).ToString()));
        HeartTextUpdate();
    }

    //harttextの更新
    public void HeartTextUpdate()
    {
        this.harttext.GetComponent<TextMeshProUGUI>().text = tap.ToString("F0");
    }
    
    // 好感度によって関係テキストが変化する
    public void KankeiDirector()
    {
        if (tap < 1000) // 1
        {
            this.kankeitext.GetComponent<TextMeshProUGUI>().text = "・・・？";
        }
        else if (tap < 2000) // 2
        {
            this.kankeitext.GetComponent<TextMeshProUGUI>().text = "咲夜が置いていったもの";
        }
        else if (tap < 5000) // 3
        {
            this.kankeitext.GetComponent<TextMeshProUGUI>().text = "おもちゃ？";
        }
        else if (tap < 10000) // 4
        {
            this.kankeitext.GetComponent<TextMeshProUGUI>().text = "おもちゃなのによく動くし喋る";
        }
        else if (tap < 20000) // 5
        {
            this.kankeitext.GetComponent<TextMeshProUGUI>().text = "かわいくない事したら壊しちゃうかも";
        }
        else if (tap < 50000) // 6
        {
            this.kankeitext.GetComponent<TextMeshProUGUI>().text = "人間？";
        }
        else if (tap < 100000) // 7
        {
            this.kankeitext.GetComponent<TextMeshProUGUI>().text = "何かずっといる人間";
        }
        else if (tap < 300000) // 8
        {
            this.kankeitext.GetComponent<TextMeshProUGUI>().text = "食べていい人間かな";
        }
        else if (tap < 600000) // 9
        {
            this.kankeitext.GetComponent<TextMeshProUGUI>().text = "咲夜に食べちゃダメなおもちゃだって言われた";
        }
        else if (tap < 1000000) // 10
        {
            this.kankeitext.GetComponent<TextMeshProUGUI>().text = "ここに入った人間はあなたで三人目よ";
        }
        else if (tap < 3000000) // 11 
        {
            this.kankeitext.GetComponent<TextMeshProUGUI>().text = "遊んでる間に壊れちゃっても仕方ないよね";
        }
        else if (tap < 6000000) // 12
        {
            this.kankeitext.GetComponent<TextMeshProUGUI>().text = "なにして遊ぶ？";
        }
        else if (tap < 10000000) // 13
        {
            this.kankeitext.GetComponent<TextMeshProUGUI>().text = "人間！これ読んで！";
        }
        else if (tap < 30000000) // 14
        {
            this.kankeitext.GetComponent<TextMeshProUGUI>().text = "人間～お人形壊れちゃった";
        }
        else if (tap < 60000000) // 15
        {
            this.kankeitext.GetComponent<TextMeshProUGUI>().text = "おやつちょーだい";
        }
        else if (tap < 100000000) // 16
        {
            this.kankeitext.GetComponent<TextMeshProUGUI>().text = "生身の人間から吸う血ってどんな味かな";
        }
        else if (tap < 300000000) // 17
        {
            this.kankeitext.GetComponent<TextMeshProUGUI>().text = "人間…じゃなくておにーさん？";
        }
        else if (tap < 500000000) // 18
        {
            this.kankeitext.GetComponent<TextMeshProUGUI>().text = "おにーさん遊んで！";
        }
        else if (tap < 800000000) // 19
        {
            this.kankeitext.GetComponent<TextMeshProUGUI>().text = "お姉様っておにーさんと違っていじわるよね";
        }
        else if (tap < 1000000000) // 20
        {
            this.kankeitext.GetComponent<TextMeshProUGUI>().text = "ねぇ、吸っていいでしょ？";
        }
        else if (tap < 2000000000) // 20
        {
            this.kankeitext.GetComponent<TextMeshProUGUI>().text = "ずーっと私のおもちゃね！";
        }
        else // 21
        {
            this.kankeitext.GetComponent<TextMeshProUGUI>().text = "ずーっと私のおもちゃね！";
        }
    }
}