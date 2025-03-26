using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject roomPrefab;
    [SerializeField] GameObject player;

    [Header("Attributes")]
    [SerializeField] private int maxRooms = 20;
    [SerializeField] private int minRooms = 10;
    [SerializeField] private int maxClusteredRoom = 1;
    [SerializeField] int gridSizeX = 20;
    [SerializeField] int gridSizeY = 20;
    private int roomWidth = 20;
    private int roomHeight = 12;



    private List<GameObject> roomObjects = new List<GameObject>();
    private Queue<Vector2Int> roomQueue = new Queue<Vector2Int>();
    private int[,] roomGrid;
    private int roomCount;
    private bool generationComplete = false;

    void Start()
    {
        roomGrid = new int[gridSizeX, gridSizeY];
        roomQueue = new Queue<Vector2Int>();

        Vector2Int initialRoomIndex = new Vector2Int(gridSizeX / 2, gridSizeY / 2);
        StartRoomGenerationFromRoom(initialRoomIndex);
    }

    private void StartRoomGenerationFromRoom(Vector2Int roomIndex)
    {
        roomQueue.Enqueue(roomIndex);
        int x = roomIndex.x;
        int y = roomIndex.y;
        roomGrid[x, y] = 1;
        roomCount++;
        
        var initialRoom = Instantiate(roomPrefab, GetPositionFromGridIndex(roomIndex), Quaternion.identity);
        initialRoom.name = $"Room-{roomCount}";
        initialRoom.GetComponent<Room>().RoomIndex = roomIndex;
        roomObjects.Add(initialRoom);

        if (player != null) player.transform.position = GetPositionFromGridIndex(roomIndex);
    }

    private void Update()
    {
        if (roomQueue.Count > 0 && roomCount < maxRooms && !generationComplete)
        {
            Vector2Int roomIndex = roomQueue.Dequeue();
            TryGenerateRoom(new Vector2Int(roomIndex.x - 1, roomIndex.y)); // Left
            TryGenerateRoom(new Vector2Int(roomIndex.x + 1, roomIndex.y)); // Right
            TryGenerateRoom(new Vector2Int(roomIndex.x, roomIndex.y + 1)); // Up
            TryGenerateRoom(new Vector2Int(roomIndex.x, roomIndex.y - 1)); // Down
        }
        else if (roomCount < minRooms)
        {
            Debug.Log("Room count is less than the minimum required. Regenerating...");
            RegenerateRoom();
        }
        else if (!generationComplete)
        {
            Debug.Log($"Generation complete, {roomCount} rooms created.");
            PrintRoomGrid();
            OpenAllDoors();
            generationComplete = true;
        }
    }

    private bool TryGenerateRoom(Vector2Int roomIndex)
    {
        int x = roomIndex.x;
        int y = roomIndex.y;

        if (x < 0 || x >= gridSizeX || y < 0 || y >= gridSizeY) return false;
        if (roomCount >= maxRooms) return false;
        if (roomGrid[x, y] != 0) return false;
        if (CountAdjacentRoom(roomIndex) > maxClusteredRoom) return false;
        if (Random.value < 0.5f && roomIndex != Vector2Int.zero) return false;

        var newRoom = Instantiate(roomPrefab, GetPositionFromGridIndex(roomIndex), Quaternion.identity);
        newRoom.GetComponent<Room>().RoomIndex = roomIndex;
        newRoom.name = $"Room-{roomCount + 1}";
        roomObjects.Add(newRoom);

        // Debug.Log($"Placing room at: {x}, {y}");

        roomGrid[x, y] = 1;
        roomCount++;
        roomQueue.Enqueue(roomIndex);

        return true;
    }

    private void RegenerateRoom()
    {
        foreach (var room in roomObjects)
            Destroy(room);
        
        roomObjects.Clear();
        roomGrid = new int[gridSizeX, gridSizeY];
        roomQueue.Clear();
        roomCount = 0;
        generationComplete = false;

        Vector2Int initialRoomIndex = new Vector2Int(gridSizeX / 2, gridSizeY / 2);
        StartRoomGenerationFromRoom(initialRoomIndex);
    }

    void OpenDoors(GameObject room, int x, int y)
    {
        Room newRoomScript = room.GetComponent<Room>();

        Room leftRoomScript = RoomExistsAt(x - 1, y) ? GetRoomScriptAt(new Vector2Int(x - 1, y)) : null;
        Room rightRoomScript = RoomExistsAt(x + 1, y) ? GetRoomScriptAt(new Vector2Int(x + 1, y)) : null;
        Room topRoomScript = RoomExistsAt(x, y + 1) ? GetRoomScriptAt(new Vector2Int(x, y + 1)) : null;
        Room bottomRoomScript = RoomExistsAt(x, y - 1) ? GetRoomScriptAt(new Vector2Int(x, y - 1)) : null;

        if (leftRoomScript != null) { newRoomScript.OpenDoor(Vector2Int.left); leftRoomScript.OpenDoor(Vector2Int.right); }
        if (rightRoomScript != null) { newRoomScript.OpenDoor(Vector2Int.right); rightRoomScript.OpenDoor(Vector2Int.left); }
        if (bottomRoomScript != null) { newRoomScript.OpenDoor(Vector2Int.down); bottomRoomScript.OpenDoor(Vector2Int.up); }
        if (topRoomScript != null) { newRoomScript.OpenDoor(Vector2Int.up); topRoomScript.OpenDoor(Vector2Int.down); }
    }

    void OpenAllDoors()
    {
        foreach (var room in roomObjects)
        {
            Room roomScript = room.GetComponent<Room>();
            if (roomScript != null)
            {
                Vector2Int index = roomScript.RoomIndex;
                OpenDoors(room, index.x, index.y);
            }
        }
    }

    Room GetRoomScriptAt(Vector2Int index)
    {
        foreach (var room in roomObjects)
        {
            Room script = room.GetComponent<Room>();
            if (script != null && script.RoomIndex == index)
                return script;
        }
        return null;
    }

    bool RoomExistsAt(int x, int y)
    {
        if (x < 0 || x >= gridSizeX || y < 0 || y >= gridSizeY) return false;
        return roomGrid[x, y] != 0;
    }

    private int CountAdjacentRoom(Vector2Int roomIndex)
    {
        int x = roomIndex.x;
        int y = roomIndex.y;
        int count = 0;

        if (RoomExistsAt(x - 1, y)) count++; 
        if (RoomExistsAt(x + 1, y)) count++; 
        if (RoomExistsAt(x, y - 1)) count++; 
        if (RoomExistsAt(x, y + 1)) count++; 

        return count;
    }

    void PrintRoomGrid()
    {
        string gridStr = "";
        for (int y = gridSizeY - 1; y >= 0; y--) 
        {
            for (int x = 0; x < gridSizeX; x++)
            {
                gridStr += roomGrid[x, y] + " ";
            }
            gridStr += "\n";
        }
        // Debug.Log($"Room Grid:\n{gridStr}");
    }

    private Vector3 GetPositionFromGridIndex(Vector2Int gridIndex)
    {
        return new Vector3(roomWidth * (gridIndex.x - gridSizeX / 2), roomHeight * (gridIndex.y - gridSizeY / 2));
    }
}
