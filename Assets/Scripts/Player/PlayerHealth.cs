using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;
    [SerializeField] private UIManager manager;
    public void TakeDamage(int amount)
    {
        health -= amount;
        manager.SetHealth(health);
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("oh no");
        // death screen
    }
}

