using UnityEngine;
using UnityEngine.AI;

public class Spider : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask groundLayer, playerLayer;
    private Animator animator;
    private float attackTimer;
    private bool isDead = false;
    public GameObject hitbox;

    // ----------- Patroling -----------
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    // ----------- Attacking -----------
    public float attackIntervals;
    bool hasAttacked;

    // ----------- States -----------
    public float sightRange, attackRange;
    public bool inSight, inRange;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        // ----------- If spider is ded, IT DOES NOT GET UP -----------
        if (isDead) return;

        // ----------- Check if player is in sight and in range -----------
        inSight = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        inRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);

        // ----------- Behaviour -----------
        if (!inSight && !inRange) Patroling();
        if (inSight && !inRange) Chasing();
        if (inSight && inRange) Attacking();
    }

    private void Patroling()
    {
        // ----------- Search a spot -----------
        if (!walkPointSet) SearchWalkPoint();

        // ----------- Go to spot -----------
        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // ----------- Spot reached, reset -----------
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;

        animator.Play("Walk");
    }
    private void SearchWalkPoint()
    {
        // ----------- Search random position on ground layer -----------
        float randomZ = Random.Range (-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, groundLayer))
            walkPointSet = true;

    }
    private void Chasing()
    {
        agent.SetDestination(player.position);
        animator.Play("Walk");
    }
    private void Attacking()
    {
        // ----------- Stop and look at player when attacking -----------
        agent.SetDestination(transform.position);
        Vector3 direction = player.position - transform.position;
        direction.y = 0f;

        if (direction != Vector3.zero)
        {
            Quaternion rot = Quaternion.LookRotation(direction);
            transform.rotation = rot;
        }

        attackTimer += Time.deltaTime;
        //attack code here

        if (attackTimer >= attackIntervals)
            {
                animator.SetTrigger("Attack");
                attackTimer = 0f;
            }
    }

    public void Die()
    {
        // ----------- Stop every process when ded and fire animation + SFX -----------
        if (isDead) return;
        isDead = true;
        agent.enabled = false;
        hitbox.SetActive(false);

        animator.SetTrigger("Dead");
        AudioManager.instance.PlaySFX(AudioManager.instance.spiderSlain);
    }

    // ----------- Setup for Animation Events -----------
    public void EnableHitbox()
    {
        hitbox.SetActive(true);
    }

    public void DisableHitbox()
    {
        hitbox.SetActive(false);
    }

    public void PlayAttackSFX()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.spiderAttack);
    }

}
