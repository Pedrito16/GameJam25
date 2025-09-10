using TMPro;
using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.SceneManagement;

public class InteragirComputador : MonoBehaviour, IInteractable
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] CinemachineCamera CineCam;
    public static InteragirComputador instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        text.gameObject.SetActive(false);
    }
    void Start()
    {
        DragAndDropPuzzle.instance.onWin.AddListener(OnComplete);
    }
    public void EnterRadius()
    {
        text.text = "<Color=yellow>E</color> - Usar";
        text.gameObject.SetActive(true);
    }

    public void Interact()
    {
        CineCam.Priority = 2;
        Player.instance.canMove = false;
        CountdownTimer.instance.PauseTimer(true);
    }
    public void LeaveRadius()
    {
        text.gameObject.SetActive(false);
    }
    public void OnComplete()
    {
        CineCam.Priority = 0;

        Player.instance.canMove = true;
        CountdownTimer.instance.PauseTimer(false);

        MinigamesController.instance.CheckIfAllCompleted();

        text.gameObject.SetActive(false);
        Destroy(DragAndDropPuzzle.instance.gameObject);
        Destroy(this);
    }
}
