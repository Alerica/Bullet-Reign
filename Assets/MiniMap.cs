using UnityEngine;

public class MiniMap : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform player; 
    public Vector3 offset = new Vector3(0, 0, 0);

    void Update()
    {
        if (player)
        {
            transform.position = player.position + offset;
        }
    }
}