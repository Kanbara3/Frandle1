using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ToyManager : MonoBehaviour
{
    public GameObject toyContent;
    public List<GameObject> toyList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // –K–âÒ‚Ì‰¶Œb‚Å—V‚ÔŠÔ‚ğ’Zk
    //public void TimeToPlayDecrease(int downRate)
    //{
    //    for (int i = 0; i < toyList.Count; i++)
    //    {
    //        toyList[i].GetComponent<Toy>().timeToPlay -= downRate;
    //    }
    //        //Debug.Log(toyList.Count);
    //}

    // ‚¨‚à‚¿‚á‚Ì‰¶Œb‚ğã‚°‚é
    public void RaiseTheBenefitOfToy(int upRate, int initialValue)
    {
        for (int i = 0; i < toyList.Count; i++)
        {
            toyList[i].GetComponent<Toy>().HeartIncreaseAmount = upRate + initialValue;
            toyList[i].GetComponent<Toy>().MoneyIncreaseAmount = upRate + initialValue;
        }
    }

    void Awake()
    {
        for (int i = 0; i < toyContent.transform.childCount; i++)
        {
            toyList.Add(toyContent.transform.GetChild(i).gameObject);
        }
    }
}
