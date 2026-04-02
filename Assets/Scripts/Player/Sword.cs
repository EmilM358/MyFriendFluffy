using UnityEngine;
using System.Collections.Generic;

public class Sword : MonoBehaviour
{
    public float damage = 10f;
    private bool canDamage = false;
    private HashSet<Target> hitTargets = new HashSet<Target>();
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Attack();
        }
    }

    void Attack()
    {// ----------- Play Anim and SFX -----------
        animator.SetTrigger("swing");
        AudioManager.instance.PlaySFX(AudioManager.instance.sword);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!canDamage) return;

        // ----------- Actual Hitting -----------
        Target target = other.GetComponent<Target>();

        if (target != null && !hitTargets.Contains(target))
        {
            target.TakeDamage(damage);
            hitTargets.Add(target);
        }
    }

    // ----------- Animation Event Stuff -----------
    public void EnableDamage()
    {
        canDamage = true;
        hitTargets.Clear();
    }

    public void DisableDamage()
    {
        canDamage = false;
    }
}