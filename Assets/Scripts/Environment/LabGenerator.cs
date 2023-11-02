using System.Collections.Generic;
using UnityEngine;

public class LabGenerator : MonoBehaviour
{
    public int width = 10;
    public int height = 10;
    public GameObject wallPrefab;
    public GameObject floorPrefab;

    private enum CellType { Wall, Floor }
    private CellType[,] grid;

    private void Start()
    {
        GenerateDungeon();
    }

    public void GenerateDungeon()
    {
        grid = new CellType[width, height];

        // Initialize all cells as walls
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                grid[x, y] = CellType.Wall;
            }
        }

        // Start the maze from a random point
        int startX = Random.Range(0, width);
        int startY = Random.Range(0, height);
        grid[startX, startY] = CellType.Floor;

        Stack<Vector2Int> stack = new Stack<Vector2Int>();
        stack.Push(new Vector2Int(startX, startY));

        while (stack.Count > 0)
        {
            Vector2Int current = stack.Pop();
            List<Vector2Int> neighbors = GetUnvisitedNeighbors(current);

            if (neighbors.Count > 0)
            {
                stack.Push(current);
                Vector2Int chosenNeighbor = neighbors[Random.Range(0, neighbors.Count)];
                Vector2Int direction = (chosenNeighbor - current) / 2;

                grid[current.x + direction.x, current.y + direction.y] = CellType.Floor;
                grid[chosenNeighbor.x, chosenNeighbor.y] = CellType.Floor;

                stack.Push(chosenNeighbor);
            }
        }

        // Instantiate the prefabs based on the grid
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (grid[x, y] == CellType.Wall)
                {
                    Instantiate(wallPrefab, new Vector3(x, 0, y), Quaternion.identity);
                }
                else
                {
                    Instantiate(floorPrefab, new Vector3(x, 0, y), Quaternion.identity);
                }
            }
        }
    }

    List<Vector2Int> GetUnvisitedNeighbors(Vector2Int current)
    {
        List<Vector2Int> neighbors = new List<Vector2Int>();

        for (int dx = -2; dx <= 2; dx += 2)
        {
            for (int dy = -2; dy <= 2; dy += 2)
            {
                if (Mathf.Abs(dx) != Mathf.Abs(dy))
                {
                    int newX = current.x + dx;
                    int newY = current.y + dy;

                    if (newX >= 0 && newX < width && newY >= 0 && newY < height && grid[newX, newY] == CellType.Wall)
                    {
                        neighbors.Add(new Vector2Int(newX, newY));
                    }
                }
            }
        }

        return neighbors;
    }
}
