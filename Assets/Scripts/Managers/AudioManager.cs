using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    Dictionary<AudioClip, float> lastPlayedTime = new Dictionary<AudioClip, float>();
    public static AudioManager instance;

    [Header("Audio Sources (Do not Add)")]
    public AudioSource sfxSource;
    public AudioSource musicSource;

    [Header("Player SFX")]
    public AudioClip jump;
    public AudioClip land;
    public AudioClip hurt;
    public AudioClip steps;

    [Header("Environment SFX")]
    public AudioClip door;
    public AudioClip sword;
    public AudioClip gun;

    [Header("Spider SFX")]
    public AudioClip spiderAttack;
    public AudioClip spiderSlain;

    [Header("Skeleton SFX")] // I put skeleton as a placeholder enemy type
    public AudioClip skeletonAttack;
    public AudioClip skeletonSlain;

    [Header("Music")]
    public AudioClip outsideMusic;
    public AudioClip towerMusic;
    public AudioClip bossMusic;
    [Range(0f, 1f)]
    public float musicVolume = 0.3f;

    [Header("SFX Settings")]
    public float sfxCooldown = 0.1f;
    public float sfxVolume = 0.1f;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (sfxSource == null)
            sfxSource = gameObject.AddComponent<AudioSource>();

        if (musicSource == null)
        {
            musicSource = gameObject.AddComponent<AudioSource>();
        }

        musicSource.loop = true;
        musicSource.volume = musicVolume;

        if (outsideMusic != null)
        {
            musicSource.clip = outsideMusic;
            musicSource.Play();
        }
    }

    public void PlaySFX(AudioClip clip, float cooldown = 0.1f)
    {
        if (clip == null || sfxSource == null) return;

        if (lastPlayedTime.TryGetValue(clip, out float lastTime))
        {
            if (Time.time - lastTime < cooldown)
                return;
        }

        sfxSource.PlayOneShot(clip, sfxVolume);
        lastPlayedTime[clip] = Time.time;
    }
    public void StartBossMusic()
    {
        if (bossMusic == null || musicSource == null) return;
        musicSource.clip = bossMusic;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void ReturnToLevelMusic()
    {
        if (outsideMusic == null || musicSource == null) return;
        musicSource.clip = outsideMusic;
        musicSource.loop = true;
        musicSource.Play();
    }

}