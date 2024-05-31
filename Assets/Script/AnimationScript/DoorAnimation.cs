using UnityEngine;
using UnityEngine.UI;

public class DoorAnimation : MonoBehaviour
{
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        //anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayDoorAnimation()
    {
        anim = gameObject.GetComponent<Animator>();
        anim.SetBool("boolZoom", true);
    }

    public void StopDoorAnimation()
    {
        anim = gameObject.GetComponent<Animator>();
        anim.SetBool("boolZoom", false);
    }
}
