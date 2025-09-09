// MousePuzzleManager.cs
using UnityEngine;
using UnityEngine.Events;

public class MousePuzzleManager : MonoBehaviour
{
    [Header("Refer�ncias")]
    public MousePuzzleController mouseController;
    public GameObject victoryScreen; // Tela de vit�ria (no mundo)

    [Header("Configura��es")]
    public float victoryScreenDelay = 1f;
    public Vector3 victoryScreenPosition; // Posi��o no mundo para aparecer a tela

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
        // Mostra tela de vit�ria ap�s delay
        Invoke("ShowVictoryScreen", victoryScreenDelay);

        // Dispara evento de conclus�o
        onPuzzleComplete?.Invoke();
    }

    void OnMouseReset()
    {
        // Aqui voc� pode adicionar l�gica adicional quando o mouse reseta
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

    // M�todo para reiniciar o puzzle completamente
    public void ResetPuzzle()
    {
        if (mouseController != null)
        {
            mouseController.ResetMouse();
            mouseController.EnableMovement();
        }

        // Esconde tela de vit�ria se existir
        if (victoryScreen != null)
        {
            victoryScreen.SetActive(false);
        }
    }
}