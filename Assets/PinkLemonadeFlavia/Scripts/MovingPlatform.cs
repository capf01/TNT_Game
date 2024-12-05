using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pontoA; // Ponto inicial
    public Transform pontoB; // Ponto final
    public float velocidade = 2f; // Velocidade da plataforma
    private Vector3 alvoAtual; // O ponto para onde a plataforma está se movendo
    private Transform jogador; // Referência ao jogador que está sobre a plataforma
    private Rigidbody2D jogadorRigidbody; // Rigidbody do jogador para manipular a física

    void Start()
    {
        // Começa indo para o ponto B
        alvoAtual = pontoB.position;
    }

    void Update()
    {
        // Move a plataforma em direção ao alvo atual
        transform.position = Vector3.MoveTowards(transform.position, alvoAtual, velocidade * Time.deltaTime);

        // Verifica se chegou no alvo
        if (Vector3.Distance(transform.position, alvoAtual) < 0.1f)
        {
            // Troca o alvo entre ponto A e ponto B
            alvoAtual = (alvoAtual == pontoA.position) ? pontoB.position : pontoA.position;
        }

        // Se o jogador estiver em cima da plataforma, mova a física dele junto com a plataforma
        if (jogador != null && jogadorRigidbody != null)
        {
            // Move o jogador horizontalmente com a plataforma
            Vector3 movimentoPlataforma = transform.position - jogador.position;
            jogadorRigidbody.velocity = new Vector2(movimentoPlataforma.x / Time.deltaTime, jogadorRigidbody.velocity.y);
        }
    }

    // Detecta quando o jogador entra na plataforma
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica se o objeto colidido tem a Layer "PlayerLayer"
        if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerLayer"))
        {
            jogador = collision.transform;
            jogadorRigidbody = jogador.GetComponent<Rigidbody2D>(); // Obtém o Rigidbody2D do jogador
        }
    }

    // Detecta quando o jogador sai da plataforma
    private void OnCollisionExit2D(Collision2D collision)
    {
        // Verifica se o objeto colidido tem a Layer "PlayerLayer"
        if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerLayer"))
        {
            jogador = null;
            jogadorRigidbody = null; // Limpa a referência ao Rigidbody
        }
    }
}
