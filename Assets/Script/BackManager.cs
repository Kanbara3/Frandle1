using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackManager : MonoBehaviour
{
    public GameObject Frandle;
    
    // Frandle�ɃA�^�b�`���Ă���FrandleManager.cs����HartDirector�����s
    public void TouchBack()
    {
        Frandle.GetComponent<FrandleManager>().HartDirector();
    }
}
