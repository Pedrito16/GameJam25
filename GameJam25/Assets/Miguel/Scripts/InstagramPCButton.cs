using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Collections;

public class InstagramPCButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("Configurações do Instagram")]
    public string profileURL = "https://www.instagram.com/climatecollapse_game/";

    [Header("Configurações Visuais")]
    public Color normalColor = new Color(0.72f, 0.22f, 0.56f, 1f);
    public Color hoverColor = new Color(0.88f, 0.28f, 0.68f, 1f);
    public Color textColor = Color.white;

    [Header("Efeitos de Animação")]
    public float hoverScale = 1.1f;
    public float clickScale = 0.95f;
    public float animationSpeed = 0.2f;

    [Header("Componentes")]
    public Button buttonComponent;
    public TextMeshProUGUI buttonText;
    public Image buttonBackground;

    private Vector3 originalScale;
    private bool isInteractive = true;
    private Coroutine scaleCoroutine;
    private Coroutine colorCoroutine;

    void Start()
    {
        InitializeButton();
    }

    void InitializeButton()
    {
        if (buttonComponent == null)
            buttonComponent = GetComponent<Button>();

        if (buttonText == null)
            buttonText = GetComponentInChildren<TextMeshProUGUI>();

        if (buttonBackground == null)
            buttonBackground = GetComponent<Image>();

        if (buttonBackground != null)
        {
            buttonBackground.color = normalColor;
        }

        if (buttonText != null)
        {
            buttonText.text = "Siga-nos no Instagram";
            buttonText.color = textColor;
            buttonText.fontStyle = FontStyles.Bold;
        }

        if (buttonComponent != null)
        {
            buttonComponent.onClick.RemoveAllListeners();
            buttonComponent.onClick.AddListener(OpenInstagram);

            ColorBlock colors = buttonComponent.colors;
            colors.normalColor = normalColor;
            colors.highlightedColor = hoverColor;
            colors.pressedColor = normalColor * 0.8f;
            colors.selectedColor = hoverColor;
            buttonComponent.colors = colors;
        }

        originalScale = transform.localScale;

        Debug.Log("Botão Instagram para PC configurado!");
    }

    public void OpenInstagram()
    {
        if (!isInteractive) return;

        PlayClickAnimation();

        Application.OpenURL(profileURL);

        Debug.Log($"Abrindo Instagram: {profileURL}");
        Debug.Log("Climate Collapse - Jogo criado por alunos do @senacpalhoca - Game Jam 2025");

        StartCoroutine(TemporaryLock());
    }

    private IEnumerator TemporaryLock()
    {
        isInteractive = false;
        yield return new WaitForSeconds(1f);
        isInteractive = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isInteractive) return;

        if (scaleCoroutine != null) StopCoroutine(scaleCoroutine);
        scaleCoroutine = StartCoroutine(ScaleTo(originalScale * hoverScale, animationSpeed));

        if (colorCoroutine != null) StopCoroutine(colorCoroutine);
        colorCoroutine = StartCoroutine(ChangeColorTo(hoverColor, animationSpeed));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isInteractive) return;

        if (scaleCoroutine != null) StopCoroutine(scaleCoroutine);
        scaleCoroutine = StartCoroutine(ScaleTo(originalScale, animationSpeed));

        if (colorCoroutine != null) StopCoroutine(colorCoroutine);
        colorCoroutine = StartCoroutine(ChangeColorTo(normalColor, animationSpeed));
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        PlayClickAnimation();
    }

    private void PlayClickAnimation()
    {
        StartCoroutine(ClickAnimation());
    }

    private IEnumerator ClickAnimation()
    {
        yield return ScaleTo(originalScale * clickScale, animationSpeed * 0.3f);

        yield return ScaleTo(originalScale, animationSpeed * 0.7f);
    }

    private IEnumerator ScaleTo(Vector3 targetScale, float duration)
    {
        Vector3 startScale = transform.localScale;
        float time = 0f;

        while (time < duration)
        {
            transform.localScale = Vector3.Lerp(startScale, targetScale, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale;
    }

    private IEnumerator ChangeColorTo(Color targetColor, float duration)
    {
        if (buttonBackground == null) yield break;

        Color startColor = buttonBackground.color;
        float time = 0f;

        while (time < duration)
        {
            buttonBackground.color = Color.Lerp(startColor, targetColor, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        buttonBackground.color = targetColor;
    }

    public static void OpenClimateCollapseInstagram()
    {
        Application.OpenURL("https://www.instagram.com/climatecollapse_game/");
    }

    [ContextMenu("Testar Abertura Instagram")]
    public void TestOpenInstagram()
    {
        Debug.Log("Testando abertura do Instagram no navegador...");
        Application.OpenURL(profileURL);
    }

    void OnValidate()
    {
        if (profileURL != "https://www.instagram.com/climatecollapse_game/")
        {
            profileURL = "https://www.instagram.com/climatecollapse_game/";
        }
    }

    void OnDestroy()
    {
        if (scaleCoroutine != null) StopCoroutine(scaleCoroutine);
        if (colorCoroutine != null) StopCoroutine(colorCoroutine);
    }
}