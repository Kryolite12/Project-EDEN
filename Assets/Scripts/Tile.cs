using UnityEngine;

public class Tile : MonoBehaviour
{
   
    public Vector3 startPosition;
    private Transform originalParent;
    private Renderer tileRenderer;
    private BoxCollider tileCollider;

    public int tileNumber; // Property to store the unique number or type
    public float transparency = 0.5f; // Adjust this value for transparency
    public LayerMask boxcastLayerMask; // Set this to the layers you want the boxcast to detect

    private bool isSelectable = false;


    public Color gizmoColor = Color.green; // Color of the Gizmo in the Scene view
   

    private TileRaycast tileRaycast;

    // Add references for AudioSources
    public AudioSource selectableAudio;   // Audio to play when tile is selectable
    public AudioSource notSelectableAudio; // Audio to play when tile is not selectable




    private void Start()
    {
        tileRaycast = GetComponent<TileRaycast>();
        startPosition = transform.position; // Store the start position
        originalParent = transform.parent; // Store the original parent
    }

    private void OnMouseDown()
    {

        if (tileRaycast != null && tileRaycast.isSelectable)
        {
            
            TileContainer tileContainer = FindObjectOfType<TileContainer>();
            if (tileContainer != null)
            {
                Slot availableSlot = tileContainer.GetAvailableSlot();
                if (availableSlot != null)
                {
                    if (selectableAudio != null)
                    {
                        selectableAudio.Play();
                    }
                    // Snap the tile to the available slot
                    availableSlot.AddTile(this);
                    tileContainer.AddMoveToHistory(this, availableSlot);
                    tileContainer.CheckForMatchingTiles(); // Check for matching tiles after placing
                }
            }
        }
        else
        {
            if (notSelectableAudio != null)
            {
                notSelectableAudio.Play();
            }
            // If the tile is not selectable, do nothing
            Debug.Log("Tile is not selectable due to raycast hit.");
          
        }

    }
    
    private void SetSelectable(bool state)
    {
        isSelectable = state;
    }

    private void SetTransparency(float alpha)
    {
        Color color = tileRenderer.material.color;
        color.a = alpha;
        tileRenderer.material.color = color;
    }

    public int GetTileNumber()
    {
        return tileNumber;
    }

    public void SetTileNumber(int newNumber)
    {
        tileNumber = newNumber;
    }

    public void DestroyTile()
    {
        Destroy(gameObject);
    }
}
