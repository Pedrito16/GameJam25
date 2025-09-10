using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] float interactRange = 1f;
    [SerializeField] LayerMask interactLayer;
    [SerializeField] Collider2D objInRadius;
    bool enteredRadius = false;
    void Start()
    {
        
    }
    void Update()
    {
        Collider2D radius = Physics2D.OverlapCircle(transform.position, interactRange, interactLayer);

        if (radius == null && objInRadius == null) return;

        else if(radius == null && objInRadius != null)
        {
            objInRadius.GetComponent<IInteractable>()?.LeaveRadius();
            objInRadius = null;
            enteredRadius = false;
            return;
        }

        IInteractable interactable = radius.GetComponent<IInteractable>();
        OnEnterRadius(interactable);

        if (Input.GetKeyDown(KeyCode.E))
        {
            interactable?.Interact();
        }
        objInRadius = radius;
    }
    void OnEnterRadius(IInteractable interactable)
    {
        if (enteredRadius) return;
        print("No raio");
        interactable?.EnterRadius();
        enteredRadius = true;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }
}
