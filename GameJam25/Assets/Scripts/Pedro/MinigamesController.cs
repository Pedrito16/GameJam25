using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigamesController : MonoBehaviour, IInteractable
{
    [SerializeField] int minigamesToComplete = 2;
    [SerializeField] TextMeshProUGUI text;
    int minigamesCompleted = 0;
    bool allCompleted;
    public static MinigamesController instance;
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
    }
    void Start()
    {
        text.gameObject.SetActive(false);
        int minigames = PlayerPrefs.GetInt("MinigamesCompleted", 0);
        if(minigames > 0)
        {
            allCompleted = true;
        }
    }

    public void CheckIfAllCompleted()
    {
        minigamesCompleted++;
        if(minigamesCompleted >= minigamesToComplete)
        {
            print("todos os minigames concluidos");
            PlayerPrefs.SetInt("MinigamesCompleted", 1);
        }
    }

    public void EnterRadius()
    {
        if(!allCompleted) return;

        text.gameObject.SetActive(true);
        text.text = "<color=yellow>E</color> - Sair";
    }

    public void LeaveRadius()
    {
        if (!allCompleted) return;

        text.gameObject.SetActive(false);
    }

    public void Interact()
    {
        if(allCompleted)
            SceneManager.LoadScene("ForaHangar");
    }
}
