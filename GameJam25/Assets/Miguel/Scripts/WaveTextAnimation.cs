using UnityEngine;
using TMPro;
using System.Collections;

public class WaveTextAnimation : MonoBehaviour
{
    [Header("Configurações da Onda")]
    public float waveHeight = 15f;
    public float waveSpeed = 2f;
    public float waveOffset = 0.3f;
    public float rotationIntensity = 5f;

    [Header("Configurações de Escala")]
    public float scaleIntensity = 0.3f;

    [Header("Cores (Opcional)")]
    public bool useColorEffect = true;
    public Gradient colorGradient;

    private TMP_Text textComponent;
    private TMP_TextInfo textInfo;
    private Vector3[][] originalVertices;
    private bool isAnimating = false;

    void Start()
    {
        textComponent = GetComponent<TMP_Text>();
        textInfo = textComponent.textInfo;

        SaveOriginalVertices();

        StartAnimation();
    }

    void SaveOriginalVertices()
    {
        textComponent.ForceMeshUpdate();
        originalVertices = new Vector3[textInfo.meshInfo.Length][];

        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            originalVertices[i] = (Vector3[])textInfo.meshInfo[i].vertices.Clone();
        }
    }

    public void StartAnimation()
    {
        if (!isAnimating)
        {
            isAnimating = true;
            StartCoroutine(AnimateWave());
        }
    }

    public void StopAnimation()
    {
        isAnimating = false;
        StopAllCoroutines();
        ResetToOriginal();
    }

    private IEnumerator AnimateWave()
    {
        while (isAnimating)
        {
            textComponent.ForceMeshUpdate();

            if (textInfo.characterCount == 0)
            {
                yield return null;
                continue;
            }

            for (int i = 0; i < textInfo.characterCount; i++)
            {
                if (!textInfo.characterInfo[i].isVisible) continue;

                int vertexIndex = textInfo.characterInfo[i].vertexIndex;
                int materialIndex = textInfo.characterInfo[i].materialReferenceIndex;

                Vector3[] sourceVertices = originalVertices[materialIndex];
                Vector3[] vertices = textInfo.meshInfo[materialIndex].vertices;

                float waveTime = Time.time * waveSpeed + i * waveOffset;
                float waveFactor = Mathf.Sin(waveTime) * waveHeight;
                float scaleFactor = 1f + Mathf.Abs(Mathf.Sin(waveTime)) * scaleIntensity;
                float rotationFactor = Mathf.Sin(waveTime) * rotationIntensity;

                Vector3 charCenter = (sourceVertices[vertexIndex] + sourceVertices[vertexIndex + 2]) / 2f;

                for (int j = 0; j < 4; j++)
                {
                    Vector3 vertex = sourceVertices[vertexIndex + j];

                    vertex.y += waveFactor;

                    vertex = charCenter + (vertex - charCenter) * scaleFactor;

                    float angle = rotationFactor * Mathf.Deg2Rad;
                    float cos = Mathf.Cos(angle);
                    float sin = Mathf.Sin(angle);

                    Vector2 rotated = new Vector2(
                        cos * (vertex.x - charCenter.x) - sin * (vertex.y - charCenter.y) + charCenter.x,
                        sin * (vertex.x - charCenter.x) + cos * (vertex.y - charCenter.y) + charCenter.y
                    );

                    vertex.x = rotated.x;
                    vertex.y = rotated.y;

                    vertices[vertexIndex + j] = vertex;
                }

                if (useColorEffect && colorGradient != null)
                {
                    Color32[] vertexColors = textInfo.meshInfo[materialIndex].colors32;
                    Color waveColor = colorGradient.Evaluate(Mathf.PingPong(waveTime, 1f));

                    for (int j = 0; j < 4; j++)
                    {
                        vertexColors[vertexIndex + j] = waveColor;
                    }
                }
            }

            for (int i = 0; i < textInfo.meshInfo.Length; i++)
            {
                textInfo.meshInfo[i].mesh.vertices = textInfo.meshInfo[i].vertices;

                if (useColorEffect)
                {
                    textInfo.meshInfo[i].mesh.colors32 = textInfo.meshInfo[i].colors32;
                }

                textComponent.UpdateGeometry(textInfo.meshInfo[i].mesh, i);
            }

            yield return null;
        }
    }

    private void ResetToOriginal()
    {
        textComponent.ForceMeshUpdate();

        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            if (originalVertices[i] != null)
            {
                originalVertices[i].CopyTo(textInfo.meshInfo[i].vertices, 0);
                textInfo.meshInfo[i].mesh.vertices = textInfo.meshInfo[i].vertices;
                textComponent.UpdateGeometry(textInfo.meshInfo[i].mesh, i);
            }
        }
    }

    void OnEnable()
    {
        if (isAnimating)
        {
            StartAnimation();
        }
    }

    void OnDisable()
    {
        StopAnimation();
    }

    void OnDestroy()
    {
        StopAllCoroutines();
    }

    public void UpdateText(string newText)
    {
        StopAnimation();
        textComponent.text = newText;
        SaveOriginalVertices();
        StartAnimation();
    }
}