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

    [Header("Player Footsteps")]
    public List<AudioClip> playerSteps;

    [Header("Fluffy Footsteps")]
    public AudioClip fluffyStep;
    [Range(0.5f, 1.5f)] public float fluffyPitchMin = 0.9f;
    [Range(0.5f, 1.5f)] public float fluffyPitchMax = 1.1f;

    [Header("Footstep Timing")]
    public float playerStepInterval = 0.5f;
    public float fluffyStepInterval = 0.4f;
    float lastPlayerStepTime;
    float lastFluffyStepTime;

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

    public void PlayPlayerFootstep()
    {
        if (playerSteps == null || playerSteps.Count == 0) return;

        if (Time.time - lastPlayerStepTime < playerStepInterval)
            return;

        AudioClip clip = playerSteps[Random.Range(0, playerSteps.Count)];
        PlaySFX(clip, sfxCooldown);

        lastPlayerStepTime = Time.time;
    }

    public void PlayFluffyFootstep()
    {
        if (fluffyStep == null || sfxSource == null) return;

        if (Time.time - lastFluffyStepTime < fluffyStepInterval)
            return;

        float randomPitch = Random.Range(fluffyPitchMin, fluffyPitchMax);
        sfxSource.pitch = randomPitch;

        sfxSource.PlayOneShot(fluffyStep, sfxVolume);

        sfxSource.pitch = 1f;
        lastFluffyStepTime = Time.time;
    }
}