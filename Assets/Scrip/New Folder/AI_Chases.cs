using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AI_Cheses : MonoBehaviour
{
    [Header("Detection Settings")]
    [Tooltip("รัศมีในการตรวจจับผู้เล่น")]
    public float detectionRadius = 10f;
    [Tooltip("มุมมองสายตา (องศา)")]
    public float fieldOfView = 120f;
    [Tooltip("เลเยอร์ของผู้เล่น")]
    public LayerMask Player;
    [Tooltip("เลเยอร์ของสิ่งกีดขวาง เช่น กำแพง")]
    public LayerMask Obstacle;

    [Header("Chase Settings")]
    [Tooltip("ความเร็วในการไล่ล่า")]
    public float chaseSpeed = 5f;
    [Tooltip("ความเร็วเวลาเดินสำรวจ")]
    public float idleSpeed = 1.5f;
    [Tooltip("ระยะที่ถ้าผู้เล่นหนีออกไปจะหยุดไล่ล่า")]
    public float loseDistance = 2f;

    [Header("Patrol Settings")]
    [Tooltip("รัศมีที่ AI จะสุ่มเดินสำรวจ")]
    public float patrolRange = 10f;
    [Tooltip("เวลาที่รอก่อนสุ่มจุดใหม่")]
    public float patrolWaitTime = 3f;

    private NavMeshAgent agent;
    private Animator animator;
    private Transform player;

    private Vector3 patrolTarget;
    private float patrolTimer = 0f;
    private bool enableToChase = false;
    private float currentAnimSpeed = 0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        patrolTarget = GetRandomPositionWithinRange();
    }

    void Update()
    {
        if (player == null) return;

        bool canSeePlayer = CanSeePlayer();
        enableToChase = canSeePlayer;

        if (enableToChase)
            ChasePlayer();
        else
            PatrolArea();

        // ปรับความเร็วอนิเมชันให้นุ่มนวล (ใช้กับ Blend Tree)
        float targetAnimSpeed = enableToChase ? 1f : 0.3f;
        currentAnimSpeed = Mathf.Lerp(currentAnimSpeed, targetAnimSpeed, Time.deltaTime * 3f);
        if (animator) animator.SetFloat("speed", currentAnimSpeed);
    }

    /// <summary>
    /// พฤติกรรมไล่ล่าผู้เล่น
    /// </summary>
    private void ChasePlayer()
    {
        agent.speed = chaseSpeed;
        if (player)
            agent.SetDestination(player.position);
    }

    /// <summary>
    /// พฤติกรรมสำรวจพื้นที่แบบสุ่ม
    /// </summary>
    private void PatrolArea()
    {
        agent.speed = idleSpeed;

        if (Vector3.Distance(transform.position, patrolTarget) <= 1.5f)
        {
            patrolTimer += Time.deltaTime;
            if (patrolTimer >= patrolWaitTime)
            {
                patrolTarget = GetRandomPositionWithinRange();
                patrolTimer = 0f;
            }
        }

        agent.SetDestination(patrolTarget);
    }

    /// <summary>
    /// ตรวจสอบว่ามองเห็นผู้เล่นหรือไม่ (ใช้ระยะ + มุม + Raycast)
    /// </summary>
    public bool CanSeePlayer()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius, Player);
        if (hits.Length == 0) return false;

        Transform target = hits[0].transform;
        Vector3 directionToPlayer = (target.position - transform.position).normalized;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);

        // อยู่นอกมุมมองสายตา
        if (angle > fieldOfView * 0.5f) return false;

        // ตรวจสอบ Raycast ว่ามีสิ่งกีดขวางไหม
        if (Physics.Raycast(transform.position + Vector3.up, directionToPlayer,
            out RaycastHit hit, detectionRadius, Obstacle | Player))
        {
            return hit.collider.CompareTag("Player");
        }

        return false;
    }

    /// <summary>
    /// สุ่มหาตำแหน่งบน NavMesh ภายในระยะ patrolRange
    /// </summary>
    private Vector3 GetRandomPositionWithinRange()
    {
        for (int i = 0; i < 10; i++) // ลองสุ่ม 10 ครั้งเพื่อหา point ที่อยู่บน NavMesh
        {
            Vector3 randomDirection = Random.insideUnitSphere * patrolRange;
            randomDirection.y = 0;
            Vector3 candidate = transform.position + randomDirection;

            if (NavMesh.SamplePosition(candidate, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
                return hit.position;
        }

        // ถ้าหาไม่ได้จริง ๆ กลับไปจุดเดิม
        return transform.position;
    }

    /// <summary>
    /// แสดง Gizmos ใน Scene สำหรับ Debug มุมมองสายตาและรัศมีตรวจจับ
    /// </summary>
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
