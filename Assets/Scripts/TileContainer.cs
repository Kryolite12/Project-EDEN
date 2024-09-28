using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileContainer : MonoBehaviour
{
    public GameObject slotPrefab;
    public int capacity = 7;
    public float slotSpacing = -1.0f;

    private List<Slot> slots = new List<Slot>();
    public GameObject AccessDenied;
    public GameObject RedContainer;
    public GameObject WhiteContainer;
    private List<(Tile tile, Slot slot)> moveHistory = new List<(Tile, Slot)>(); // To store move history

    private bool undoUsed = false;
    public Button undoButton;
    private bool hintUsed = false;
    public Button solveButton;

    private void Start()
    {
        CreateSlots();
    }

    private void CreateSlots()
    {
        Vector3 startPosition = transform.position + new Vector3(-0.5f * (capacity - 1) * slotSpacing, 0, 0); // Adjust start position based on the number of slots
        for (int i = 0; i < capacity; i++)
        {
            Vector3 position = startPosition + new Vector3(i * slotSpacing, 0, 0);
            GameObject slotObject = Instantiate(slotPrefab, position, Quaternion.identity, transform);
            Slot slot = slotObject.GetComponent<Slot>();
            if (slot != null)
            {
                slots.Add(slot);
            }
           
        }

       
    }

    public Slot GetAvailableSlot()
    {
        foreach (var slot in slots)
        {
            if (!slot.HasTile)
            {
                return slot;
            }
        }
      return null;
       
    }
    public Slot CheckAvailableSlot()
    {
        foreach (var slot in slots)
        {
            if (!slot.HasTile)
            {
                return slot;
            }
            else 
            {
                Debug.Log("No Available Slots");
            }
        }
        return null ;
        

    }

    public void CheckForMatchingTiles()
    {
        Dictionary<int, List<Slot>> numberSlots = new Dictionary<int, List<Slot>>();

        foreach (var slot in slots)
        {
            if (slot.HasTile)
            {
                int tileNumber = slot.CurrentTile.GetTileNumber();
                if (!numberSlots.ContainsKey(tileNumber))
                {
                    numberSlots[tileNumber] = new List<Slot>();
                }
                numberSlots[tileNumber].Add(slot);
            }
        }
       


        foreach (var kvp in numberSlots)
        {
            if (kvp.Value.Count >= 3)
            {
                foreach (var slot in kvp.Value)
                {
                    slot.CurrentTile.DestroyTile();
                    slot.RemoveTile();

                   
                    
                }
            }
        }
        CheckIfAllSlotsAreEmpty();
    }
    private void CheckIfAllSlotsAreEmpty()
    {
        foreach (var slot in slots)
        {
            if (!slot.HasTile)
            {
                return; // If any slot has a tile, exit the method
            }
        }

        // If we reach here, it means all slots are empty
        if (AccessDenied != null)
        {
            AccessDenied.SetActive(true);
            RedContainer.SetActive(true);
            WhiteContainer.SetActive(false);
        }
    }
    // Call this function whenever a move is made
    public void AddMoveToHistory(Tile tile, Slot slot)
    {
        if (tile != null && slot != null)
        {
            moveHistory.Add((tile, slot)); // Add the move (tile, slot) to the history
            Debug.Log("Move added to history.");
        }
    }


    public void UndoLastMove()
    {
        if (undoUsed)
        {
            
            Debug.Log("Undo has already been used.");
            return;
        }

        if (moveHistory.Count > 0)
        {
            // Get the last move from history
            var lastMove = moveHistory[moveHistory.Count - 1];
            Tile tile = lastMove.tile;
            Slot slot = lastMove.slot;
           

            // Undo the move by removing the tile from the slot and returning it to its original state
            if (slot != null && tile != null)
            {
                slot.RemoveTile(); // Remove the tile from the slot
                tile.transform.SetParent(null); // Detach tile from slot
                tile.transform.position = tile.startPosition; // Reset tile's position (assuming startPosition is stored in Tile class)
                tile.gameObject.SetActive(true); // Reactivate the tile in the play area
            }

            // Remove the move from history
            moveHistory.RemoveAt(moveHistory.Count - 1);
            Debug.Log("Last move undone.");
            undoUsed = true;
            undoButton.interactable = false;// Set the flag to true
        }
        else
        {
            Debug.Log("No moves to undo.");
        }
    }
    public void UseHint()
    {
        if (hintUsed)
        {
           
            Debug.Log("Hint has already been used.");
            return;
        }

        Dictionary<int, List<Slot>> numberSlots = new Dictionary<int, List<Slot>>();

        // Collect all tiles by their number
        foreach (var slot in slots)
        {
            if (slot.HasTile)
            {
                int tileNumber = slot.CurrentTile.GetTileNumber();
                if (!numberSlots.ContainsKey(tileNumber))
                {
                    numberSlots[tileNumber] = new List<Slot>();
                }
                numberSlots[tileNumber].Add(slot);
            }
        }

        
        foreach (var kvp in numberSlots)
        {
            if (kvp.Value.Count == 2)
            {
                int matchingTileNumber = kvp.Key;

                // Step 3: Remove two tiles from the container with the same number
                for (int i = 0; i < 2; i++)
                {
                    Slot slotToClear = kvp.Value[i];
                    slotToClear.CurrentTile.DestroyTile();
                    slotToClear.RemoveTile();
                    hintUsed = true;
                    solveButton.interactable = false;
                }

                // Step 4: Find a matching tile in the play area and remove it
                Tile playAreaTile = FindMatchingTileInPlayArea(matchingTileNumber);
                if (playAreaTile != null)
                {
                    playAreaTile.DestroyTile();
                }

                // Stop the loop once a match is found and handled
                break;

            }
        }

        Debug.Log("No matching tiles found for a hint.");
    }
    private Tile FindMatchingTileInPlayArea(int tileNumber)
    {
        Tile[] playAreaTiles = FindObjectsOfType<Tile>(); // Find all Tile objects in the play area
        List<Tile> matchingTiles = new List<Tile>();

        // Step 5: Find all tiles in the play area that match the tile number
        foreach (var tile in playAreaTiles)
        {
            if (tile.GetTileNumber() == tileNumber)
            {
                matchingTiles.Add(tile);
            }
        }

        // Step 6: Pick a random tile from the matching ones
        if (matchingTiles.Count > 0)
        {
            int randomIndex = Random.Range(0, matchingTiles.Count);
            return matchingTiles[randomIndex];
        }

        return null; // No matching tile found
    }

}