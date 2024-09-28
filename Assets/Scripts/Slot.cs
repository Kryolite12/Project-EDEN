using UnityEngine;

public class Slot : MonoBehaviour
{
    public bool HasTile { get; private set; } = false;
    public Tile CurrentTile { get; private set; }

    public void AddTile(Tile tile)
    {
        if (tile != null)
        {
            tile.transform.SetParent(transform);
            tile.transform.localPosition = Vector3.zero;
            HasTile = true;
            CurrentTile = tile;
        }
        else
        {
            Debug.LogError("Attempted to add a null tile to the slot.");
        }
    }

    public void RemoveTile()
    {
        HasTile = false;
        CurrentTile = null;
    }
}
