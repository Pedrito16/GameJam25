using UnityEngine;
using UnityEngine.EventSystems;

public class ItemContainer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Item item;
    [SerializeField] float timeOver = 2;
    [SerializeField] bool isMouseOver = false;
    float timer = 0;
    bool canCount;
    void Start()
    {
        
    }
    void Update()
    {

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isMouseOver)
        {
            TextFollowMouse textFollowMouse = TextFollowMouse.instance;
            textFollowMouse.EnableOrDisableFollow(true);
            textFollowMouse.ChangeText(item.itemName, item.typeColor);
            isMouseOver = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(isMouseOver)
        {
            TextFollowMouse textFollowMouse = TextFollowMouse.instance;
            textFollowMouse.EnableOrDisableFollow(false);
            print("Saiu");
            isMouseOver = false;
        }
    }
}
