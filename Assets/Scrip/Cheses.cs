using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Cheses : MonoBehaviour
{
    [Header("Detection Settings")]
    public float detectionRadius = 10f;
    public float fieldOfView = 120f;
    public LayerMask Player;
    public LayerMask Obstacle;

    [Header("Chase Settings")]
    public float chaseSpeed = 5f;
    public float idleSpeed = 1.5f;
    public float loseDistance = 2f;

    [Header("Patrol Settings")]
    public float patrolWaitTime = 3f;
    private float patrolTimer = 0f;
    private Vector3 patrolTarget;
    private Patal patrolGenerator;

    private NavMeshAgent agent;
    private Transform player;
    private Animator animator;

    public bool EnableToChase { get; private set; } = false;
    private float currentAnimSpeed = 0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        patrolGenerator = GetComponent<Patal>();

        // ตั้งเป้าเริ่มต้นสุ่มครั้งแรก
        if (patrolGenerator != null)
            patrolTarget = patrolGenerator.GetRandomPositionWithinRange();
    }

    void Update()
    {
        if (player == null) return;

        bool canSee = CanSeePlayer();
        EnableToChase = canSee;

        // ปรับ Blend Animation
        float targetAnimSpeed = EnableToChase ? 7f : 4.5f;
        currentAnimSpeed = Mathf.Lerp(currentAnimSpeed, targetAnimSpeed, Time.deltaTime * 3f);
        animator.SetFloat("speed", currentAnimSpeed);
    }

    private void ChasePlayer()
    {
        agent.speed = chaseSpeed;
        agent.SetDestination(player.position);
    }

    private void PatrolArea()
    {
        if (patrolGenerator == null) return;

        agent.speed = idleSpeed;

        if (Vector3.Distance(transform.position, patrolTarget) <= 1.5f)
        {
            patrolTimer += Time.deltaTime;
            if (patrolTimer >= patrolWaitTime)
            {
                patrolTarget = patrolGenerator.GetRandomPositionWithinRange();
                patrolTimer = 0f;
            }
        }

        agent.SetDestination(patrolTarget);
    }

    public bool CanSeePlayer()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius, Player);
        if (hits.Length == 0) return false;

        Transform target = hits[0].transform;
        Vector3 directionToPlayer = (target.position - transform.position).normalized;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);
        if (angle > fieldOfView * 0.5f) return false;

        if (Physics.Raycast(transform.position + Vector3.up, directionToPlayer,
            out RaycastHit hit, detectionRadius, Obstacle | Player))
        {
            return true;
        }
        ;
        return false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Vector3 leftLimit = Quaternion.Euler(0, -fieldOfView / 2, 0) * transform.forward;
        Vector3 rightLimit = Quaternion.Euler(0, fieldOfView / 2, 0) * transform.forward;

        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(transform.position, leftLimit * detectionRadius);
        Gizmos.DrawRay(transform.position, rightLimit * detectionRadius);
    }
}
