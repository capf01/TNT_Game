using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DisappearingTilemap : MonoBehaviour
{
    public float disappearDelay = 3f; // Tempo em segundos para o tilemap desaparecer após a colisão
    private Tilemap tilemap; // Referência ao Tilemap

    void Start()
    {
        tilemap = GetComponent<Tilemap>(); // Obtém o Tilemap do objeto

        if (tilemap == null)
        {
            Debug.LogError("A plataforma precisa ter um Tilemap anexado.");
        }
    }

    // Detecta a colisão com o jogador
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica se o objeto colidido é o jogador (colocar no player a tag "Player")
        if (collision.gameObject.CompareTag("Player"))
        {
            
            StartCoroutine(DisappearAfterDelay());
        }
    }

    // Coroutine que desativa o Tilemap após o tempo de delay
    IEnumerator DisappearAfterDelay()
    {
        yield return new WaitForSeconds(disappearDelay); // Espera o tempo de delay (3 segundos)
        gameObject.SetActive(false); ; // Desativa o Tilemap (a plataforma desaparece)
    }
}
