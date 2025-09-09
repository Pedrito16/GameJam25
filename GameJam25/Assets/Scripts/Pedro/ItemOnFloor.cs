using TMPro;
using UnityEngine;

public class ItemOnFloor : MonoBehaviour, IInteractable
{
    [Header("SLOT DO ITEM")]
    [SerializeField] Item item;

    TextMeshProUGUI CollectText;
    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = item.itemSprite;
        CollectText = GetComponentInChildren<TextMeshProUGUI>();
    }
    public void EnterRadius()
    {
        CollectText.text = "<Color=black>E</Color> - Coletar";
        print("Interagindo");
    }

    public void Interact()
    {
        InventoryManager.instance.AddItem(item, transform.position);
        Destroy(gameObject);
    }

    public void LeaveRadius()
    {
        CollectText.text = "";
    }
}
