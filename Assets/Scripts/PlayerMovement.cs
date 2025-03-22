using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private Camera cam;
    [SerializeField] private Transform gun;
    [SerializeField] private SpriteRenderer spriteRenderer; 
    private Animator animator;

    [Header("Attributes")]
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float sprintMultiplier = 1.4f;
    [SerializeField] private float rollMultiplier = 2f; 
    [SerializeField] private float rollDuration = 0.5f;
    [SerializeField] private float rollCooldown = 2f;

    Vector2 movement = Vector2.zero;
    Vector2 mousePosition = Vector2.zero;
    private bool isRolling = false;
    private bool canRoll = true;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        Debug.Log("Position X: " + movement.x + " Y: " + movement.y);

        if (movement.x > 0)
            spriteRenderer.flipX = false;
        else if (movement.x < 0)
            spriteRenderer.flipX = true; 

        animator.SetFloat("Speed", movement.sqrMagnitude);

        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);


        // Roll Mechanic
         if (Input.GetKeyDown(KeyCode.Space) && canRoll)
        {
            StartCoroutine(Roll());
        }
    }

    void FixedUpdate()
    {
        // Sprint
        float currentSpeed = movementSpeed;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed *= sprintMultiplier;
        }

        if (isRolling)
        {
            currentSpeed *= rollMultiplier;
        }

        rb2d.MovePosition(rb2d.position + movement * currentSpeed * Time.fixedDeltaTime);  

        Vector2 lookDirection = mousePosition - rb2d.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        gun.rotation = Quaternion.Euler(0, 0, angle);
    }

    IEnumerator Roll()
    {
        isRolling = true;
        canRoll = false;
        animator.SetBool("IsRolling", true);

        yield return new WaitForSeconds(rollDuration);

        isRolling = false;
        animator.SetBool("IsRolling", false);

        yield return new WaitForSeconds(rollCooldown);

        canRoll = true;
    }
}
