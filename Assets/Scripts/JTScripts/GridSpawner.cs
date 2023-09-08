using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSpawner : MonoBehaviour
{
    public int width;
    public int height;
    public float cellSize;
    public GameObject objectToSpawn;
    public GameObject cellChild; // Reference to your prefab
    public int gridSizeX = 5; // Number of grid cells in the X direction
    public int gridSizeY = 5; // Number of grid cells in the Y direction

    void Start()
    {
        CreateGrid();
    }



    void CreateGrid()
    {
        // Loop through the grid's rows and columns
        for (int row = 0; row < gridSizeY; row++)
        {
            for (int col = 0; col < gridSizeX; col++)
            {
                // Calculate the position for each grid cell
                Vector3 position = new Vector3(col * cellSize, row * cellSize, 0f);

                // Spawn the prefab as a child of the GridManager at the calculated position
                GameObject newObject = Instantiate(cellChild, position, Quaternion.identity);
                newObject.transform.SetParent(transform); // Set GridManager as the parent.
                newObject.name = (("cellChild") + row + col);
                newObject.SetActive(false);
            }
        }
    }
}

