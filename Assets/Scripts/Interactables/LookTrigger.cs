using UnityEngine;
using System.Collections;
public class LookTrigger : MonoBehaviour
{
    public float lookDistance = 10f; // How far you can look to trigger it
    public string targetTag = "Target"; 
    private bool hasTriggered = false;

    public AudioSource FluffyIntro;

    void Update()
    {
        // 1. Create a ray starting from camera position, pointing forward
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        // 2. Perform the raycast
        if (Physics.Raycast(ray, out hit, lookDistance))
        {
            // 3. Check if the object hit has the correct tag
            if (hit.collider.CompareTag(targetTag) && !hasTriggered)
            {
                TriggerAudio(hit.collider.gameObject);
                hasTriggered = true; // Prevents the sound from looping every frame
            }
        }
        else
        {
            // Optional: reset trigger when looking away
            hasTriggered = false; 
        }
    }

    void TriggerAudio(GameObject target)
    {
        AudioSource audio = target.GetComponent<AudioSource>();
        if (audio != null)
        {
            audio.Play();
        }
        if (FluffyIntro != null)
        {
            FluffyIntro.Stop();
        }
}
}

