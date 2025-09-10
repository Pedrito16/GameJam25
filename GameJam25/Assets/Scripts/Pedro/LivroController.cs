using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LivroController : MonoBehaviour
{
    [System.Serializable]
    public class Page
    {
        public TextMeshProUGUI[] texts;
        public Image[] images;
    }

    [SerializeField] Sprite[] bookSprites;
    [SerializeField] Image bookImage;

    [SerializeField] Page[] pages;
    public int pagAtual;

    public static LivroController instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        pagAtual = -1;
    }
    void Update()
    {

    }
    public void Fechar()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        pagAtual = -1;

        Player.instance.canMove = true;
        CountdownTimer.instance.PauseTimer(false);

    }
    public void PassarPag(bool somar = true)
    {
        Player.instance.canMove = false;
        CountdownTimer.instance.PauseTimer(true);

        transform.GetChild(0).gameObject.SetActive(true);

        if (somar) pagAtual++;
        else pagAtual--;

        print("Passando página: " + pagAtual);

        pagAtual = Mathf.Clamp(pagAtual, 0, pages.Length - 1);

        if (pagAtual <= 0)
        {
            bookImage.sprite = bookSprites[0];
        }
        else if (pagAtual == pages.Length - 1)
        {
            bookImage.sprite = bookSprites[2];
        }
        else if (pagAtual < pages.Length)
        {
            bookImage.sprite = bookSprites[1];
        }
        MostrarPag();
    }
    public void MostrarPag()
    {
        for (int p = 0; p < pages.Length; p++)
        {
            for (int i = 0; i < pages[p].texts.Length; i++)
            {
                pages[p].texts[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < pages[p].images.Length; i++)
            {
                pages[p].images[i].gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < pages[pagAtual].texts.Length; i++)
        {
            pages[pagAtual].texts[i].gameObject.SetActive(true);
        }
        for (int i = 0; i < pages[pagAtual].images.Length; i++)
        {
            pages[pagAtual].images[i].gameObject.SetActive(true);
        }
    }
}
