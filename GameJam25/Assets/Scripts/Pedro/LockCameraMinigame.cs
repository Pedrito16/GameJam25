using UnityEngine;
using Unity.Cinemachine;
using TMPro;

public class LockCameraMinigame : MonoBehaviour, IInteractable
{
    [SerializeField] Transform lockPlace;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] CinemachineCamera CMcamNew;
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
        CMcamNew.Priority = 2;
        CountdownTimer.instance.canCount = false;
        Player.instance.canMove = false;
    }
    public void Deactivate()
    {
        particle.SetActive(false);
        text.gameObject.SetActive(false);
        CMcamNew.Priority = 0;
        CountdownTimer.instance.canCount = true;
        Player.instance.canMove = true;
        Destroy(this);
    }

    public void LeaveRadius()
    {
        text.gameObject.SetActive(false);
    }
}
