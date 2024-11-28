using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManage : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Demo"); // Carrega a cena chamada "Game"
    }

    public void OpenCredits()
    {
        SceneManager.LoadScene("OpenCredits"); // Carrega a cena chamada "Credits"
    }

    public void OpenControls()
    {
        SceneManager.LoadScene("Controll"); // Carrega a cena chamada "Control"
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
