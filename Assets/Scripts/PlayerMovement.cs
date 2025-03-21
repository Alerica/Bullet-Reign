using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb2d;


    [Header("Attributes")]
    [SerializeField] private float playerSpeed = 5f;

    Vector2 movement = Vector2.zero;
    
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        Debug.Log("X: " + movement.x + " Y: " + movement.y);
    }

    void FixedUpdate()
    {
        rb2d.MovePosition(rb2d.position + movement * playerSpeed * Time.fixedDeltaTime);   
    }
}
