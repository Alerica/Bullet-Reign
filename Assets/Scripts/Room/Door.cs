using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Door Settings")]
    [SerializeField] private bool isVerticalDoor;     
    float teleportDistance = 1.8f; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();

            if (player != null && player.canTeleport)
            {
                Vector2 newPosition = other.transform.position;

                if (isVerticalDoor)
                {
                    float direction = Mathf.Sign(transform.position.y - other.transform.position.y);
                    newPosition.y = transform.position.y + (teleportDistance * direction);

                    newPosition += (newPosition - (Vector2)transform.position).normalized * 0.5f;
                    other.transform.position = newPosition;

                }
                else
                {
                    float direction = Mathf.Sign(transform.position.x - other.transform.position.x);
                    newPosition.x = transform.position.x + (teleportDistance * direction);

                    newPosition += (newPosition - (Vector2)transform.position).normalized * 0.5f;
                    other.transform.position = newPosition;
                }
                Debug.Log($"Player teleported to {newPosition}");
            }
           
        }
    }

}
