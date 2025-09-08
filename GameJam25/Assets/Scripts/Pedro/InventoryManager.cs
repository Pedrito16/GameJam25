using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] GameObject sorterHierarchy;
    [SerializeField] List<Image> slots;
    [SerializeField] GameObject itemTemplate;
    public static InventoryManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
    void Start()
    {
        foreach(Transform child in sorterHierarchy.transform)
        {
            slots.Add(child.GetComponent<Image>());
        }
    }
    public void AddItem(Item item)
    {
        GameObject itemImage = Instantiate(itemTemplate, transform);
        Image image = itemImage.GetComponent<Image>();

        image.sprite = item.itemSprite;
        image.preserveAspect = true;

        itemImage.transform.SetParent(FindFirstAvailableSlot());
        itemImage.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;

        itemImage.GetComponent<ItemContainer>().item = item;
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
        print("No available slots");
        return null;
    }
}
