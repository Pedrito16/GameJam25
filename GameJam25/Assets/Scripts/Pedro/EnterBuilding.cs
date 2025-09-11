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
    [SerializeField] bool sairOuEntrar = false;
    [SerializeField] UnityEvent onLoadScene;
    public void EnterRadius()
    {
        if (!condition) return;

        text.gameObject.SetActive(true);

        if (!sairOuEntrar)
            text.text = "<color=yellow>E</color> - Sair";
        else text.text = "<color=yellow>E</color> - Entrar";
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
            PlayerPrefs.SetInt("AlreadyActivated", 1);
            onLoadScene?.Invoke();
            if (usePlayerPos)
                PassInfo.instance.LoadPlayerPos();
            SceneManager.LoadScene(sceneName);
        }
    }
}
