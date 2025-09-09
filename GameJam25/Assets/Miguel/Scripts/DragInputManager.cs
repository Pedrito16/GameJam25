// DragInputManager.cs
using UnityEngine;

public class DragInputManager : MonoBehaviour
{
    [Header("Configurações")]
    public LayerMask draggableLayer;
    public float clickRadius = 0.5f;

    private DragAndDropPuzzle currentlyDragging;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        HandleGlobalInput();
    }

    void HandleGlobalInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryStartDrag();
        }

        if (Input.GetMouseButtonUp(0))
        {
            EndCurrentDrag();
        }
    }

    void TryStartDrag()
    {
        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Collider2D[] hits = Physics2D.OverlapCircleAll(mousePos, clickRadius, draggableLayer);

        foreach (Collider2D hit in hits)
        {
            DragAndDropPuzzle dragObject = hit.GetComponent<DragAndDropPuzzle>();
            if (dragObject != null)
            {
                currentlyDragging = dragObject;
                break;
            }
        }
    }

    void EndCurrentDrag()
    {
        if (currentlyDragging != null)
        {
            currentlyDragging = null;
        }
    }

    public bool IsDragging(DragAndDropPuzzle obj)
    {
        return currentlyDragging == obj;
    }
}