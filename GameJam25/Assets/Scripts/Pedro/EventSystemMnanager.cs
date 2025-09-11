using UnityEngine;
using UnityEngine.EventSystems;

public class EventSystemMnanager : MonoBehaviour
{
    [SerializeField] EventSystem eventSystem;
    public static EventSystemMnanager instance;
    private void Awake()
    {
        if (instance == null) 
        {
            instance = this;
            DontDestroyOnLoad(transform.root);
        }else
            Destroy(gameObject);
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }
}
