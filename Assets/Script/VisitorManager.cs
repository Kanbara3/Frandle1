using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitorManager : MonoBehaviour
{

    public GameObject visitorPrefab;
    public GameObject visitorContent;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i < 11; i++)
        {
            //Debug.Log(i);
            GameObject newVisitor = Instantiate(visitorPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            newVisitor.transform.SetParent(visitorContent.transform, false);
            newVisitor.GetComponent<Visitor>().InitVisitor(i.ToString());
            //newVisitor.GetComponent<Visitor>().InitVisitor("2");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
