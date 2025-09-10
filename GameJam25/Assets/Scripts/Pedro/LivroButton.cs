using UnityEngine;
using UnityEngine.UI;

public class LivroButton : MonoBehaviour
{
    [SerializeField] Image bookImage;
    public static LivroButton instance;
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
        bookImage.gameObject.SetActive(false);
    }
    void Start()
    {
        int minigame = PlayerPrefs.GetInt("MinigamesCompleted", 0);
        if(minigame > 0)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    public void SetActive(bool state)
    {
        gameObject.SetActive(state);
    }
    public void OnClickLivrinho()
    {
        bookImage.gameObject.SetActive(true);
        LivroController.instance.PassarPag();
    }
}
