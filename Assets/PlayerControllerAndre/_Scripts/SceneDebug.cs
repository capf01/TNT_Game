using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDebug : MonoBehaviour
{
    void Update()
    {
        // Avança para a próxima cena quando a tecla M é pressionada
        if (Input.GetKeyDown(KeyCode.M))
        {
            LoadNextScene();
        }

        // Retrocede para a cena anterior quando a tecla N é pressionada
        if (Input.GetKeyDown(KeyCode.N))
        {
            LoadPreviousScene();
        }
    }

    void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        // Verifica se a próxima cena está dentro do índice válido
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("Já está na última cena!");
        }
    }

    void LoadPreviousScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int previousSceneIndex = currentSceneIndex - 1;

        // Verifica se a cena anterior está dentro do índice válido
        if (previousSceneIndex >= 0)
        {
            SceneManager.LoadScene(previousSceneIndex);
        }
        else
        {
            Debug.Log("Já está na primeira cena!");
        }
    }
}
