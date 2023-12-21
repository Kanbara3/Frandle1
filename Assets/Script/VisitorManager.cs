using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VisitorJsonData
{
    public VisitorInfo[] visitorInfos;
}
[System.Serializable]
public class VisitorInfo
{
    public int id;
    public string name;
}

public class VisitorManager : MonoBehaviour
{

    public GameObject visitorPrefab;
    public GameObject visitorContent;

    private VisitorJsonData visitorJsonData;

    // Start is called before the first frame update
    void Start()
    {

        readVisiterJson();
        foreach (var visitor in visitorJsonData.visitorInfos)
        {
            GameObject newVisitor = Instantiate(visitorPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            newVisitor.transform.SetParent(visitorContent.transform, false);
            newVisitor.GetComponent<Visitor>().InitVisitor(visitor.id.ToString(), visitor.name);
            newVisitor.GetComponent<Visitor>().id = visitor.id;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //ÉKÉ`ÉÉÉ{É^ÉìÇâüÇ≥ÇÍÇΩéû
    public void GachaButton()
    {

    }


    // jsonÇÃì«Ç›çûÇ›
    void readVisiterJson()
    {
        string visiterJson = Resources.Load<TextAsset>("visitorJson").ToString();
        visitorJsonData = JsonUtility.FromJson<VisitorJsonData>(visiterJson);
    }
}
