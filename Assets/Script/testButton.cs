using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testButton : MonoBehaviour
{
    public GachaManager gachaManager;
    public FrandleManager frandleManager;
    public FrandleLevelManager frandleLevelManager;
    public VisitorManager visitorManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //ガチャ10連
        if (Input.GetKeyDown(KeyCode.A))
        {
            gachaManager.ticketCount++;
            gachaManager.GachaButton10();
        }

        //好感度リセット
        if (Input.GetKeyDown(KeyCode.S))
        {
            frandleManager.XP = 0;
            frandleManager.UpdateHeartUI();
        }

        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    frandleManager.XP = 0;
        //    frandleLevelManager.FrandleLevelUp(0);
        //}

        // 訪問者のレベルリセット
        if (Input.GetKeyDown(KeyCode.E))
        {
            for (int i = 0; i < visitorManager.visitorJsonData.visitorInfos.Length; i++)
            {
                //visitorLevelのセーブ
                visitorManager.visitorList[i].GetComponent<Visitor>().level = 0;
                visitorManager.visitorList[i].GetComponent<Visitor>().virtualLevel = 0;
            }
        }
    }
}
