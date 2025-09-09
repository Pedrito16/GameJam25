using UnityEngine;

public class DragVisualFeedBack : MonoBehaviour
{
    [Header("Referências")]
    public DragAndDropPuzzle dragController;
    public SpriteRenderer objectSprite;

    [Header("Configurações Visuais")]
    public Color normalColor = Color.white;
    public Color dragColor = new Color(1f, 0.8f, 0.8f);
    public Color winColor = Color.green;
    public Color resetColor = Color.red;

    [Header("Escala")]
    public float dragScaleMultiplier = 1.1f;
    public float scaleSpeed = 5f;

    private Vector3 originalScale;
    private Color currentColor;

    void Start()
    {
        originalScale = transform.localScale;
        currentColor = normalColor;

        if (dragController != null)
        {
            dragController.onDragStart.AddListener(OnDragStart);
            dragController.onDragEnd.AddListener(OnDragEnd);
            dragController.onWin.AddListener(OnWin);
            dragController.onReset.AddListener(OnReset);
        }
    }

    void Update()
    {
        if (dragController == null) return;

        // Atualiza escala - AGORA FUNCIONA!
        Vector3 targetScale = dragController.isDragging ?
            originalScale * dragScaleMultiplier : originalScale;

        transform.localScale = Vector3.Lerp(
            transform.localScale,
            targetScale,
            scaleSpeed * Time.deltaTime
        );

        // Atualiza cor
        if (objectSprite != null)
        {
            objectSprite.color = Color.Lerp(
                objectSprite.color,
                currentColor,
                10f * Time.deltaTime
            );
        }
    }

    void OnDragStart()
    {
        currentColor = dragColor;
    }

    void OnDragEnd()
    {
        currentColor = normalColor;
    }

    void OnWin()
    {
        currentColor = winColor;
    }

    void OnReset()
    {
        currentColor = resetColor;
    }

    void OnDestroy()
    {
        if (dragController != null)
        {
            dragController.onDragStart.RemoveListener(OnDragStart);
            dragController.onDragEnd.RemoveListener(OnDragEnd);
            dragController.onWin.RemoveListener(OnWin);
            dragController.onReset.RemoveListener(OnReset);
        }
    }
}