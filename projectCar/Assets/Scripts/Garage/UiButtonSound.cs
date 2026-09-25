using UnityEngine;

public class UiButtonSound : MonoBehaviour
{
    public void ButtonSound()
    {
        AudioManager.instance.PlayButtonSound();
    }
}
