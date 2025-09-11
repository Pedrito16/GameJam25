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
    [SerializeField] GameObject fundo;
    [SerializeField] Sprite[] bookSprites;
    [SerializeField] Image bookImage;

    [SerializeField] Page[] pages;
    public int pagAtual;
    [SerializeField] AudioSource pagSound;
    [SerializeField] GameObject passarSeta;
    [SerializeField] GameObject voltarSeta;

    public static LivroController instance;
    void Awake()
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
        fundo.SetActive(false);
        pagAtual = -1;
    }
    public void Fechar()
    {
        fundo.SetActive(false);
        pagAtual = -1;

        Player.instance.canMove = true;
        CountdownTimer.instance.PauseTimer(false);

    }
    public void PassarPag(bool somar = true)
    {
        voltarSeta.gameObject.SetActive(true);
        passarSeta.gameObject.SetActive(true);

        Player.instance.canMove = false;
        CountdownTimer.instance.PauseTimer(true);

        fundo.SetActive(true);

        if (somar) pagAtual++;
        else pagAtual--;

        print("Passando p�gina: " + pagAtual);

        pagAtual = Mathf.Clamp(pagAtual, 0, pages.Length - 1);

        int random = Random.Range(1, 2);
        pagSound.pitch = pagSound.pitch + (random * 0.05f);
        pagSound?.Play();

        if (pagAtual <= 0)
        {
            voltarSeta.SetActive(false);
            bookImage.sprite = bookSprites[0];
        }
        else if (pagAtual == pages.Length - 1)
        {
            bookImage.sprite = bookSprites[2];
            passarSeta.SetActive(false);
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
