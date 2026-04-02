using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Gun : MonoBehaviour
{
    [Header ("Settings")]
    public Camera fpsCam;
    public float damage = 50f;
    public float range = 100f;


    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        Shoot();
    }

    public void Shoot()
    {
        // ----------- Hitscan stuff ----------- 
        AudioManager.instance.PlaySFX(AudioManager.instance.gun);
        animator.SetTrigger("fired");
        RaycastHit hit; // I prefer shooting with hitscan rather than spawning physical bullets. feel free to disagree and we can talk about it

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Target target = hit.transform.GetComponent<Target>(); // This part will be updated later when we get enemy scripts
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }

    }
}
