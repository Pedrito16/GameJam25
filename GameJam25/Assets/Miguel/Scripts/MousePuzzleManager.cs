// MousePuzzleManager.cs
using UnityEngine;
using UnityEngine.Events;

public class MousePuzzleManager : MonoBehaviour
{
    [Header("Referências")]
    public MousePuzzleController mouseController;
    public GameObject victoryScreen; // Tela de vitória (no mundo)

    [Header("Configurações")]
    public float victoryScreenDelay = 1f;
    public Vector3 victoryScreenPosition; // Posição no mundo para aparecer a tela

    [Header("Eventos")]
    public UnityEvent onPuzzleStart;
    public UnityEvent onPuzzleComplete;

    void Start()
    {
        if (mouseController != null)
        {
            // Conecta os eventos
            mouseController.onWin.AddListener(OnPuzzleWon);
            mouseController.onReset.AddListener(OnMouseReset);
        }

        // Inicia o puzzle
        onPuzzleStart?.Invoke();
    }

    void OnPuzzleWon()
    {
        // Mostra tela de vitória após delay
        Invoke("ShowVictoryScreen", victoryScreenDelay);

        // Dispara evento de conclusão
        onPuzzleComplete?.Invoke();
    }

    void OnMouseReset()
    {
        // Aqui você pode adicionar lógica adicional quando o mouse reseta
        Debug.Log("Mouse foi resetado");
    }

    void ShowVictoryScreen()
    {
        if (victoryScreen != null)
        {
            GameObject screen = Instantiate(victoryScreen, victoryScreenPosition, Quaternion.identity);
            screen.SetActive(true);
        }
    }

    // Método para reiniciar o puzzle completamente
    public void ResetPuzzle()
    {
        if (mouseController != null)
        {
            mouseController.ResetMouse();
            mouseController.EnableMovement();
        }

        // Esconde tela de vitória se existir
        if (victoryScreen != null)
        {
            victoryScreen.SetActive(false);
        }
    }
}