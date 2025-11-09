using UnityEngine;

public class BlendTreeController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float smooth = 5f;
    private float currentSpeed;
    private float targetSpeed;

    void Update()
    {
        // ไล่ค่า speed แบบ smooth
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Time.deltaTime * smooth);
        animator.SetFloat("speed", currentSpeed);
    }

    public void SetTargetSpeed(float newSpeed)
    {
        targetSpeed = newSpeed;
    }
}
