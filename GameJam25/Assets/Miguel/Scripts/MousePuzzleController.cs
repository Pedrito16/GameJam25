// MousePuzzleController.cs
using UnityEngine;
using UnityEngine.Events;

public class MousePuzzleController : MonoBehaviour
{
    [Header("Configura��es do Mouse")]
    public float mouseSpeed = 5f;
    public Transform mouseTransform; // Transform do objeto mouse
    public Vector3 startPosition; // Posi��o inicial do mouse

    [Header("Configura��es de Colis�o")]
    public LayerMask winColliderLayer; // Layer do collider de vit�ria
    public LayerMask resetColliderLayer; // Layer do collider de reset
    public float collisionCheckRadius = 0.5f; // Raio para verifica��o de colis�o

    [Header("Eventos")]
    public UnityEvent onWin; // Evento chamado quando o jogador vence
    public UnityEvent onReset; // Evento chamado quando o mouse reseta

    [Header("Feedback Visual")]
    public ParticleSystem winParticles;
    public ParticleSystem resetParticles;
    public AudioClip winSound;
    public AudioClip resetSound;

    private bool canMove = true;
    private bool hasWon = false;
    private Camera mainCamera;
    private AudioSource audioSource;
    private Vector3 targetPosition;

    void Start()
    {
        // Garante que temos uma refer�ncia para a c�mera
        mainCamera = Camera.main;

        // Configura �udio source
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Posiciona o mouse na posi��o inicial
        ResetMousePosition();
    }

    void Update()
    {
        if (!canMove || hasWon) return;

        HandleMouseMovement();
        CheckCollisions();
    }

    void HandleMouseMovement()
    {
        if (mouseTransform == null) return;

        // Converte a posi��o do mouse da tela para o mundo
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Mathf.Abs(mainCamera.transform.position.z);
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(mouseScreenPos);

        // Move suavemente o mouse para a posi��o do cursor
        targetPosition = new Vector3(mouseWorldPos.x, mouseWorldPos.y, mouseTransform.position.z);
        mouseTransform.position = Vector3.Lerp(mouseTransform.position, targetPosition, mouseSpeed * Time.deltaTime);
    }

    void CheckCollisions()
    {
        if (mouseTransform == null) return;

        // Verifica colis�o com collider de vit�ria
        Collider2D winCollider = Physics2D.OverlapCircle(mouseTransform.position, collisionCheckRadius, winColliderLayer);
        if (winCollider != null)
        {
            WinGame();
            return;
        }

        // Verifica colis�o com collider de reset
        Collider2D resetCollider = Physics2D.OverlapCircle(mouseTransform.position, collisionCheckRadius, resetColliderLayer);
        if (resetCollider != null)
        {
            ResetMouse();
            return;
        }
    }

    void WinGame()
    {
        if (hasWon) return;

        hasWon = true;
        canMove = false;

        // Feedback visual e sonoro
        if (winParticles != null)
        {
            Instantiate(winParticles, mouseTransform.position, Quaternion.identity);
        }

        if (winSound != null)
        {
            audioSource.PlayOneShot(winSound);
        }

        // Dispara evento de vit�ria
        onWin?.Invoke();

        Debug.Log("Vit�ria! Mouse chegou ao destino.");
    }

    public void ResetMouse()
    {
        if (hasWon) return;

        // Feedback visual e sonoro
        if (resetParticles != null)
        {
            Instantiate(resetParticles, mouseTransform.position, Quaternion.identity);
        }

        if (resetSound != null)
        {
            audioSource.PlayOneShot(resetSound);
        }

        // Reseta a posi��o do mouse
        ResetMousePosition();

        // Dispara evento de reset
        onReset?.Invoke();

        Debug.Log("Mouse resetado para posi��o inicial.");
    }

    void ResetMousePosition()
    {
        if (mouseTransform != null)
        {
            mouseTransform.position = startPosition;
        }
    }

    public void SetCanMove(bool canMove)
    {
        this.canMove = canMove;
    }

    public void EnableMovement()
    {
        canMove = true;
    }

    public void DisableMovement()
    {
        canMove = false;
    }

    void OnDrawGizmosSelected()
    {
        if (mouseTransform != null)
        {
            // Desenha o raio de colis�o
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(mouseTransform.position, collisionCheckRadius);

            // Desenha a posi��o inicial
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(startPosition, Vector3.one * 0.3f);
        }
    }
}