using UnityEngine;
public enum ItemType
{
    Semente,
    Suprimento,
    Equipamento //add mais
}

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite itemSprite;
    public ItemType tipoDeItem;
    [HideInInspector] public Color typeColor;

    public void OnValidate()
    {
        switch (tipoDeItem)
        {
            case ItemType.Semente:
                typeColor = Color.green;
                break;
            case ItemType.Suprimento:
                typeColor = Color.gray;
                break;
            case ItemType.Equipamento:
                typeColor = Color.red;
                break;
            default:
                typeColor = Color.white;
                break;
        }
    }
}
