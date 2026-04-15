using UnityEngine;

public class LookTrigger : MonoBehaviour
{
    public float lookDistance = 10f;
    public string targetTag = "Target";

    private bool hasTriggered = false;

    public AudioSource Scene1Clip1;

    void Update()
    {
        if (hasTriggered) return;

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, lookDistance))
        {
            if (hit.collider.CompareTag(targetTag))
            {
                TriggerAudio(hit.collider.gameObject);
                hasTriggered = true;
            }
        }
    }

    void TriggerAudio(GameObject target)
    {
        if (Scene1Clip1 != null && Scene1Clip1.isPlaying)
        {
            Scene1Clip1.Stop();
        }

        AudioSource audio = target.GetComponent<AudioSource>();
        if (audio != null && !audio.isPlaying)
        {
            audio.Play();
        }
    }
}