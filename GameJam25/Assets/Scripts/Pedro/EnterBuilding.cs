using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMPro;
public class EnterBuilding : MonoBehaviour, IInteractable
{
    public TextMeshProUGUI text;
    public bool condition;
    public bool usePlayerPos;
    public string sceneName;
    [SerializeField] UnityEvent onLoadScene;
    public void EnterRadius()
    {
        if (!condition) return;

        text.gameObject.SetActive(true);
        text.text = "<color=yellow>E</color> - Sair";
    }

    public void LeaveRadius()
    {
        if (!condition) return;

        text.gameObject.SetActive(false);
    }

    public void Interact()
    {
        if (condition)
        {
            onLoadScene?.Invoke();
            if (usePlayerPos)
                PassInfo.instance.LoadPlayerPos();
            SceneManager.LoadScene(sceneName);
        }
    }
}
