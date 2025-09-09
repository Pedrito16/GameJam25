using UnityEngine;
using TMPro;

public class LockCameraMinigame : MonoBehaviour, IInteractable
{
    [SerializeField] Transform lockPlace;
    [SerializeField] TextMeshProUGUI text;
    void Start()
    {

    }
    public void EnterRadius()
    {
        text.gameObject.SetActive(true);
        text.text = "Color=yellow>E</Color> - Consertar";
    }

    public void Interact()
    {
        throw new System.NotImplementedException();
    }

    public void LeaveRadius()
    {
        text.gameObject.SetActive(false);
    }
}
