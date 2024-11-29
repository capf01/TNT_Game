using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManage : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Demo"); 
    }

    public void OpenCredits()
    {
        SceneManager.LoadScene("OpenCredits");  
    }

    public void OpenControls()
    {
        SceneManager.LoadScene("Controll"); 
    }
    public void Menu()
    {
        SceneManager.LoadScene("Menu");  
    }

    public void ExitGames()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Para no editor
#else
        Application.Quit(); // Sai do jogo na build
#endif
    }
}
