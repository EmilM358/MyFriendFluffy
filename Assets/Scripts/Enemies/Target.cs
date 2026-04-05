using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 100f;
    private Spider spider;
    private bool isDead = false;

    void Awake()
    {
        spider = GetComponent<Spider>();
    }

    public void TakeDamage(float amount)
    {
        // ----------- If dead, stop everything -----------
        if (isDead) return;

        // ----------- Applying damage, die when its 0 or lower -----------

        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }
    void Die()
    {
        // ----------- Telling it to stop everything and make the spider disappear -----------
        if (isDead) return;
        isDead = true;
        spider.Die();
        Destroy(gameObject, 5f);
    }
}