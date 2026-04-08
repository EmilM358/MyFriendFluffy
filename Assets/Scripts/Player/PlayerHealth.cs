using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    public int health = 100;
    [SerializeField] private UIManager manager;

    void OnStart()
    {
        health = maxHealth;
    }
    public void TakeDamage(int amount)
    {
        if (amount == 0) return;
        health -= amount;
        manager.SetHealth(health);
        if (health <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        if (amount == 0) return;
        health = Mathf.Clamp(health + amount, 0, maxHealth);
        manager.SetHealth(health);
    }
    void Die()
    {
        Debug.Log("oh no");
        // death screen
    }

    [ContextMenu("Test: Take 10 Damage")]
    public void TestTakeDamage() => TakeDamage(10);

    [ContextMenu("Test: Heal 20")]
    public void TestHeal() => Heal(20);
}

