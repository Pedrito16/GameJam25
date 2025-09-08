using UnityEngine;
using TMPro;

public class TextFollowMouse : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Vector3 offset;
    bool canFollow;
    public static TextFollowMouse instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    void Start()
    {
        EnableOrDisableFollow(false);
    }
    public void EnableOrDisableFollow(bool enable)
    {
        canFollow = enable;
        text.gameObject.SetActive(enable);
    }
    public void ChangeText(string newText, Color newColor)
    {
        text.text = newText;
        text.color = newColor;
    }
    void Update()
    {
        if (!canFollow) return;

        Vector3 mousePos = Input.mousePosition;
        text.transform.position = mousePos + offset;
    }
}
