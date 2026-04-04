using UnityEngine;

public class Target : MonoBehaviour
{
    // Temporary script so that the player can deal damage, will be replaced by enemy scripts at some point
    public float health = 100f;

    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}