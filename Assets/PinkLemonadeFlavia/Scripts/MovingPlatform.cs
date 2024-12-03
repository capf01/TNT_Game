using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pontoA; // Ponto inicial
    public Transform pontoB; // Ponto final
    public float velocidade = 1f; // Velocidade da plataforma

    private Vector3 direcao; // Direção do movimento
    private Transform player; // Referência ao player para mover junto com a plataforma
    private bool playerSobrePlataforma = false; // Verifica se o player está na plataforma

    void Start()
    {
        // Inicializar direção de movimento
        direcao = (pontoB.position - pontoA.position).normalized;
    }

    void Update()
    {
        // Movimenta a plataforma
        transform.position += direcao * velocidade * Time.deltaTime;

        // Verifica os limites e inverte a direção
        if (Vector2.Distance(transform.position, pontoB.position) < 0.1f)
        {
            direcao = (pontoA.position - pontoB.position).normalized;
        }
        else if (Vector2.Distance(transform.position, pontoA.position) < 0.1f)
        {
            direcao = (pontoB.position - pontoA.position).normalized;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica se o player entrou na plataforma
        if (collision.collider.CompareTag("Player"))
        {
            player = collision.transform; // Armazena a referência ao player
            player.SetParent(transform); // Faz o player seguir a plataforma
            playerSobrePlataforma = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Verifica se o player saiu da plataforma
        if (collision.collider.CompareTag("Player"))
        {
            player.SetParent(null); // Remove o vínculo com a plataforma
            player = null; // Limpa a referência ao player
            playerSobrePlataforma = false;
        }
    }
}
