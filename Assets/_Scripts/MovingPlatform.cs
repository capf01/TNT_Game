using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pontoA; // Ponto inicial
    public Transform pontoB; // Ponto final
    public float velocidade = 1f; // Velocidade da plataforma

    private Vector3 direcao; // Direção do movimento
    private List<Transform> objetosNaPlataforma = new List<Transform>(); // Lista de objetos na plataforma

    void Start()
    {
        // Inicializar direção de movimento
        direcao = (pontoB.position - pontoA.position).normalized;
    }

    void Update()
    {
        // Movimenta a plataforma
        Vector3 movimento = direcao * velocidade * Time.deltaTime;
        transform.position += movimento;

        // Move os objetos na plataforma
        foreach (Transform obj in objetosNaPlataforma)
        {
            obj.position += movimento;
        }

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
        // Adiciona o objeto à lista se ele colidir com a plataforma
        if (collision.collider.CompareTag("Player") || collision.collider.CompareTag("Carregavel"))
        {
            objetosNaPlataforma.Add(collision.transform);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Remove o objeto da lista se ele sair da plataforma
        if (collision.collider.CompareTag("Player") || collision.collider.CompareTag("Carregavel"))
        {
            objetosNaPlataforma.Remove(collision.transform);
        }
    }
}
