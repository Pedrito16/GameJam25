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


    [SerializeField] Image bookImage;
    public int pagAtual;
    [SerializeField] Page[] pages;
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

    }
    void Update()
    {

    }
    void PassarPag()
    {
        //if (pagAtual < firstPageTexts.Length - 1)
        //{
        //    firstPageTexts[pagAtual].gameObject.SetActive(false);
        //    pagAtual++;
        //    firstPageTexts[pagAtual].gameObject.SetActive(true);
        //}
        //else
        //{
        //    bookImage.gameObject.SetActive(false);
        //    firstPageTexts[pagAtual].gameObject.SetActive(false);
        //}
    }
}
