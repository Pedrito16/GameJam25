using UnityEngine;
using UnityEngine.UI;

public class LivroButton : MonoBehaviour
{
    [SerializeField] Image bookImage;
    void Start()
    {
        
    }
    public void OnClickLivrinho()
    {
        bookImage.gameObject.SetActive(true);
        LivroController.instance.pagAtual = 0;
    }
}
