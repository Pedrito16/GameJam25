using UnityEngine;
using UnityEngine.Events;

public class DragAndDropPuzzle : MonoBehaviour
{
    [Header("Configurações do Arraste")]
    public float dragSpeed = 10f;
    public float maxDragDistance = 10f;
    public bool restrictToBounds = true;
    public Vector2 minBounds = new Vector2(-5f, -5f);
    public Vector2 maxBounds = new Vector2(5f, 5f);

    [Header("Configurações de Colisão")]
    public LayerMask winColliderLayer;
    public LayerMask resetColliderLayer;
    public float collisionCheckRadius = 0.5f;

    [Header("Eventos")]
    public UnityEvent onWin;
    public UnityEvent onReset;
    public UnityEvent onDragStart;
    public UnityEvent onDragEnd;

    [Header("Feedback Visual")]
    public ParticleSystem winParticles;
    public ParticleSystem resetParticles;
    public AudioClip winSound;
    public AudioClip resetSound;
    public AudioClip dragStartSound;
    public AudioClip dragEndSound;

    // VARIÁVEL PÚBLICA PARA ACESSO EXTERNO - CORRIGIDO
    public bool isDragging { get; private set; }

    private Vector3 startPosition;
    private bool hasWon = false;
    private Camera mainCamera;
    private AudioSource audioSource;
    private Vector3 offset;
    private Rigidbody2D rb;

    void Start()
    {
        startPosition = transform.position;
        mainCamera = Camera.main;
        isDragging = false; // Inicializa como false

        // Configura Rigidbody2D para física
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0;
            rb.freezeRotation = true;
        }

        // Configura áudio
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (hasWon) return;

        HandleDragInput();

        if (isDragging)
        {
            DragObject();
            CheckCollisions();
        }
    }

    void HandleDragInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartDrag();
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            EndDrag();
        }
    }

    void StartDrag()
    {
        // Verifica se clicou no objeto
        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Collider2D hit = Physics2D.OverlapPoint(mousePos);

        if (hit != null && hit.transform == transform)
        {
            isDragging = true; // AGORA ESTÁ PÚBLICO

            // Calcula o offset entre o clique e a posição do objeto
            offset = transform.position - mainCamera.ScreenToWorldPoint(
                new Vector3(Input.mousePosition.x, Input.mousePosition.y,
                Mathf.Abs(mainCamera.transform.position.z))
            );

            // Para o movimento físico durante o arraste
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
                rb.isKinematic = true;
            }

            // Feedback
            if (dragStartSound != null)
            {
                audioSource.PlayOneShot(dragStartSound);
            }

            onDragStart?.Invoke();
        }
    }

    void DragObject()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y,
            Mathf.Abs(mainCamera.transform.position.z));

        Vector3 targetPosition = mainCamera.ScreenToWorldPoint(mousePosition) + offset;

        // Restringe a distância máxima de arraste
        if (maxDragDistance > 0)
        {
            float distance = Vector3.Distance(startPosition, targetPosition);
            if (distance > maxDragDistance)
            {
                Vector3 direction = (targetPosition - startPosition).normalized;
                targetPosition = startPosition + direction * maxDragDistance;
            }
        }

        // Restringe aos bounds definidos
        if (restrictToBounds)
        {
            targetPosition.x = Mathf.Clamp(targetPosition.x, minBounds.x, maxBounds.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minBounds.y, maxBounds.y);
        }

        // Move suavemente o objeto
        transform.position = Vector3.Lerp(transform.position, targetPosition, dragSpeed * Time.deltaTime);
    }

    void EndDrag()
    {
        isDragging = false; // AGORA ESTÁ PÚBLICO

        // Reativa a física
        if (rb != null)
        {
            rb.isKinematic = false;
        }

        // Feedback
        if (dragEndSound != null)
        {
            audioSource.PlayOneShot(dragEndSound);
        }

        onDragEnd?.Invoke();
    }

    void CheckCollisions()
    {
        // Verifica colisão com collider de vitória
        Collider2D winCollider = Physics2D.OverlapCircle(transform.position, collisionCheckRadius, winColliderLayer);
        if (winCollider != null)
        {
            WinGame();
            return;
        }

        // Verifica colisão com collider de reset
        Collider2D resetCollider = Physics2D.OverlapCircle(transform.position, collisionCheckRadius, resetColliderLayer);
        if (resetCollider != null)
        {
            ResetObject();
            return;
        }
    }

    void WinGame()
    {
        if (hasWon) return;

        hasWon = true;
        isDragging = false;

        // Feedback visual e sonoro
        if (winParticles != null)
        {
            Instantiate(winParticles, transform.position, Quaternion.identity);
        }

        if (winSound != null)
        {
            audioSource.PlayOneShot(winSound);
        }

        // Dispara evento de vitória
        onWin?.Invoke();

        Debug.Log("Vitória! Objeto chegou ao destino.");
    }

    void ResetObject()
    {
        isDragging = false;

        // Feedback visual e sonoro
        if (resetParticles != null)
        {
            Instantiate(resetParticles, transform.position, Quaternion.identity);
        }

        if (resetSound != null)
        {
            audioSource.PlayOneShot(resetSound);
        }

        // Reseta a posição do objeto
        transform.position = startPosition;

        // Reseta a física
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

        // Dispara evento de reset
        onReset?.Invoke();

        Debug.Log("Objeto resetado para posição inicial.");
    }

    public void ForceReset()
    {
        ResetObject();
    }

    public void SetStartPosition(Vector3 newPosition)
    {
        startPosition = newPosition;
    }

    void OnDrawGizmosSelected()
    {
        // Desenha o raio de colisão
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, collisionCheckRadius);

        // Desenha a posição inicial
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(Application.isPlaying ? startPosition : transform.position, Vector3.one * 0.3f);

        // Desenha área de bounds
        if (restrictToBounds)
        {
            Gizmos.color = Color.blue;
            Vector3 center = new Vector3(
                (minBounds.x + maxBounds.x) / 2f,
                (minBounds.y + maxBounds.y) / 2f,
                transform.position.z
            );
            Vector3 size = new Vector3(
                maxBounds.x - minBounds.x,
                maxBounds.y - minBounds.y,
                0.1f
            );
            Gizmos.DrawWireCube(center, size);
        }

        // Desenha distância máxima de arraste
        if (maxDragDistance > 0)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(Application.isPlaying ? startPosition : transform.position, maxDragDistance);
        }
    }
}