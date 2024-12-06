using UnityEngine;
using System.Collections;

public class Plataforma2DCair : MonoBehaviour
{
    public float delayTime = 3f; // Tempo de espera antes de cair
    private bool playerOnPlatform = false; // Verifica se o player está na plataforma
    private bool hasFallen = false; // Verifica se a plataforma já caiu
    private float timer = 0f; // Timer para controlar o tempo em cima da plataforma
    private Rigidbody2D rb; // Referência ao Rigidbody2D da plataforma

    void Start()
    {
        // Obtém o Rigidbody2D da plataforma
        rb = GetComponent<Rigidbody2D>();

        // Verifica se o Rigidbody2D está presente
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D não encontrado! Certifique-se de adicionar o componente Rigidbody2D ao GameObject da plataforma.");
            return; // Interrompe a execução do Start se o Rigidbody2D não estiver presente
        }

        // Desativa a gravidade inicialmente para a plataforma não cair
        rb.gravityScale = 0;
    }

    void Update()
    {
        // Se o player está na plataforma e a plataforma ainda não caiu, conta o tempo
        if (playerOnPlatform && !hasFallen)
        {
            // Incrementa o tempo do timer
            timer += Time.deltaTime;

            // Quando o tempo exceder o delayTime, a plataforma começa a cair
            if (timer >= delayTime)
            {
                StartCoroutine(CairComDelay());
                hasFallen = true; // Impede que o código de execute várias vezes
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica se o player está em cima da plataforma
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnPlatform = true; // Marca que o player está em cima da plataforma
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Reseta o timer se o player sair da plataforma antes do tempo
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnPlatform = false; // O player não está mais na plataforma
            timer = 0f; // Reseta o timer
        }
    }

    // Coroutine que espera o tempo de delay antes de ativar a gravidade
    private IEnumerator CairComDelay()
    {
        // Espera pelo tempo de delay
        yield return new WaitForSeconds(0f); // Não precisamos de mais delay aqui, pois já estamos controlando o tempo com o timer

        // Após o delay, ativa a gravidade para a plataforma cair
        rb.gravityScale = 1;
    }
}
