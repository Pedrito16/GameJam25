using UnityEngine;
using Unity.Cinemachine;
using TMPro;

public class LockCameraMinigame : MonoBehaviour, IInteractable
{
    [SerializeField] Transform lockPlace;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] CinemachineCamera CMcam;
    [SerializeField] CinemachineCamera CMcamNew;

    Vector3 originalPos;
    Transform originalFollow;
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

        //CMcam.Follow = null;
        //CMcam.transform.position = lockPlace.transform.position;
    }

    public void LeaveRadius()
    {
        text.gameObject.SetActive(false);
    }
}
