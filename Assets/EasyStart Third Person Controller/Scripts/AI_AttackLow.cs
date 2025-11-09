using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AI_AttackLow : MonoBehaviour
{
    [Header("AttackLow Settings")]
    public float attackRange = 6f;          // ระยะที่เริ่มพุ่งหาเป้าหมาย
    public float attackSpeed = 9f;          // ความเร็วในการพุ่ง
    public float attackDuration = 1.2f;     // ระยะเวลาที่พุ่ง
    public float cooldownTime = 2f;         // เวลาพักก่อนพุ่งใหม่

    [Header("Detection Settings")]
    public LayerMask Player;           // สำหรับตรวจจับผู้เล่น
    public LayerMask Obstacle;         // สำหรับกันชน
    public float viewAngle = 120f;          // มุมมองในการตรวจจับ

    private NavMeshAgent agent;
    private Animator anim;
    private Transform player;
    private float attackTimer;
    private float cooldownTimer;
    private bool isAttacking;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        // ตรวจว่าผู้เล่นอยู่ในระยะและในมุมมองไหม
        if (!isAttacking && distance <= attackRange && CanSeePlayer() && cooldownTimer <= 0f)
        {
            StartAttackLow();
        }

        if (isAttacking)
        {
            ExecuteAttackLow();
        }
        else
        {
            cooldownTimer -= Time.deltaTime;
        }
    }

    // -----------------------------
    // เริ่มโจมตีแบบ AttackLow
    // -----------------------------
    private void StartAttackLow()
    {
        isAttacking = true;
        attackTimer = 0f;
        cooldownTimer = cooldownTime;

        anim.SetTrigger("attackLow"); // ให้ Animator เล่นท่าพุ่ง
        agent.isStopped = false;
        agent.speed = attackSpeed;
    }

    // -----------------------------
    // การเคลื่อนไหวระหว่างพุ่ง
    // -----------------------------
    private void ExecuteAttackLow()
    {
        attackTimer += Time.deltaTime;

        if (player != null)
        {
            agent.SetDestination(player.position);
            LookAtPlayer();
        }

        if (attackTimer >= attackDuration)
        {
            StopAttackLow();
        }
    }

    // -----------------------------
    // หยุดหลังพุ่งครบเวลา
    // -----------------------------
    private void StopAttackLow()
    {
        isAttacking = false;
        agent.isStopped = true;
        agent.ResetPath();
        anim.ResetTrigger("attackLow");
    }

    // -----------------------------
    // ตรวจจับผู้เล่นในระยะและมุมมอง
    // -----------------------------
    private bool CanSeePlayer()
    {
        Vector3 dirToPlayer = (player.position - transform.position).normalized;
        float angle = Vector3.Angle(transform.forward, dirToPlayer);

        if (angle > viewAngle * 0.5f) return false;

        if (Physics.Raycast(transform.position + Vector3.up, dirToPlayer, out RaycastHit hit, attackRange, Obstacle | Player))
        {
            return hit.collider.CompareTag("Player");
        }

        return false;
    }

    private void LookAtPlayer()
    {
        Vector3 dir = (player.position - transform.position).normalized;
        dir.y = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 10f);
    }

    // -----------------------------
    // วาด Gizmos ช่วย Debug
    // -----------------------------
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Vector3 leftLimit = Quaternion.Euler(0, -viewAngle / 2, 0) * transform.forward;
        Vector3 rightLimit = Quaternion.Euler(0, viewAngle / 2, 0) * transform.forward;
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(transform.position, leftLimit * attackRange);
        Gizmos.DrawRay(transform.position, rightLimit * attackRange);
    }

}
