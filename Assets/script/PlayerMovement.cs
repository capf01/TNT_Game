using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // Velocidade de movimento do jogador
    private Rigidbody2D rb;
    private Vector2 windForce; // For�a do vento aplicada ao jogador

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Movimenta��o do jogador
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

    // Detecta a colis�o com a linha de chegada
    void OnTriggerEnter2D(Collider2D other)
    {
        // Verifique se o objeto que colidiu tem a tag "FinishLine"
        if (other.CompareTag("FinishLine"))
        {
            // Obter o �ndice da cena atual
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            // Carregar o pr�ximo n�vel (pr�ximo �ndice)
            int nextSceneIndex = currentSceneIndex + 1;

            // Verifica se o pr�ximo �ndice existe (evita erro ao tentar carregar uma cena inexistente)
            if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(nextSceneIndex); // Carregar o pr�ximo n�vel
            }
            else
            {
                Debug.Log("Voc� completou todos os n�veis!"); // Ou qualquer outra l�gica para o fim do jogo
            }
        }
    }
}
