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
    public int score;
    [HideInInspector] public Color typeColor;
    public bool hasBeenTaken = false;

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
