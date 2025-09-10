using UnityEngine;
using Unity.Cinemachine;
using TMPro;

public class LockCameraMinigame : MonoBehaviour, IInteractable
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] CinemachineCamera CinemachineCamNew;
    [SerializeField] GameObject particle;
    public static LockCameraMinigame instance;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        text.gameObject.SetActive(false);
    }
    public void EnterRadius()
    {
        text.gameObject.SetActive(true);
        text.text = "<Color=yellow>E</color> - Consertar";
    }

    public void Interact()
    {
        print("Locking camera");
        CinemachineCamNew.Priority = 2;
        CountdownTimer.instance.PauseTimer(true);
        Player.instance.canMove = false;
    }
    public void Deactivate()
    {
        CinemachineCamNew.Priority = 0;

        CountdownTimer.instance.PauseTimer(false);
        Player.instance.canMove = true;

        MinigamesController.instance.CheckIfAllCompleted();

        particle.SetActive(false);
        text.gameObject.SetActive(false);
        Destroy(this);
    }

    public void LeaveRadius()
    {
        text.gameObject.SetActive(false);
    }
}
