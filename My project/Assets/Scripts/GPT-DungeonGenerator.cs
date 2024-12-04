using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonGenerator : MonoBehaviour
{
    public int width = 50;
    public int height = 50;
    public int roomCount = 10;
    public int minRoomSize = 5;
    public int maxRoomSize = 10;
    public int minHallwayLength = 5;
    public int maxHallwayLength = 15;

    [SerializeField]
    public Tile floorTile;  // Tile for floor
    [SerializeField]
    public Tilemap tilemap; // Tilemap to place tiles on

    private int[,] dungeonGrid;

    void Start()
    {
        dungeonGrid = new int[width, height];
        GenerateDungeon();
    }

    void GenerateDungeon()
    {
        // Initialize the grid to 0 (empty space)
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                dungeonGrid[x, y] = 0;
            }
        }

        List<Rect> rooms = new List<Rect>();

        // Generate rooms
        for (int i = 0; i < roomCount; i++)
        {
            int roomWidth = Random.Range(minRoomSize, maxRoomSize);
            int roomHeight = Random.Range(minRoomSize, maxRoomSize);
            int roomX = Random.Range(0, width - roomWidth);
            int roomY = Random.Range(0, height - roomHeight);

            Rect room = new Rect(roomX, roomY, roomWidth, roomHeight);

            bool overlap = false;
            foreach (var existingRoom in rooms)
            {
                if (room.Overlaps(existingRoom))
                {
                    overlap = true;
                    break;
                }
            }

            if (!overlap)
            {
                rooms.Add(room);

                for (int x = (int)room.xMin; x < room.xMax; x++)
                {
                    for (int y = (int)room.yMin; y < room.yMax; y++)
                    {
                        dungeonGrid[x, y] = 1;  // 1 represents a room tile
                    }
                }
            }
        }

        for (int i = 1; i < rooms.Count; i++)
        {
            Vector2Int roomCenter1 = new Vector2Int((int)(rooms[i - 1].x + rooms[i - 1].width / 2),
                                                     (int)(rooms[i - 1].y + rooms[i - 1].height / 2));
            Vector2Int roomCenter2 = new Vector2Int((int)(rooms[i].x + rooms[i].width / 2),
                                                     (int)(rooms[i].y + rooms[i].height / 2));

            CreateHallway(roomCenter1, roomCenter2);
        }

        CreateDungeonTiles();
    }

    void CreateHallway(Vector2Int start, Vector2Int end)
    {
        bool horizontalFirst = Random.value > 0.5f;

        if (horizontalFirst)
        {
            for (int x = start.x; x != end.x; x += (int)Mathf.Sign(end.x - start.x))
            {
                dungeonGrid[x, start.y] = 2;  // 2 represents a hallway tile
            }

            for (int y = start.y; y != end.y; y += (int)Mathf.Sign(end.y - start.y))
            {
                dungeonGrid[end.x, y] = 2;  // 2 represents a hallway tile
            }
        }
        else
        {
            for (int y = start.y; y != end.y; y += (int)Mathf.Sign(end.y - start.y))
            {
                dungeonGrid[start.x, y] = 2;  // 2 represents a hallway tile
            }

            for (int x = start.x; x != end.x; x += (int)Mathf.Sign(end.x - start.x))
            {
                dungeonGrid[x, end.y] = 2;  // 2 represents a hallway tile
            }
        }
    }

    void CreateDungeonTiles()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (dungeonGrid[x, y] == 1 || dungeonGrid[x, y] == 2)
                {
                    // Set a tile for rooms and hallways
                    tilemap.SetTile(new Vector3Int(x, y, 0), floorTile);
                }
            }
        }
    }
}
