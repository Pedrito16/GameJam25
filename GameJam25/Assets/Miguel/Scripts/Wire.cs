using UnityEngine;

public class Wire : MonoBehaviour
{
    [Header("Referências")]
    public LineRenderer lineRenderer;
    public Transform startPoint;
    public Transform endPoint;

    [Header("Configurações")]
    public float wireWidth = 0.1f;
    public Color wireColor = Color.white;
    public Material wireMaterial;

    private Vector3 originalEndPosition;
    private bool isDragging = false;

    void Start()
    {
        InitializeWire();
    }

    private void InitializeWire()
    {
        // Cria o LineRenderer automaticamente se não existir
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
            Debug.Log("LineRenderer criado automaticamente em " + gameObject.name);
        }

        // Configura o LineRenderer
        lineRenderer.startWidth = wireWidth;
        lineRenderer.endWidth = wireWidth;
        lineRenderer.positionCount = 2;

        // Usa material padrão se não for atribuído
        if (wireMaterial == null)
        {
            wireMaterial = new Material(Shader.Find("Sprites/Default"));
            Debug.Log("Material padrão criado para " + gameObject.name);
        }
        lineRenderer.material = wireMaterial;

        // Configura posições iniciais
        if (startPoint == null || endPoint == null)
        {
            CreateDefaultPoints();
        }

        originalEndPosition = endPoint.position;
        UpdateWireVisual();
    }

    private void CreateDefaultPoints()
    {
        // Cria pontos automaticamente se não existirem
        if (startPoint == null)
        {
            GameObject startObj = new GameObject("StartPoint");
            startPoint = startObj.transform;
            startPoint.SetParent(transform);
            startPoint.localPosition = Vector3.left * 0.5f;
        }

        if (endPoint == null)
        {
            GameObject endObj = new GameObject("EndPoint");
            endPoint = endObj.transform;
            endPoint.SetParent(transform);
            endPoint.localPosition = Vector3.right * 0.5f;
        }
    }

    void Update()
    {
        UpdateWireVisual();
    }

    public void SetWireColor(Color color)
    {
        wireColor = color;
        if (lineRenderer != null)
        {
            lineRenderer.startColor = color;
            lineRenderer.endColor = color;
        }
    }

    public void UpdateEndPosition(Vector3 newPosition)
    {
        if (endPoint != null)
        {
            endPoint.position = newPosition;
            UpdateWireVisual();
        }
    }

    public void SetDragging(bool dragging)
    {
        isDragging = dragging;
    }

    public void ConnectToSocket(Vector3 socketPosition)
    {
        if (endPoint != null)
        {
            endPoint.position = socketPosition;
            UpdateWireVisual();
        }
        isDragging = false;
    }

    public void ResetPosition()
    {
        if (endPoint != null)
        {
            endPoint.position = originalEndPosition;
            UpdateWireVisual();
        }
        isDragging = false;
    }

    private void UpdateWireVisual()
    {
        if (lineRenderer != null && startPoint != null && endPoint != null)
        {
            lineRenderer.SetPosition(0, startPoint.position);
            lineRenderer.SetPosition(1, endPoint.position);
        }
    }

    public Vector3 GetEndPosition()
    {
        return endPoint != null ? endPoint.position : Vector3.zero;
    }

    void OnMouseDown()
    {
        if (!isDragging)
        {
            isDragging = true;
        }
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            UpdateEndPosition(mousePos);
        }
    }

    void OnMouseUp()
    {
        if (isDragging)
        {
            isDragging = false;
        }
    }

    // Método para debug no Editor
    void OnDrawGizmos()
    {
        if (startPoint != null && endPoint != null)
        {
            Gizmos.color = wireColor;
            Gizmos.DrawLine(startPoint.position, endPoint.position);
            Gizmos.DrawWireSphere(startPoint.position, 0.1f);
            Gizmos.DrawWireSphere(endPoint.position, 0.1f);
        }
    }
}