using UnityEngine;

public class WireSocket : MonoBehaviour
{
    [Header("Configurações")]
    public Color socketColor = Color.white;
    public float socketSize = 0.3f;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        InitializeSocket();
    }

    private void InitializeSocket()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = socketColor;
        }

        transform.localScale = Vector3.one * socketSize;
    }

    public void SetSocketColor(Color color)
    {
        socketColor = color;
        if (spriteRenderer != null)
        {
            spriteRenderer.color = color;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = socketColor;
        Gizmos.DrawWireSphere(transform.position, socketSize);
    }
}