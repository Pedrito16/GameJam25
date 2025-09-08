using UnityEngine;

public class Occlusion : MonoBehaviour
{
    [SerializeField] float transparency = 0.5f;
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            Color c = sr.color;
            c.a = transparency;
            sr.color = c;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            Color c = sr.color;
            c.a = 1f;
            sr.color = c;
        }
    }
}
