using System;
using System.Collections;
using UnityEngine;
using Utils;

public class AudioManager : MonoBehaviour
{
    public static EventHandler<SoundItem> soundTriggered;
    // public static EventHandler<SoundEntity> soundTriggered;


    public static AudioManager Instance;
    public SoundLibrary soundLibrary;

    private AudioSourcePool _pool;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if(Instance != this)
            Destroy(gameObject);
    
        DontDestroyOnLoad(gameObject);

        _pool = GetComponent<AudioSourcePool>();
    }
    
    private void OnEnable()
    {
        // GameSettings.audioVolumeChanged += SetSourceVolume;
        soundTriggered += PlaySound;
    }

    private void OnDisable()
    {
        // GameSettings.audioVolumeChanged -= SetSourceVolume;
        soundTriggered -= PlaySound;

    }

    public void PlaySound(object sender, SoundItem item)
    {
        AudioSource audioSource = _pool.ClaimSource(((Component) sender).gameObject);
        SoundEntity entity = soundLibrary.GetEntitiy(item);

        audioSource.volume = entity.volume;
        audioSource.pitch = entity.pitch;
        if (entity.type == SoundType.Ambient)
        {
            audioSource.loop = entity.loop;
            audioSource.clip = entity.clip;
            audioSource.Play();
        }
        else
            audioSource.PlayOneShot(entity.clip);
    }
    
    public void PlaySound(object sender, SoundEntity entity)
    {
        AudioSource audioSource = _pool.ClaimSource(((Component) sender).gameObject);

        audioSource.volume = entity.volume;
        audioSource.pitch = entity.pitch;
        if (entity.type == SoundType.Ambient)
        {
            audioSource.loop = entity.loop;
            audioSource.clip = entity.clip;
            audioSource.Play();
        }
        else
            audioSource.PlayOneShot(entity.clip);
    }

    // public void PlaySoundFX(object sender, SoundItem item)
    // {
    //     StartCoroutine(PlaySoundFXRoutine(sender, item));
    // }

    // private IEnumerator PlaySoundFXRoutine(object sender, SoundItem item)
    // {
    //     AudioSource audioSource = _pool.ClaimSource(((Component) sender).gameObject);
    //     SoundEntity entity = soundLibrary.GetEntitiy(item);
    //     audioSource.loop = entity.loop;
    //     audioSource.volume = entity.volume;
    //     audioSource.pitch = entity.pitch;
    //     audioSource.clip = entity.clip;
    //     audioSource.Play();
    //
    //     yield return null;
    //     // while(true
    //     // {
    //     //     
    //     // }
    // }
}
