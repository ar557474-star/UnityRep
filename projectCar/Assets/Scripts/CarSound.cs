using UnityEngine;

public class CarSound : MonoBehaviour
{
    [SerializeField] AudioClip engineSound;
    [SerializeField] AudioClip brakeSound;
    [SerializeField] AudioClip hitSound;

    private AudioSource engineSource;
    private AudioSource sfxSource;

    [SerializeField] float minPitch = 0.6f;
    [SerializeField] float maxPitch = 1.6f;
    [SerializeField] float maxSpeed = 100f;

    private CarController carController;
    private bool isBraking = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioSource[] source = GetComponents<AudioSource>();
        engineSource=source[0];
        sfxSource =source[1];
        PlayEngineSound();

        carController = GetComponent<CarController>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateEngineSoundPitch();
        HandleBrakeSound();
    }
    private void PlayEngineSound()
    {
        engineSource.clip = engineSound;
        engineSource.volume = 0.1f;
        engineSource.loop = true;
        engineSource.Play();
    }
    private void UpdateEngineSoundPitch()
    {
        float speed = carController.CarSpeed();
        float normalizeSpeed = speed / maxSpeed;
        normalizeSpeed = Mathf.Clamp01(normalizeSpeed);//between 0 and 1
        engineSource.pitch = Mathf.Lerp(minPitch, maxPitch, normalizeSpeed);
    }
    private void HandleBrakeSound()
    {
        if(Input.GetKey(KeyCode.Space) && carController.CarSpeed() > 1)
        {
            if (!isBraking)
            {
                isBraking= true;
                sfxSource.clip = brakeSound;
                sfxSource.volume = 0.2f;
                sfxSource.loop = true;
                sfxSource.Play();
            } 
        }
        else
        {
            if (isBraking)
            {
                isBraking = false;
                sfxSource.Stop();
            }
        }
    }
    public void PlayHitSound()
    {
        sfxSource.volume = 0.1f;
        sfxSource.PlayOneShot(hitSound);
    }
    public void StopAllCarSounds()
    {
        if(engineSource.isPlaying) engineSource.Stop();
        if(sfxSource.isPlaying) sfxSource.Stop();
    }
}
