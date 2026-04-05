using UnityEngine;

public class SpiderAnimationEvents : MonoBehaviour
{
    // This activated the collider to deal damage based on the animation. it also plays the sfx based on the animation.

    private Spider spider;

    private void Awake()
    {
        spider = GetComponentInParent<Spider>();
    }

    public void EnableHitbox()
    {
        spider.EnableHitbox();
    }

    public void DisableHitbox()
    {
        spider.DisableHitbox();
    }

    public void PlayAttackSFX()
    {
        spider.PlayAttackSFX();
    }
}