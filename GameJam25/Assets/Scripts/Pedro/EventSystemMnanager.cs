using UnityEngine;
using UnityEngine.EventSystems;

public class EventSystemMnanager : MonoBehaviour
{
    [SerializeField] EventSystem eventSystem;
    private void Awake()
    {
        DontDestroyOnLoad(transform.root);
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
