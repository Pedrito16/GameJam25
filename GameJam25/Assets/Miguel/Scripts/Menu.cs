using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
public class Menu : MonoBehaviour
{
    public GameObject painelAbrirCredito;



    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void AbrirPainel()
    {
       painelAbrirCredito.SetActive(!painelAbrirCredito.activeSelf);  
      
    }

    public void QuitGame()
    {
        print("saindo do jogo");
        Application.Quit();
    }
}