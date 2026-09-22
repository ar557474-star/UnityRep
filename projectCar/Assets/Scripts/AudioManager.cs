using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    public void ButtonClickSound()
    {
        audioSource.Play();
    }
}
