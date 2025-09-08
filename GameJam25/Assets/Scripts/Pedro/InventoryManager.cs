using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] GameObject sorterHierarchy;
    [SerializeField] List<Image> slots;
     
    void Start()
    {
        foreach(Transform child in sorterHierarchy.transform)
        {
            slots.Add(child.GetComponent<Image>());
        }
    }
    public void AddItem(Item item)
    {

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
        return null;
    }
}
