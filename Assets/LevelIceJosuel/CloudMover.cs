using UnityEngine;

public class CloudMover : MonoBehaviour
{
    [Header("Configurações de Movimento")]
    public float speed = 2f; // Velocidade do movimento
    public float moveDistanceRight = 5f; // Distância que a nuvem irá para a direita
    public float moveDistanceLeft = 5f; // Distância que a nuvem irá para a esquerda

    private Vector3 startPosition; // Posição inicial da nuvem
    private Vector3 targetPosition; // Posição alvo atual da nuvem
    private bool movingRight = true; // Controla se a nuvem está indo para a direita

    void Start()
    {
        // Define a posição inicial e o primeiro destino
        startPosition = transform.position;
        targetPosition = startPosition + Vector3.right * moveDistanceRight;
    }

    void Update()
    {
        MoveCloud();

        // Verifica se a nuvem alcançou o destino
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            ChangeDirection();
        }
    }

    private void MoveCloud()
    {
        // Move a nuvem em direção à posição alvo
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    private void ChangeDirection()
    {
        // Alterna entre direita e esquerda
        movingRight = !movingRight;

        if (movingRight)
        {
            targetPosition = startPosition + Vector3.right * moveDistanceRight;
        }
        else
        {
            targetPosition = startPosition + Vector3.left * moveDistanceLeft;
        }
    }
}
