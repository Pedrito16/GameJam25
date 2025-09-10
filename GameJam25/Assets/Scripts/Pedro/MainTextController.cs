using System.Collections;
using TMPro;
using UnityEngine;

public class MainTextController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI mainText;
    public static MainTextController instance;
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
        mainText.text = "";
    }
    public void WriteText(string text, Color textColor)
    {
        mainText.text = text;
        mainText.color = textColor;
        StartCoroutine(ClearText());
    }
    IEnumerator ClearText()
    {
        yield return new WaitForSeconds(2f);
        mainText.text = "";
    }
}
