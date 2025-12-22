using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CustomerMove : MonoBehaviour
{
    [SerializeField] CustomerNPC customer;
    [SerializeField] float speed = 3f;
    [SerializeField] private Sprite newSprite;
    //[SerializeField] DessertMenuTrigger menu; 
    Rigidbody2D rb2d;
    private CustomerManager manager; 
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    private Animator animator;

    int i;
    private Vector3 target;
    private bool NextMove = false;
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();  // Get SpriteRenderer
        boxCollider = GetComponent<BoxCollider2D>();
        manager = FindObjectOfType<CustomerManager>();
        animator = GetComponent<Animator>();
        if(manager == null)
        {
            Debug.Log("manager is null");
        }
    }

    private void Start()
    {
       // Debug.Log("Position on start: " + transform.position);
        target = customer.movement[i].transform;
        boxCollider.enabled = false;
    }

    private void Update()
    {
        if (i >= customer.movement.Count) return;

        float distance = Vector3.Distance(transform.position, target);
        if (distance < 0.1f)
        {
            rb2d.velocity = Vector2.zero;
            rb2d.bodyType = RigidbodyType2D.Kinematic;
            //Debug.Log($"Arrived at destination {i}");

            if (customer.movement[i].arrivedTable == true)
            {
                //Debug.Log("Arrive at table, waiting for food");
                boxCollider.enabled = true;
                WaitAtTable(); 
                return;
            }

            // Move to the next movement
            i++;

            if (i < customer.movement.Count)
            {
                
                target = customer.movement[i].transform;
                rb2d.bodyType = RigidbodyType2D.Dynamic; // Allow next move
            }
            else
            {
                spriteRenderer.enabled = false;
                boxCollider.enabled = false;
                manager.CheckOrdered();
               // Debug.Log("Schedule completed.");
                customer.data.Ordered = false;
            }

            return;
        }

        // Move toward target
        Vector3 direction = (target - transform.position).normalized;
        rb2d.velocity = direction * speed;
    }

   private void WaitAtTable()
    {
        rb2d.velocity = Vector2.zero;
        rb2d.bodyType = RigidbodyType2D.Kinematic;
        animator.enabled = false; 
        spriteRenderer.sprite = newSprite;
        StartCoroutine(WaitForFood());
        if (NextMove == true)
        {
           // Debug.Log("finished eating");
            i++;
            if (i < customer.movement.Count)
            {
                customer.data.Ordered = false;
                animator.enabled = true;
                target = customer.movement[i].transform;
                rb2d.bodyType = RigidbodyType2D.Dynamic; // Allow next move
            }
        }
    }

    private IEnumerator WaitForFood()
    {
  
       
        yield return new WaitForSeconds(60f);
        //disable box collider when customer about to leave
        
        boxCollider.enabled = false;
        NextMove = true;
        
 
    }
}
