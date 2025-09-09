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
        wires = GetComponentsInChildren<WireLittleCube>();
    }
    public void CheckIfAllEnded()
    {
        bool completed = false;
        for(int i = 0; i < wires.Length; i++)
        {
            if (!wires[i].isCompleted) break;
            completed = true;
        }

        if (completed)
        {
            Debug.Log("Minigame Completed!");
        }
    }
    void Update()
    {
        
    }
}
