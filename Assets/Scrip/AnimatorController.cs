using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private string parameterName = "DefaultParameter";

    [SerializeField]
    private float smoothTime = 0.3f;

    private float currentValue;
    private float targetValue;
    private float currentVelocity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get the animator component if not assigned
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        // Initialize current value from animator
        currentValue = animator.GetFloat(parameterName);
        targetValue = currentValue;
    }

    // Update is called once per frame
    void Update()
    {
        // Smoothly transition to target value
        currentValue = Mathf.SmoothDamp(currentValue, targetValue, ref currentVelocity, smoothTime);

        // Update animator parameter
        animator.SetFloat(parameterName, currentValue);
    }

    public void SetTargetValue(float newTarget)
    {
        targetValue = newTarget;
    }
}

