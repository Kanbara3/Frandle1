using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FrandleManager : MonoBehaviour
{
    private int tap;
    public GameObject harttext;
    public GameObject kankeitext;

    // Start is called before the first frame update
    void Start()
    {
        tap = 0;
    }

    // Update is called once per frame
    void Update()
    {
        KankeiDirector();
    }

    public void HartDirector()
    {
        tap += 1;
        this.harttext.GetComponent<TextMeshProUGUI>().text = tap.ToString("F0");
    }

    

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
