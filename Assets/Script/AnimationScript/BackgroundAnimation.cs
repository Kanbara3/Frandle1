using UnityEngine;

public class BackgroundAnimation : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayBackgroundlAnimation()
    {
        anim = gameObject.GetComponent<Animator>();
        anim.SetBool("boolFade2", true);
    }

    public void StopBackgroundAnimation()
    {
        anim = gameObject.GetComponent<Animator>();
        anim.SetBool("boolFade2", false);
    }
}
