using TMPro;
using UnityEngine;

public class ItemOnFloor : MonoBehaviour, IInteractable
{
    [Header("SLOT DO ITEM")]
    public Item item;
    public int id;
    [SerializeField] TextMeshProUGUI CollectText;
    void Start()
    {
        Item newItem = Instantiate(item);
        item = newItem;

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = item.itemSprite;
        CollectText = GetComponentInChildren<TextMeshProUGUI>();
    }
    public void EnterRadius()
    {
        CollectText.text = "<Color=blue>E</color> - Pegar";
        print("Interagindo");
    }

    public void Interact()
    {
        InventoryManager.instance.AddItem(item, transform.position);
        gameObject.SetActive(false);
        item.hasBeenTaken = true;
    }

    public void LeaveRadius()
    {
        CollectText.text = "";
    }
}
