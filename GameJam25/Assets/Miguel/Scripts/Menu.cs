using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;
using UnityEngine.UI;
public class Menu : MonoBehaviour
{
    public GameObject painelAbrirCredito;



    public void LoadScene(string sceneName)
    {
        PlayerPrefs.DeleteKey("MinigamesCompleted");
        DeletarSave();
        SceneManager.LoadScene(sceneName);
    }
    void DeletarSave()
    {
        string path = Application.persistentDataPath;
        if(Directory.Exists(path))
        {
            foreach(string file in Directory.GetFiles(path))
            {
                File.Delete(file);
            }
        }
        else
        {
            Debug.Log("Nenhum save encontrado");
        }
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