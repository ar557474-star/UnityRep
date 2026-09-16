using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    AudioSource audioSource;
    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
            return;
        }
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }
    public void PlaySfx(AudioClip clip,float volumeScale)
    {
        audioSource.PlayOneShot(clip, volumeScale);
    }
}
