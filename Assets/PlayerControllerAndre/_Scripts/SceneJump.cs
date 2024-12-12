using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneJump : MonoBehaviour
{
    // Defina a Layer do Player no inspector ou configure pelo código.
    [SerializeField] private string playerLayerName = "Player";

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("asdadssad");
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Obtém o índice da cena atual
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Calcula o índice da próxima cena
        int nextSceneIndex = currentSceneIndex + 1;

        // Verifica se o índice da próxima cena é válido
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            // Carrega a próxima cena
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogWarning("Não há mais cenas no Build Settings para carregar.");
        }
    }
}
