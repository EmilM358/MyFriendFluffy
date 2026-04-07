using UnityEngine;
using UnityEngine.AI;

public class FluffyMovements : MonoBehaviour
{
    public Transform player;
    public float followDistance = 2f;

    [Header("Spider Detection")]
    public float spiderDetectionRadius = 8f;
    public float safeDistanceFromSpiders = 6f;
    private Transform closestSpider;

    private NavMeshAgent agent;
    private Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // ----------- Behaviour -----------
        FindClosestSpider();

        if (closestSpider != null && IsSpiderTooClose())
        {
            FleeFromSpider();
        }
        else
        {
            FollowPlayer();
        }

        UpdateAnimation();
    }

    void FindClosestSpider()
    {
        // ----------- Lists all spiders in the scene -----------
        GameObject[] spiders = GameObject.FindGameObjectsWithTag("Enemy");

        // ----------- Detect closest spider(s) and stay away -----------
        float closestDistance = Mathf.Infinity;
        closestSpider = null;

        foreach (GameObject spider in spiders)
        {
            float distance = Vector3.Distance(transform.position, spider.transform.position);

            if (distance < closestDistance && distance <= spiderDetectionRadius)
            {
                closestDistance = distance;
                closestSpider = spider.transform;
            }
        }
    }

    bool IsSpiderTooClose()
    {
        // ----------- If there are no spiders, forget about this -----------
        if (closestSpider == null) return false;

        // ----------- Check distance from spiders -----------
        float distance = Vector3.Distance(transform.position, closestSpider.position);
        return distance < safeDistanceFromSpiders;
    }

    void FollowPlayer()
    {
        // ----------- Follow and stand next to the player -----------
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > followDistance)
        {
            agent.SetDestination(player.position);
        }
        else
        {
            agent.ResetPath();
        }
    }

    void FleeFromSpider()
    {
        Vector3 fleeDirection = (transform.position - closestSpider.position).normalized;

        // ----------- Stay away from spider but as close as possible to player -----------
        Vector3 targetPosition = transform.position + fleeDirection * safeDistanceFromSpiders;
        targetPosition = Vector3.Lerp(targetPosition, player.position, 0.5f);

        NavMeshHit hit;
        if (NavMesh.SamplePosition(targetPosition, out hit, 5f, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }

    void UpdateAnimation()
    {
        // ----------- Check if should be walking or idling -----------
        float speed = agent.velocity.magnitude;

        if (speed > 0.1f)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }
}
