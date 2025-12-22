    using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject FadingImage;

    public float moveSpeed;
    private bool isMoving;
   // private Vector2 input;
    private Animator animator;
    private Rigidbody2D rb;
    public LayerMask solidObjectsLayer;
    public LayerMask interactableLayer;
    public int moveStaminaCost = 1;
    public float staminaCostInterval = 1f; //rate of consuming stamina instead of every frame
    private Character character;
    public Vector2 lastMovingVector { get; private set; } // Added lastMovingVector
    //greg's variable
    Vector2 motionVector;

    private float staminaTimer; // Tracks time for stamina reduction
    [SerializeField] AudioSource walkAudio;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        character = GetComponent<Character>();
    }
    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        motionVector = new Vector2(horizontal, vertical);
                                    
        animator.SetFloat("moveX",horizontal);
        animator.SetFloat("moveY", vertical);

        isMoving = horizontal != 0 || vertical != 0;
        animator.SetBool("isMoving", isMoving);
        if(horizontal != 0 || vertical != 0)
        {
            lastMovingVector = new Vector2(
                horizontal,
                vertical).normalized;
            animator.SetFloat("lastmoveX", horizontal);
            animator.SetFloat("lastmoveY", vertical);
        }
        HandleWalkingAudio();
        if (Input.GetKeyDown(KeyCode.Z))
        {
            HandleInteraction();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            HandleGifting();
        }
    }
    private void HandleWalkingAudio()
    {
        if (isMoving)
        {
            if (!walkAudio.isPlaying)
            {
                walkAudio.volume = 0.05f;
                walkAudio.Play();
            }
        }
        else
        {
            if (walkAudio.isPlaying)
            {
                walkAudio.Stop();
            }
        }
    }
    void FixedUpdate()
    {
        Move();
        /*
        if (Input.GetKeyDown(KeyCode.Z))
        {
            HandleInteraction();
        }
           
        if (Input.GetKeyDown(KeyCode.X))
        {
            HandleGifting();
        }*/
    }

    private void Move()
    {
        if (isMoving)
        {
            //prevent player from pushing npc or interactables.
            bool isNear  = Physics2D.OverlapCircle(transform.position + (Vector3)motionVector * 0.4f, 0.4f, interactableLayer);
            if(isNear == false)
            {
                animator.SetBool("isMoving", true);
                rb.velocity = motionVector * moveSpeed;
                // Reduce stamina based on interval
                staminaTimer += Time.fixedDeltaTime;
                if (staminaTimer >= staminaCostInterval)
                {
                 character.GetTired(moveStaminaCost);
                 staminaTimer = 0; // Reset the timer
                }

            }
            else
            {
                // Stop movement if too close to an interactable
                rb.velocity = Vector2.zero;
                animator.SetBool("isMoving", false);
            }

        }
        else
        {
            animator.SetBool("isMoving", false);
            rb.velocity = Vector2.zero; // Stop movement if not moving
        }
        
    }
    private void HandleInteraction()
    {
        FadingImage.SetActive(false);
        //Debug.Log("handling interaction");
        var facingDir = new Vector3(animator.GetFloat("lastmoveX"), animator.GetFloat("lastmoveY"));
        var interactPos = transform.position + facingDir;

        Debug.DrawLine(transform.position, interactPos, Color.red, 1f);

        var collider = Physics2D.OverlapCircle(interactPos, 1f, interactableLayer);
        if (collider != null)
        {
            var character = GetComponent<Character>(); // Assuming the Character component is on the same GameObject
            collider.GetComponent<Interactable>()?.Interact(character); // still need to watch from greg
            
            

        }
    }
    private void HandleGifting()
    {
        var facingDir = new Vector3(animator.GetFloat("lastmoveX"), animator.GetFloat("lastmoveY"));
        var interactPos = transform.position + facingDir;
        var collider = Physics2D.OverlapCircle(interactPos, 0.4f, interactableLayer);
        if (collider != null) 
        {   var giftingComponent = collider.GetComponent<Gifting>();
            if (giftingComponent != null)
            {
                giftingComponent.GiftItem(character);
            }

            // Check if the NPC has a Gifting component

        }
      
    }
}
