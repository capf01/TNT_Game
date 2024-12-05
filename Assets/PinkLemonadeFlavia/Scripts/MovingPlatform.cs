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
    }

    // Detecta quando o jogador entra na plataforma
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            jogador = collision.transform;
            jogador.SetParent(transform); // Faz o jogador seguir a plataforma
        }
    }

    // Detecta quando o jogador sai da plataforma
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            jogador.SetParent(null); // Remove o jogador da plataforma
            jogador = null;
        }
    }
}
