using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class TileRaycast : MonoBehaviour
{
    public bool isSelectable = false;
    private SpriteRenderer tileRenderer;

    public Vector3 boxSize = new Vector3(1f, 1f, 1f); // Size of the box for the box cast
    public float transparency ; // Adjust this value for transparency
    public LayerMask raycastLayerMask; // Set this to the layers you want the box cast to detect
    bool isHit = false;
    private void Start()
    {
        tileRenderer = GetComponent<SpriteRenderer>();
        SetTransparency(1.0f); // Start fully opaque
    }

    public void Update()
    {
        // Perform a box cast along the Z-axis
        Vector3 boxCenter = transform.position + Vector3.forward * (boxSize.z / 2);
        RaycastHit hit;

        if (Physics.BoxCast(boxCenter, boxSize / 2, Vector3.forward, out hit, Quaternion.identity, Mathf.Infinity, raycastLayerMask))
        {
            // If the box cast hits an object, make the tile non-selectable and semi-transparent
            SetSelectable(false);
            SetTransparency(transparency);
            isHit = true; // Mark as hit
          
        }
        else
        {
            // If the box cast doesn't hit anything, make the tile selectable
            SetSelectable(true);
            SetTransparency(1.0f);
            isHit = false; // Mark as hit
            
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

    private void OnDrawGizmos()
    {
    
        // Change Gizmos color based on whether the box cast hits something
        Gizmos.color = isHit ? Color.red : Color.green;

        Vector3 boxCenter = transform.position + Vector3.forward * (boxSize.z / 2);
        Gizmos.DrawWireCube(boxCenter, boxSize);
    }
}
