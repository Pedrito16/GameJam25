using UnityEngine;

public class WireMinigameContrller : MonoBehaviour
{
    [SerializeField] WireLittleCube[] wires;
    public WireLittleCube currentDragging;
    public static WireMinigameContrller instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        int minigames = PlayerPrefs.GetInt("MinigamesCompleted", 0);
        if (minigames > 0)
        {
            Destroy(this);
            return;
        }
        wires = GetComponentsInChildren<WireLittleCube>();
    }
    public void CheckIfAllEnded()
    {
        bool completed = true;
        for(int i = 0; i < wires.Length; i++)
        {
            if (!wires[i].isCompleted)
            {
                completed = false;
                break;
            }
        }

        if (completed)
        {
            WireMinigameTrigger.instance.Deactivate();
            Debug.Log("Minigame Completed!");
        }
    }
    void Update()
    {
        
    }
}
