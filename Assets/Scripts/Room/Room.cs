using UnityEngine;
using System.Collections.Generic;

public class Room : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject topDoor;
    [SerializeField] GameObject bottomDoor;
    [SerializeField] GameObject leftDoor;
    [SerializeField] GameObject rightDoor;
    [SerializeField] GameObject topWall;
    [SerializeField] GameObject bottomWall;
    [SerializeField] GameObject leftWall;
    [SerializeField] GameObject rightWall;
    [SerializeField] EnemySpawner enemySpawner;
    [SerializeField] private bool isBossRoom = false; 
    [SerializeField] private GameObject bossPrefab;   
    List<GameObject> enemies = new List<GameObject>();

    private List<GameObject> closedDoors = new List<GameObject>(); 
    private bool roomCleared = false;

    public Vector2Int RoomIndex { get; set;}

    public void OpenDoor(Vector2Int direction)
    {
        if(direction == Vector2Int.up)
        {
            topDoor.SetActive(true);
        }

        if(direction == Vector2Int.down)
        {
            bottomDoor.SetActive(true);
        }

        if(direction == Vector2Int.left)
        {
            leftDoor.SetActive(true);
        }

        if(direction == Vector2Int.right)
        {
            rightDoor.SetActive(true);
        }
    }

    private void ActivateDoor(GameObject door)
    {
        if (door != null)
        {
            door.SetActive(true);
        }
    }

    private void DeactivateDoor(GameObject door)
    {
        if (door != null && door.activeSelf)
        {
            closedDoors.Add(door);
            door.SetActive(false);
        }
    }

    private void LockDoors()
    {
        closedDoors.Clear(); 

        DeactivateDoor(topDoor);
        DeactivateDoor(bottomDoor);
        DeactivateDoor(leftDoor);
        DeactivateDoor(rightDoor);
    }

    private void UnlockDoors()
    {
        foreach (GameObject door in closedDoors)
        {
            ActivateDoor(door);
        }
        closedDoors.Clear();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Player entering room {this.name}");
        if (other.CompareTag("Player") && !roomCleared)
        {
            ActivateRoom();
        }
    }

    private void ActivateRoom()
    {
        if (gameObject.name == StringManager.initialRoom) return;

        if (isBossRoom)
        {
            Debug.Log("Boss Room Activated!");
            SpawnBoss();
            LockDoors();
            return;
        }


        if (enemies == null || enemies.Count == 0) 
        {
            enemies = enemySpawner.SpawnEnemies(this);
        }

        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(true);
        }

        LockDoors();
    }

    private void SpawnBoss()
    {
        if (bossPrefab != null)
        {
            GameObject boss = Instantiate(bossPrefab, transform.position, Quaternion.identity);
            enemies.Add(boss);
        }
    }

    public void SetBossRoom()
    {
        isBossRoom = true;
        Debug.Log($"{this.name} is now the Boss Room!");
    }



    public void EnemyDefeated(GameObject enemy)
    {
        enemies.Remove(enemy);
        Debug.Log("Enemy Defeated");
        if (AllEnemiesDefeated())
        {
            roomCleared = true;
            UnlockDoors();
        }
    }

    private bool AllEnemiesDefeated()
    {
        foreach (GameObject enemy in enemies)
        {
            if (enemy.activeInHierarchy)
            {
                return false;
            }
        }
        return true;
    }

}
