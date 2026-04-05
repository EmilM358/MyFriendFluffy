using UnityEngine;

public class SpiderDamage : MonoBehaviour
{
    public int damage = 10;
    // ----------- When the player is in the trigger zone, it gets damaged -----------
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>()?.TakeDamage(damage);
        }
    }
}
