using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JTCell : MonoBehaviour
{
    [SerializeField] public bool IsAlive;
    public SpriteRenderer renderer;
    private Color color;

    public void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    public void Initialize(bool isAlive)
    {
        IsAlive = isAlive;

        UpdateCellAppearance();
    }

    public void UpdateState(bool newState)
    {
        IsAlive = newState;
        UpdateCellAppearance();
    }

    private void UpdateCellAppearance()
    {

        // Change the appearance of the cell based on its state (e.g., change color).
        renderer.material.color = IsAlive ? Color.yellow : Color.white;
        color = renderer.material.color;

    }

    public void SetWalkable(bool isWalkable)
    {
        // Set the cell's color to black if it's walkable; otherwise, set it to another color.

    }
}
