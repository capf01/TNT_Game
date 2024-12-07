using UnityEngine;

public class CloudMover : MonoBehaviour
{
    [Header("Configurações de Movimento")]
    public float speed = 2f; // Velocidade do movimento
    public float leftLimit = -10f; // Limite à esquerda
    public float rightLimit = 10f; // Limite à direita
    private Vector3 direction = Vector3.left; // Direção inicial do movimento

    void Update()
    {
        // Move a nuvem na direção atual
        MoveCloud();

        // Verifica se a nuvem alcançou os limites
        if (transform.position.x <= leftLimit)
        {
            ChangeDirection(Vector3.right);
        }
        else if (transform.position.x >= rightLimit)
        {
            ChangeDirection(Vector3.left);
        }
    }

    private void MoveCloud()
    {
        // Realiza o movimento baseado na direção e velocidade
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void ChangeDirection(Vector3 newDirection)
    {
        // Altera a direção do movimento
        direction = newDirection;
    }
}
