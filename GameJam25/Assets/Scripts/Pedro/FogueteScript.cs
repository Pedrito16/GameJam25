using UnityEngine;
using TMPro;

public class FogueteScript : MonoBehaviour, IInteractable
{
    [SerializeField] TextMeshProUGUI Etext;
    [SerializeField] TextMeshProUGUI Ttext;
    [SerializeField] bool isOnRadius;
    [SerializeField] AudioSource completeSound; //configurar som quando depositar

    [SerializeField] int totalScore;
    [SerializeField] int[] scoreType;

    void Start()
    {
        scoreType = new int[3];
    }
    public void EnterRadius()
    {
        isOnRadius = true;
        Etext.text = "<Color=yellow>E</color> - Depositar";

        Ttext.text = "<Color=green>T</color> - Lançar";
    }

    public void Interact()
    {
        InventoryManager inventory = InventoryManager.instance;
        if (!inventory.HasItems())
        {
            MainTextController.instance.WriteText("Não possuí nada!", Color.red);
            return;
        }

        Item[] items = inventory.SlotsWithItem();
        for (int i = 0; i < items.Length; i++)
        {
            Item item = items[i];
            scoreType[(int)item.tipoDeItem] = item.score;
        }
        inventory.ClearInventory();
    }

    public void LeaveRadius()
    {
        Etext.text = "";
        Ttext.text = "";
    }

    void Update()
    {
        if(isOnRadius && Input.GetKeyDown(KeyCode.T))
        {
            //lógica de acabar o jogo(cutscene)
        }
    }
}
