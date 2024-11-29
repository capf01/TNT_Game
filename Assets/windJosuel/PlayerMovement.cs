using UnityEngine;

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
}
