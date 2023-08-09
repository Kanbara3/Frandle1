using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackManager : MonoBehaviour
{
    public GameObject Frandle;
    
    // FrandleにアタッチしているFrandleManager.csからHartDirectorを実行
    public void TouchBack()
    {
        Frandle.GetComponent<FrandleManager>().HartDirector();
    }
}
