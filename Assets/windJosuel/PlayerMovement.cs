using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // Velocidade de movimento do jogador
    private Rigidbody2D rb;
    private Vector2 windForce; // Força do vento aplicada ao jogador

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Movimentação do jogador
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(horizontal, vertical);

        // Adicionar vento ao movimento do jogador
        rb.velocity = movement * speed + windForce;
    }

    public void SetWindForce(Vector2 force)
    {
        windForce = force;
    }

    // Detecta a colisão com a linha de chegada
    void OnTriggerEnter2D(Collider2D other)
    {
        // Verifique se o objeto que colidiu tem a tag "FinishLine"
        if (other.CompareTag("FinishLine"))
        {
            // Obter o índice da cena atual
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            // Carregar o próximo nível (próximo índice)
            int nextSceneIndex = currentSceneIndex + 1;

            // Verifica se o próximo índice existe (evita erro ao tentar carregar uma cena inexistente)
            if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(nextSceneIndex); // Carregar o próximo nível
            }
            else
            {
                Debug.Log("Você completou todos os níveis!"); // Ou qualquer outra lógica para o fim do jogo
            }
        }
    }
}
