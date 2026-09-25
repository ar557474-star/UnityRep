using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] AudioClip buttonSfx;
    [SerializeField] AudioClip mainMenuMusic;
    [SerializeField] AudioClip level01Music;

    private AudioSource musicSource;
    private AudioSource uiSource;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        AudioSource[] source = GetComponents<AudioSource>();
        musicSource = source[0];
        uiSource = source[1];
    }

    public void PlayMenuMusic()
    {
        if (musicSource.isPlaying && musicSource.clip == mainMenuMusic) return;
        musicSource.clip = mainMenuMusic;
        musicSource.playOnAwake = true;
        musicSource.loop = true;
        musicSource.volume = 0.1f;
        musicSource.Play();
    }
    public void PlayLevel01Music()
    {
        musicSource.clip = level01Music;
        musicSource.playOnAwake = true;
        musicSource.loop = true;
        musicSource.volume = 0.04f;
        musicSource.Play();
    }
    public void StopLevel01Music()
    {
        if (musicSource.isPlaying)
            musicSource.Stop();
    }
    public void PlayButtonSound()
    {
        uiSource.volume = 0.2f;
        uiSource.PlayOneShot(buttonSfx);
    }
}
