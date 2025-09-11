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
    [ContextMenu("Apagar Save")]
    void ApagarSave()
    {
        PlayerPrefs.DeleteKey("MinigamesCompleted");
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

        //esse if inteiro poderia ser substituido por um DELEGATE, mas é a game jam e eu to com preguiça
        if (minigamesCompleted >= minigamesToComplete)
        {
            print("todos os minigames concluidos");
            PlayerPrefs.SetInt("MinigamesCompleted", 1);
            allCompleted = true;
            interactable.condition = allCompleted;
            doorUnlockSound?.Play();
            FogueteScript.instance.canInteract = true;     
            LivroButton.instance.SetActive(true);
            InventoryManager.instance.SetActive(true);
            CountdownTimer.instance.TimerShowAndStart();
            MainTextController.instance.WriteText("Concluído! Vá adquirir recursos", Color.green);
        }
    }
}
