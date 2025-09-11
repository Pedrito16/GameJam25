using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigamesController : MonoBehaviour
{
    [SerializeField] int minigamesToComplete = 2;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] AudioSource completeSound;
    [SerializeField] AudioSource doorUnlockSound;
    int minigamesCompleted = 0;
    bool allCompleted;
    EnterBuilding interactable;
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
        //PlayerPrefs.DeleteAll();

    }
    void Start()
    {
        interactable = GetComponent<EnterBuilding>();
        interactable.text = text;
        
        interactable.sceneName = "ForaHangar";

        text.gameObject.SetActive(false);
        int minigames = PlayerPrefs.GetInt("MinigamesCompleted", 0);
        if(minigames > 0)
        {
            ActivateEverything();
            allCompleted = true;
            interactable.condition = allCompleted;
        }
    }
    void ActivateEverything()
    {
        LivroButton.instance.SetActive(true);
        InventoryManager.instance.SetActive(true);
        CountdownTimer.instance.TimerShowAndStart();
    }
    public void CheckIfAllCompleted()
    {
        minigamesCompleted++;
        completeSound?.Play();
        MainTextController.instance.WriteText("Concluído!", Color.green);

        if (minigamesCompleted >= minigamesToComplete)
        {
            print("todos os minigames concluidos");
            PlayerPrefs.SetInt("MinigamesCompleted", 1);
            allCompleted = true;
            interactable.condition = allCompleted;
            doorUnlockSound?.Play();
            LivroButton.instance.SetActive(true);
            InventoryManager.instance.SetActive(true);
            CountdownTimer.instance.TimerShowAndStart();
            MainTextController.instance.WriteText("Concluído! A porta foi liberada", Color.green);
        }
    }
}
