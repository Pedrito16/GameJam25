using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class FogueteScript : MonoBehaviour, IInteractable
{
    [SerializeField] TextMeshProUGUI Etext;
    [SerializeField] TextMeshProUGUI Ttext;
    [SerializeField] bool isOnRadius;
    [SerializeField] bool canInteractOnStart = false;
    [SerializeField] AudioSource completeSound; //configurar som quando depositar

    [SerializeField] int totalScore;
    [SerializeField] int[] scoreType;
    [hideInInspector] public bool canInteract;
    bool canLaunch = false;
    public static FogueteScript instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else Destroy(gameObject);
    }
    void Start()
    {
        scoreType = new int[3];
        
        int minigame = PlayerPrefs.GetInt("MinigamesCompleted", 0);
        if(minigame >= 1)
        {
            canInteract = true;
        }
        else
        {
            canInteract = canInteractOnStart;
        }

        CountdownTimer.instance.onMinuteRemaining.AddListener(() => canLaunch = true);
    }
    public void EnterRadius()
    {
        if(!canInteract) return;
        isOnRadius = true;

        Etext.text = "<Color=yellow>E</color> - Depositar";

        if(canLaunch)
            Ttext.text = "<Color=green>T</color> - Lançar";
    }

    public void Interact()
    {
        if (!canInteract) return;
        InventoryManager inventory = InventoryManager.instance;
        if (!inventory.HasItems())
        {
            MainTextController.instance.WriteText("Não possuí nada!", Color.red);
            return;
        }
        completeSound?.Play();
        Item[] items = inventory.SlotsWithItem();
        for (int i = 0; i < items.Length; i++)
        {
            Item item = items[i];
            scoreType[(int)item.tipoDeItem] = item.score;
            totalScore += item.score;
        }
        inventory.ClearInventory();
    }

    public void LeaveRadius()
    {
        if (!canInteract) return;
        Etext.text = "";
        Ttext.text = "";
    }

    void Update()
    {
        if(isOnRadius && Input.GetKeyDown(KeyCode.T) && canInteract && canLaunch)
        {
            print("Lançando foguete");
            CountdownTimer.instance.SetActive(false);
            CountdownTimer.instance.PauseTimer(true);

            LivroButton.instance.SetActive(false);
            InventoryManager.instance.SetActive(false);

            PassInfo.instance.totalScore = totalScore;
            PassInfo.instance.scoreTypes = scoreType;

            SceneManager.LoadScene("Final");
        }
    }
}
