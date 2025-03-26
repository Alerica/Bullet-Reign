using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private Camera cam;
    [SerializeField] private Transform gun;
    [SerializeField] private SpriteRenderer spriteRenderer; 
    [SerializeField] private Slider rollCooldownBar;
    [SerializeField] private GameObject rollCooldownBarObject;
    [SerializeField] private Skill defaultSkill;


    private Animator animator;

    [Header("Attributes")]
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float sprintMultiplier = 1.4f;
    [SerializeField] private float rollMultiplier = 2f; 
    [SerializeField] private float rollDuration = 0.5f;
    [SerializeField] private float rollCooldown = 0.9f;
    private float teleportCooldown = 0.2f; 

    Vector2 movement = Vector2.zero;
    public Vector2 mousePosition = Vector2.zero;
    private bool isRolling = false;
    private bool canRoll = true;
    public bool canTeleport = true;

    // Skill System
    public Skill[] skillSlots = new Skill[4];
    

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
         skillSlots[0] = defaultSkill;
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if (movement.sqrMagnitude > 1)
        {
            movement = movement.normalized;
        }
        // Debug.Log("Position X: " + movement.x + " Y: " + movement.y);

        if (movement.x > 0)
            spriteRenderer.flipX = false;
        else if (movement.x < 0)
            spriteRenderer.flipX = true; 

        animator.SetFloat(StringManager.speed, movement.sqrMagnitude);

        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);


        // Roll Mechanic
        if (Input.GetKeyDown(KeyCode.Space) && canRoll)
        {
            StartCoroutine(Roll());
        }

        // Skill
        HandleSkillInput();
    }

    void FixedUpdate()
    {
        
        float currentSpeed = movementSpeed;

        // Sprint
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

    void HandleSkillInput()
    {
        if (Input.GetKeyDown(KeyCode.Q) && skillSlots[0] != null)
            skillSlots[0].Activate(this);
        if (Input.GetKeyDown(KeyCode.W) && skillSlots[1] != null)
            skillSlots[1].Activate(this);
        if (Input.GetKeyDown(KeyCode.E) && skillSlots[2] != null)
            skillSlots[2].Activate(this);
        if (Input.GetKeyDown(KeyCode.R) && skillSlots[3] != null)
            skillSlots[3].Activate(this);
    }

    public void AddSkill(Skill newSkill)
    {
        for (int i = 0; i < skillSlots.Length; i++)
        {
            if (skillSlots[i] == null)
            {
                skillSlots[i] = newSkill;
                return;
            }
        }
    }

    public void RemoveSkill(int index)
    {
        if (index >= 0 && index < skillSlots.Length)
            skillSlots[index] = null;
    }

    IEnumerator Roll()
    {
        isRolling = true;
        canRoll = false;
        animator.SetBool(StringManager.isRolling, true);

        rollCooldownBarObject.SetActive(true); 
        rollCooldownBar.value = 0; 

        yield return new WaitForSeconds(rollDuration);

        isRolling = false;
        animator.SetBool(StringManager.isRolling, false);

        float elapsedTime = 0f;
        while (elapsedTime < rollCooldown)
        {
            elapsedTime += Time.deltaTime;
            rollCooldownBar.value = elapsedTime / rollCooldown;
            yield return null;
        }

        rollCooldownBar.value = 1; 

        rollCooldownBarObject.SetActive(false); 
        canRoll = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Door") && canTeleport)
        {
            Debug.Log("Entering Door");
            StartCoroutine(TeleportCooldown());
        }
    }

    private IEnumerator TeleportCooldown()
    {
        canTeleport = false; 
        yield return new WaitForSeconds(teleportCooldown);
        canTeleport = true; 
    }

}
