// PuzzleCollider.cs
using UnityEngine;

public class PuzzleCollider : MonoBehaviour
{
    public enum ColliderType { Win, Reset }

    [Header("Configurações do Collider")]
    public ColliderType colliderType = ColliderType.Reset;

    [Header("Feedback Visual")]
    public ParticleSystem touchParticles;
    public Color gizmoColor = Color.red;

    void OnDrawGizmos()
    {
        // Desenha um gizmo colorido baseado no tipo
        Gizmos.color = colliderType == ColliderType.Win ? Color.green : Color.red;

        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            if (collider is BoxCollider2D boxCollider)
            {
                Gizmos.DrawWireCube(transform.position + (Vector3)boxCollider.offset, boxCollider.size);
            }
            else if (collider is CircleCollider2D circleCollider)
            {
                Gizmos.DrawWireSphere(transform.position + (Vector3)circleCollider.offset, circleCollider.radius);
            }
        }
    }
}