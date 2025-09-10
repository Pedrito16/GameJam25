using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ButtonAnimation : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [Header("Animation Settings")]
    public float scaleDownFactor = 0.9f;
    public float animationDuration = 0.15f;

    private Vector3 originalScale;
    private Button button;
    private bool isInteractable = true;

    void Start()
    {
        // Guarda a escala original do botão
        originalScale = transform.localScale;

        // Obtém o componente Button
        button = GetComponent<Button>();

        // Se não houver componente Button, adiciona um
        if (button == null)
        {
            button = gameObject.AddComponent<Button>();
        }
    }

    // Detecta quando o botão é pressionado
    public void OnPointerDown(PointerEventData eventData)
    {
        if (isInteractable && button.interactable)
        {
            StopAllCoroutines();
            StartCoroutine(ScaleButton(originalScale * scaleDownFactor));
        }
    }

    // Detecta quando o botão é liberado
    public void OnPointerUp(PointerEventData eventData)
    {
        if (isInteractable && button.interactable)
        {
            StopAllCoroutines();
            StartCoroutine(ScaleButton(originalScale));
        }
    }

    // Corrotina para animar a escala do botão
    private IEnumerator ScaleButton(Vector3 targetScale)
    {
        Vector3 initialScale = transform.localScale;
        float timeElapsed = 0;

        while (timeElapsed < animationDuration)
        {
            transform.localScale = Vector3.Lerp(initialScale, targetScale, timeElapsed / animationDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale;
    }

    // Método público para desativar a animação temporariamente
    public void SetInteractable(bool value)
    {
        isInteractable = value;
        if (!value)
        {
            transform.localScale = originalScale;
        }
    }
}