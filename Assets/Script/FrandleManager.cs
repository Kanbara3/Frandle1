using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FrandleManager : MonoBehaviour
{
    public int tap;
    public GameObject harttext;
    public GameObject kankeitext;
    int oneTapIncrease = 1;

    // Start is called before the first frame update
    void Start()
    {
        tap = 0;
        //GiveButton();
    }

    // Update is called once per frame
    void Update()
    {
        KankeiDirector();
        
    }

    // 好感度(tap)の更新
    public void changeOneTapIncreaseRate(int upRate)
    {
        oneTapIncrease += upRate;
    }
    
    public void HartDirector()
    {
        tap += oneTapIncrease;
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

    /*
    // ごはんの数制御
    // ごはんの個数のtext
    public TextMeshProUGUI text_food1_1;
    public TextMeshProUGUI text_food1_2;
    public TextMeshProUGUI text_food1_3;
    public TextMeshProUGUI text_food1_4;
    // ごはんの個数のint
    int num_food1_1 = 10;
    int num_food1_2 = 10;
    int num_food1_3 = 10;
    int num_food1_4 = 10;
    // ごはんを与えるButton
    public Button button_food1_1;
    public Button button_food1_2;
    public Button button_food1_3;
    public Button button_food1_4;
    // ごはんを与えた時のtapした時の好感度増幅量
    private int tap_food1_1 = 1;
    private int tap_food1_2 = 2;
    private int tap_food1_3 = 3;
    private int tap_food1_4 = 4;
    public void GiveButton()
    {
        button_food1_1.onClick.AddListener(() =>
        {
            if (num_food1_1 > 0)
            {
                num_food1_1 -= 1;
                one_tap += tap_food1_1;
                text_food1_1.text = "×" + " " + num_food1_1.ToString();
            }
        });
        button_food1_2.onClick.AddListener(() =>
        {
            if (num_food1_2 > 0)
            {
                num_food1_2 -= 1;
                one_tap += tap_food1_2;
                text_food1_2.text = "×" + " " + num_food1_2.ToString();
            }
        });
        button_food1_3.onClick.AddListener(() =>
        {
            if (num_food1_3 > 0)
            {
                num_food1_3 -= 1;
                one_tap += tap_food1_3;
                text_food1_3.text = "×" + " " + num_food1_3.ToString();
            }
        });
        button_food1_4.onClick.AddListener(() =>
        {
            if (num_food1_4 > 0)
            {
                num_food1_4 -= 1;
                one_tap += tap_food1_4;
                text_food1_4.text = "×" + " " + num_food1_4.ToString();
            }
        });
    }
    */
}