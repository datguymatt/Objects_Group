using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JTGridAnim : MonoBehaviour
{

    public GameObject cellPrefab;
    public int gridSizeX = 32;
    public int gridSizeY = 32;



    private JTCell[,] grid;
    public bool IsAlive = false;

    void Start()
    {
        InitializeGrid();
        InvokeRepeating("UpdateSet", 2.0f, 1.0f);
    }

    void InitializeGrid()
    {
        grid = new JTCell[gridSizeX, gridSizeY];

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {

                GameObject cellObject = Instantiate(cellPrefab, new Vector3(x+.5f, y+.5f), Quaternion.identity);
                JTCell cellScript = cellObject.GetComponent<JTCell>();
                bool IsAlive = Random.Range(0, 2) == 0; // Initialize cell state randomly.
                cellScript.Initialize(IsAlive);
                cellObject.name = (("cellChild") + x + y);

                // Set the GridManager as the parent of the cellObject.
                cellObject.transform.parent = transform; // 'transform' refers to the GridManager's transform.
                grid[x, y] = cellScript;
            }
        }
    }

    void UpdateSet()
    {
        // Update the state of each cell based on Game of Life rules.
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                int liveNeighbors = CountLiveNeighbors(x, y);
                bool newState = ApplyGameOfLifeRules(grid[x, y].IsAlive, liveNeighbors);
                grid[x, y].UpdateState(newState);
            }
        }

    }
    int CountLiveNeighbors(int x, int y)
    {
        int liveNeighbors = 0;

        // Define offsets for checking neighboring cells.
        int[] dx = { -1, -1, -1, 0, 0, 1, 1, 1 };
        int[] dy = { -1, 0, 1, -1, 1, -1, 0, 1 };

        // Loop through the neighboring cells.
        for (int i = 0; i < 8; i++)
        {
            int nx = x + dx[i];
            int ny = y + dy[i];

            // Check if the neighbor is within the grid boundaries.
            if (nx >= 0 && nx < gridSizeX && ny >= 0 && ny < gridSizeY)
            {
                // Check if the neighbor is alive.
                if (grid[nx, ny].IsAlive)
                {
                    liveNeighbors++;
                }
            }
        }

        return liveNeighbors;
    }

    bool ApplyGameOfLifeRules(bool currentState, int liveNeighbors)
    {
        // Conway's Game of Life rules:
        // 1. Any live cell with fewer than two live neighbors dies (underpopulation).
        // 2. Any live cell with two or three live neighbors lives on to the next generation.
        // 3. Any live cell with more than three live neighbors dies (overpopulation).
        // 4. Any dead cell with exactly three live neighbors becomes alive (reproduction).

        if (currentState)
        {
            // Cell is currently alive.
            if (liveNeighbors < 2)
            {
                // Rule 1: Underpopulation - Cell dies.
                return false;
            }
            else if (liveNeighbors == 2 || liveNeighbors == 3)
            {
                // Rule 2: Survival - Cell lives on.
                return true;
            }
            else
            {
                // Rule 3: Overpopulation - Cell dies.
                return false;
            }
        }
        else
        {
            // Cell is currently dead.
            if (liveNeighbors == 3)
            {
                // Rule 4: Reproduction - Cell becomes alive.
                return true;
            }
            else
            {
                // Cell remains dead.
                return false;
            }
        }
    }

}
