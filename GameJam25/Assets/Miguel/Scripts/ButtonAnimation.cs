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
        // Guarda a escala original do bot�o
        originalScale = transform.localScale;

        // Obt�m o componente Button
        button = GetComponent<Button>();

        // Se n�o houver componente Button, adiciona um
        if (button == null)
        {
            button = gameObject.AddComponent<Button>();
        }
    }

    // Detecta quando o bot�o � pressionado
    public void OnPointerDown(PointerEventData eventData)
    {
        if (isInteractable && button.interactable)
        {
            StopAllCoroutines();
            StartCoroutine(ScaleButton(originalScale * scaleDownFactor));
        }
    }

    // Detecta quando o bot�o � liberado
    public void OnPointerUp(PointerEventData eventData)
    {
        if (isInteractable && button.interactable)
        {
            StopAllCoroutines();
            StartCoroutine(ScaleButton(originalScale));
        }
    }

    // Corrotina para animar a escala do bot�o
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

    // M�todo p�blico para desativar a anima��o temporariamente
    public void SetInteractable(bool value)
    {
        isInteractable = value;
        if (!value)
        {
            transform.localScale = originalScale;
        }
    }
}