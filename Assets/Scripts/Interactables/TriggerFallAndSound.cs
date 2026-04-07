using UnityEngine;
using System.Collections;

public class FallingObjectTrigger : MonoBehaviour
{
    [Header("Prefab Reference")]
    public GameObject goatPrefab;     // Drag SK_Goat_white prefab here
    public Transform spawnPoint;      // Drag a "SpawnPoint" empty object here

    [Header("Goat Sound")]
    public AudioSource audioSource;   // Drag your AudioSource object here
    public AudioClip goatSound;       // Drag your goat sound file here

    [Header("Settings")]
    public string animationTriggerName = "Fall"; 
    public float soundDelay = 1.0f;

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;
            SpawnGoat();
        }
    }

    void SpawnGoat()
    {
        // 1. Create the goat in the scene
        GameObject spawnedGoat = Instantiate(goatPrefab, spawnPoint.position, spawnPoint.rotation);

        // 2. Start the sound delay
        StartCoroutine(PlayGoatSound());

        // 3. (Optional) If your prefab has an animator and needs to start falling immediately
        Animator anim = spawnedGoat.GetComponent<Animator>();
        if (anim != null)
        {
            anim.SetTrigger(animationTriggerName);
        }
    }

    IEnumerator PlayGoatSound()
    {
        yield return new WaitForSeconds(soundDelay);
        if (audioSource != null && goatSound != null)
        {
            audioSource.PlayOneShot(goatSound);
        }
    }
}
