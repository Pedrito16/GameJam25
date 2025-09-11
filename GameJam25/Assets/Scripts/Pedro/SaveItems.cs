using UnityEngine;
using System.IO;

[System.Serializable]

public class Items
{
    public bool[] itemsState;
}
public class SaveItems : MonoBehaviour
{
    [SerializeField] ItemOnFloor[] itemsOnFloor;
    [SerializeField] string caminho;
    private void Awake()
    {
        itemsOnFloor = GetComponentsInChildren<ItemOnFloor>();
    }
    void Start()
    {
        for(int i = 0; i < itemsOnFloor.Length; i++)
        {
            itemsOnFloor[i].id = i + 1;
        }
        caminho = Application.persistentDataPath + caminho;
        Carregar();
    }
    public void Salvar()
    {
        Items items = new Items();
        items.itemsState = new bool[itemsOnFloor.Length];
        bool[] itemsState = items.itemsState;

        for(int i = 0; i < itemsOnFloor.Length; i++)
        {
            itemsState[i] = itemsOnFloor[i].item.hasBeenTaken;
        }
        items.itemsState = itemsState;
        string json = JsonUtility.ToJson(items, true);
        File.WriteAllText(caminho, json);
        print(caminho);
    }
    public void Carregar()
    {
        Items items = new Items();
        if (File.Exists(caminho))
        {
            string json = File.ReadAllText(caminho);
            items = JsonUtility.FromJson<Items>(json);
        }
        else return;

        for (int i = 0; i < items.itemsState.Length; i++)
        {
            ItemOnFloor itemOnFloor = itemsOnFloor[i];
            itemOnFloor.item.hasBeenTaken = items.itemsState[i];
            if (items.itemsState[i])
            {
                itemOnFloor.gameObject.SetActive(false);
            }
        }
    }
}
