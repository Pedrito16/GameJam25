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
        SetActive(false);
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
