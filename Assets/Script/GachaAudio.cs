using UnityEngine;

public class GachaAudio : MonoBehaviour
{
    public AudioClip sound1;
    public AudioClip sound2;
    AudioSource audioSource;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //‰¹(sound1)‚ð–Â‚ç‚·
            audioSource.PlayOneShot(sound1);
        }
    }

    public void PlayKnockAudio()
    {
        audioSource.PlayOneShot(sound1);
    }

    public void PlayOpenAudio()
    {
        audioSource.PlayOneShot(sound2);
    }

    public void PlayAudio()
    {
        PlayKnockAudio();
        Invoke(nameof(PlayOpenAudio), 0.7f);
    }
}
