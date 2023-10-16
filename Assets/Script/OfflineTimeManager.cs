using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OfflineTimeManager : MonoBehaviour
{
    //DateTime dt;
    DateTime _endTime;

    void Start()
    {
        //dt = DateTime.Now;
        //Debug.Log(dt);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private double CalcSecond(DateTime now, DateTime prevTime)
    {
        return (now - prevTime).TotalSeconds;
    }
    
    private void SetOfflineEarning()
    {
        double seconds = CalcSecond(DateTime.UtcNow, _endTime);
        _endTime = DateTime.UtcNow;
        Debug.Log(seconds);
    }
    
    private void OnApplicationPause(bool pauseStatus)
    {
        //ÉQÅ[ÉÄíÜÇÕñ≥éã
        //if (GameSceneManager.IsGameStart) return;
    }
}
