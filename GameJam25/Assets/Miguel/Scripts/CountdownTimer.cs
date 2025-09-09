using UnityEngine;
using TMPro;
using System.Collections;

public class CountdownTimer : MonoBehaviour
{
    [Header("Configurações do Timer")]
    public float startMinutes = 5f;
    public bool startOnAwake = true;

    [Header("Referência do Text Mesh Pro")]
    public TextMeshProUGUI timerText;

    [Header("Cores e Efeitos")]
    public Color normalColor = Color.white;
    public Color dangerColor = Color.red;
    public float blinkSpeed = 2f;

    private float currentTime;
    private bool isTimerRunning = false;
    private bool isBlinking = false;
    private Coroutine timerCoroutine;
    private Coroutine blinkCoroutine;

    void Start()
    {
        InitializeTimer();

        if (startOnAwake)
        {
            StartTimer();
        }
    }

    void InitializeTimer()
    {
        currentTime = startMinutes * 60f;
        UpdateTimerDisplay();
    }

    public void StartTimer()
    {
        if (isTimerRunning) return;

        isTimerRunning = true;
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
        }
        timerCoroutine = StartCoroutine(TimerCountdown());
    }

    public void StopTimer()
    {
        isTimerRunning = false;
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
        }
        StopBlinking();
    }

    public void ResetTimer()
    {
        StopTimer();
        currentTime = startMinutes * 60f;
        UpdateTimerDisplay();
        timerText.color = normalColor;
        timerText.alpha = 1f;
    }

    private IEnumerator TimerCountdown()
    {
        while (currentTime > 0 && isTimerRunning)
        {
            yield return new WaitForSeconds(1f);
            currentTime -= 1f;
            UpdateTimerDisplay();
            UpdateVisualEffects();
        }

        if (currentTime <= 0)
        {
            TimerEnded();
        }
    }

    private void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(currentTime / 60f);
            int seconds = Mathf.FloorToInt(currentTime % 60f);

            timerText.text = $"{minutes:00}:{seconds:00}";
        }
    }

    private void UpdateVisualEffects()
    {
        if (timerText == null) return;

        if (currentTime <= 60f && currentTime > 0f)
        {
            timerText.color = dangerColor;
            if (!isBlinking)
            {
                StartBlinking();
            }
        }
        else if (currentTime > 60f)
        {
            StopBlinking();
            timerText.color = normalColor;
            timerText.alpha = 1f;
        }
    }

    private void StartBlinking()
    {
        if (isBlinking) return;

        isBlinking = true;
        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
        }
        blinkCoroutine = StartCoroutine(BlinkEffect());
    }

    private void StopBlinking()
    {
        isBlinking = false;
        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
        }

        if (timerText != null)
        {
            timerText.alpha = 1f;
        }
    }

    private IEnumerator BlinkEffect()
    {
        while (isBlinking && currentTime > 0f)
        {
            float alpha = Mathf.PingPong(Time.time * blinkSpeed * 2f, 0.7f) + 0.3f;
            timerText.alpha = alpha;

            yield return null;
        }

        timerText.alpha = 1f;
    }

    private void TimerEnded()
    {
        isTimerRunning = false;
        StopBlinking();

        if (timerText != null)
        {
            timerText.text = "00:00";
            timerText.color = dangerColor;
            timerText.alpha = 1f;
        }

        Debug.Log("Tempo esgotado! Game Over!");


    }

    public void AddTime(float minutesToAdd)
    {
        currentTime += minutesToAdd * 60f;
        UpdateTimerDisplay();
        UpdateVisualEffects();
    }

    public bool IsTimeUp()
    {
        return currentTime <= 0f;
    }

    public float GetRemainingMinutes()
    {
        return currentTime / 60f;
    }

    public float GetRemainingSeconds()
    {
        return currentTime;
    }

    void OnDestroy()
    {
        StopAllCoroutines();
    }

    void OnEnable()
    {
        if (isTimerRunning)
        {
            StartTimer();
        }
    }

    void OnDisable()
    {
        StopTimer();
    }
}