using UnityEngine;
using UnityEngine.Tilemaps;

public class Plataforma2DCair : MonoBehaviour
{
    public float delayBeforeFalling = 0.7f;  // Tempo antes do player cair
    private bool isPlayerOnPlatform = false;  // Verifica se o player está em cima
    private Rigidbody2D rb;  // Rigidbody da plataforma para mover ela
    private TilemapCollider2D tileCollider;  // TileCollider da plataforma (substituindo o BoxCollider2D)

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // Pegando o Rigidbody2D da plataforma
        tileCollider = GetComponent<TilemapCollider2D>();  // Pegando o TilemapCollider2D
        rb.isKinematic = true;  // A plataforma começa sem ser afetada pela física
    }

    private void Update()
    {
        if (isPlayerOnPlatform)
        {
            // Se o player está em cima, inicia a contagem de tempo
            Invoke("MakePlatformFall", delayBeforeFalling);
            isPlayerOnPlatform = false;  // Evita que a contagem comece várias vezes
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Se o player entra em contato com a plataforma
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerOnPlatform = true;  // Player está em cima da plataforma
        }
    }

    private void MakePlatformFall()
    {
        rb.isKinematic = false;  // Deixa a plataforma ser afetada pela física
        rb.gravityScale = 1;  // Aplica a gravidade para a plataforma cair

        // Remover o TilemapCollider2D após a queda
        if (tileCollider != null)
        {
            Destroy(tileCollider);  // Destrói o TilemapCollider2D
        }
    }
}
