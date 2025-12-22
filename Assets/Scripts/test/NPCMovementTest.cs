using UnityEngine;

public class NPCMovementTest : MonoBehaviour
{
    public float moveSpeed = 1f;
    private Vector3 targetPosition;
    private bool isMoving = false;
    private bool canMove = true; // Default to not being able to move
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isMoving && canMove)
        {
           //Debug.Log("NPC is moving.");
            MoveToTarget();
        }
        else if (!canMove)
        {
           // Debug.Log("NPC movement is paused.");
        }
    }


    public void SetTarget(Vector3 target)
    {
        targetPosition = target;
        isMoving = true;
    }

    private void MoveToTarget()
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Set animation parameters
        animator.SetFloat("moveX", direction.x);
        animator.SetFloat("moveY", direction.y);
        animator.SetBool("isMoving", true);

        // Stop when close enough to the target
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            isMoving = false;
            animator.SetBool("isMoving", false);
        }
    }

    public void ToggleMovement(bool enable)
    {
        canMove = enable;
        Debug.Log($"NPC movement toggled. canMove: {canMove}");

        if (!enable)
        {
            isMoving = false;
            animator.SetBool("isMoving", false);
            Debug.Log("NPC movement stopped.");
        }
        else
        {
            isMoving = true;
            animator.SetBool("isMoving", true);
            Debug.Log("NPC continue movement");
        }
    }

}
