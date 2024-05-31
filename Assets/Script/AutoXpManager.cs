using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AutoXpManager : MonoBehaviour
{
    FrandleManager frandleManager;

    private float currentTimer; // 1•b‚ğ‘ª‚é•Ï”
    private float increaseXpSpan = 1f; // ‰½•b‚²‚Æ‚É‘‚â‚·‚©
    private int lastAddXpTime; // ÅŒã‚É©“®‚ÅXP‚ªã‚ª‚Á‚½ŠÔ
    private int timeIncreaseXp; // ’â~’†‚É‘‚¦‚é‚Í‚¸‚¾‚Á‚½XPor1•b‚Å‘‚¦‚éXP
    private bool autoXpGain = false; // ƒ{ƒ^ƒ“‚ğ‰Ÿ‚µ‚½‚©”Û‚©

    void Start()
    {
        if(lastAddXpTime == 0)
        {
            lastAddXpTime = (int)(DateTime.UtcNow.Subtract(new DateTime(1979, 1, 1))).TotalSeconds;
        }
    }

    void Update()
    {
        if (autoXpGain)
        {
            IncreaseXp();
        }
        IncreaseXPOverTime();
    }

    // increaseXpSpan•b‚²‚Æ‚ÉXP‚ğtimeIncreaseXp‘‚â‚·
    private void IncreaseXPOverTime()
    {
        currentTimer += Time.deltaTime;
        if (currentTimer >= increaseXpSpan)
        {
            frandleManager.GainXP(timeIncreaseXp);
            lastAddXpTime = (int)(DateTime.UtcNow.Subtract(new DateTime(1979, 1, 1))).TotalSeconds; //ÅŠú‚ÉXP‚ª‘‚¦‚½ŠÔ
            timeIncreaseXp = 0;
            currentTimer = 0f;
        }
    }

    // Xp‚ğ‘‚â‚·ŠÖ”
    private void IncreaseXp()
    {
        int currentTime;
        currentTime = (int)(DateTime.UtcNow.Subtract(new DateTime(1979, 1, 1))).TotalSeconds;
        timeIncreaseXp = (int)(currentTime - lastAddXpTime) * 1;
        getTimeToPlay();
        timeIncreaseXp = Mathf.Min(timeIncreaseXp, 10); //Œo‰ßŠÔ‚ÆŒo‰ß‰Â”\ŠÔ‚ğ”ä‚×‚Ä¬‚³‚¢•û‚ğæ‚é
        //Debug.Log(currentTime + "-" + lastAddXpTime);
        IncreaseXPOverTime();
    }

    // ‚¨‚à‚¿‚á‚ÌŠÔ‚Ìæ“¾
    private int timeToPlay = 0;
    private int elapsedTime = 0;
    private int timeLeft = 0;
    private Toy toy;
    public void SetToy(Toy toy)
    {
        this.toy = toy;
    }
    private void getTimeToPlay()
    {
        elapsedTime = (int)toy.elapsedTime.TotalSeconds;
        timeLeft = timeToPlay - elapsedTime;
        Debug.Log(timeLeft);
    }

    // timeIncreaseXp

    public void AutoXpGainOn(int timeToPlay)
    {
        timeIncreaseXp = 0;
        this.timeToPlay = timeToPlay;
        autoXpGain = true;
    }

    public void AutoXpGainOff()
    {
        
        autoXpGain = false;
    }

    private void Awake()
    {
        frandleManager = GameObject.Find("Frandle").GetComponent<FrandleManager>();
    }

    public void SaveAutoXpManager()
    {
        PlayerPrefs.SetInt("lastAddXpTime"+this.name, lastAddXpTime);
        PlayerPrefs.SetInt("autoXp"+this.name, autoXpGain ? 1 : 0);
        PlayerPrefs.SetInt("timeLeft" + this.name, timeLeft);
    }
    public void LoadAutoXpManager()
    {
        lastAddXpTime = PlayerPrefs.GetInt("lastAddXpTime"+this.name, (int)(DateTime.UtcNow.Subtract(new DateTime(1979, 1, 1))).TotalSeconds);
        autoXpGain = PlayerPrefs.GetInt("autoXp"+this.name, 0) == 1;
        timeLeft = PlayerPrefs.GetInt("timeLeft" + this.name, 0);
    }
}
