using UnityEngine;

public class WireLittleCube : MonoBehaviour
{
    public int id;
    [SerializeField] LayerMask layerMask;

    LineRenderer lineRenderer;
    bool isDragging = false;
    public bool isCompleted = false;
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }
    private void OnMouseDown()
    {
        if (isCompleted) return;

        lineRenderer.enabled = true;
        WireMinigameContrller.instance.currentDragging = this;
        isDragging = true;
    }
    private void OnMouseDrag()
    {
        if(!isDragging) return;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, mousePos);
    }
    private void OnMouseUp()
    {
        if (!isDragging) return;
        isDragging = false;
        WireMinigameContrller wireMinigame = WireMinigameContrller.instance;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D hit = Physics2D.OverlapPoint(mousePos, layerMask);

        if (hit != null)
        {
            WireReciever reciever = hit.GetComponent<WireReciever>();
            
            if (reciever != null && reciever.id == id)
            {
                Debug.Log("Certo!");
                wireMinigame.currentDragging = null;
                isCompleted = true;
                wireMinigame.CheckIfAllEnded();
            }
            else
            {
                Debug.Log("Errado!");
                ResetWirePos();
                wireMinigame.currentDragging = null;
            }
        }
        else
        {
            Debug.Log("Nada");
            WireMinigameContrller.instance.currentDragging = null;
            ResetWirePos();
        }
    }
    void ResetWirePos()
    {
        lineRenderer.enabled = false;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.position);
    }
}
