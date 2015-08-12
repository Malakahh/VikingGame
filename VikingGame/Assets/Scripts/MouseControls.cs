using UnityEngine;
using System.Collections;

public class MouseControls : MonoBehaviour {
    public GameObject HoverHighlight;

    int worldMapLayer = -1;
    RaycastHit2D hit;

    void Start()
    {
        worldMapLayer = LayerMask.NameToLayer("WorldMap");
    }

    void Update()
    {
        MouseOverHighlight();
        Visit();
    }

    void MouseOverHighlight()
    {
        hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null)
        {
            HoverHighlight.transform.position = hit.collider.transform.position - Vector3.forward;
        }
        else
        {
            HoverHighlight.transform.position = Vector3.forward;
        }
    }

    void Visit()
    {
        if (Input.GetMouseButton(0) && hit.collider != null)
        {
            WorldMapHexagonTile tile = hit.collider.GetComponent<WorldMapHexagonTile>();
            if (tile != null && !tile.FogOfWar && !tile.Visited)
            {
                tile.Visited = true;
            }
        }
    }
}
