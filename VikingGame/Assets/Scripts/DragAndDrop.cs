using UnityEngine;
using System.Collections;

public class DragAndDrop : MonoBehaviour {
    public System.Type TargetType;

    private bool colliderAdded = false;
    private Vector3 initialPosition;
    private bool isDragging = false;

    void Start()
    {
        TargetType = typeof(DropZone);
        
        if (this.GetComponent<Collider2D>() == null)
        {
            BoxCollider2D col = this.gameObject.AddComponent<BoxCollider2D>();
            col.size = new Vector2(100, 100);
            colliderAdded = true;
        }
    }

    public void BeginDrag()
    {
        //Save initial Position
        initialPosition = this.transform.position;
    }

    public void OnDrag()
    {
        isDragging = true;

        DoDrag();
    }

    public void EndDrag()
    {
        if (isDragging)
        {
            isDragging = false;

            DoDrop();
        }
    }

    void DoDrag()
    {
        Vector3 pos = Input.mousePosition;
        pos.x = Mathf.Clamp(pos.x, 0, Screen.width);
        pos.y = Mathf.Clamp(pos.y, 0, Screen.height);
        this.transform.position = pos;
    }

    bool DoDrop()
    {
        RaycastHit2D[] infos = Physics2D.RaycastAll(Input.mousePosition, Vector2.zero);
        foreach (RaycastHit2D info in infos)
        {
            if (info.collider != null)
            { 
                    //Test if we've dropped on something of target type
                    if (info.collider.gameObject.GetComponent(TargetType) != null)
                    {
                        //Can drop!!
                        this.transform.position = info.collider.transform.position;
                        return true;
                    }
            }
        }

        //No hit
        this.transform.position = initialPosition;
        return false;
    }

    void OnDestroy()
    {
        if (colliderAdded)
        {
            Destroy(this.GetComponent<Collider2D>());
        }
    }
}
