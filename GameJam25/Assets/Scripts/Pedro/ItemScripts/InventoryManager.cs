using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] GameObject sorterHierarchy;
    [SerializeField] List<Image> slots;
    [SerializeField] GameObject itemTemplate;
    [SerializeField] bool showOnStart = false;
    public static InventoryManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(transform.root);
        }
        else Destroy(gameObject);
    }
    void Start()
    {
        foreach(Transform child in sorterHierarchy.transform)
        {
            slots.Add(child.GetComponent<Image>());
        }
        SetActive(showOnStart);
    }
    public void ClearInventory()
    {
        foreach (Image slot in slots)
        {
            if (slot.transform.childCount > 0)
            {
                foreach (Transform child in slot.transform)
                {
                    Destroy(child.gameObject);
                }
            }
        }
    }
    public void SetActive(bool active)
    {
        sorterHierarchy.SetActive(active);
    }
    public void AddItem(Item item, Vector2 currentPos)
    {
        GameObject itemImage = Instantiate(itemTemplate, transform);
        Image image = itemImage.GetComponent<Image>();
        RectTransform itemRect = itemImage.GetComponent<RectTransform>();

        image.sprite = item.itemSprite;
        image.preserveAspect = true;

        itemImage.transform.SetParent(FindFirstAvailableSlot());
        itemRect.anchoredPosition = Vector3.zero;

        itemImage.GetComponent<ItemContainer>().item = item;
        itemImage.SetActive(false);

        StartCoroutine(LerpAnimation(currentPos, itemRect, item.itemSprite));
    }
    IEnumerator LerpAnimation(Vector2 currentPos, RectTransform originalObject, Sprite itemSprite)
    {
        GameObject itemImage = Instantiate(itemTemplate, transform.parent);
        RectTransform itemRect = itemImage.GetComponent<RectTransform>();
        itemImage.GetComponent<Image>().sprite = itemSprite;

        // converte world -> local no espaço do pai do item novo
        Vector3 worldPos = originalObject.position;
        Vector3 localPos = itemRect.parent.InverseTransformPoint(worldPos);

        // inicia no ponto atual
        itemRect.anchoredPosition = currentPos;

        itemRect.DOAnchorPos(localPos, 0.5f, true);
        yield return new WaitForSeconds(0.5f);
        originalObject.gameObject.SetActive(true);
        Destroy(itemRect.gameObject);
    }
    public bool HasItems()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].rectTransform.childCount > 0)
            {
                return true;
            }
        }
        return false;
    }
    public Item[] SlotsWithItem()
    {
        List<Item> itemsQuantity = new List<Item>();
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].rectTransform.childCount <= 0) continue;

            Item container = slots[i].rectTransform.GetChild(0).GetComponent<ItemContainer>().item;
            if (container != null)
            {
                itemsQuantity.Add(container);
            }
        }
        return itemsQuantity.ToArray();
    }
    public RectTransform FindFirstAvailableSlot()
    {
        for(int i = 0; i < slots.Count; i++)
        {
            if (slots[i].rectTransform.childCount == 0)
            {
                return slots[i].rectTransform;
            }
        }
        MainTextController.instance.WriteText("Inventário cheio!", Color.red);
        return null;
    }
}
