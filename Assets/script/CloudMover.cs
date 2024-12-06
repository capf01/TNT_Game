using UnityEngine;

public class CloudMover : MonoBehaviour
{
    [Header("Configura��es de Movimento")]
    public float speed = 2f; // Velocidade do movimento
    public float leftLimit = -10f; // Limite � esquerda
    public float rightLimit = 10f; // Limite � direita
    private Vector3 direction = Vector3.left; // Dire��o inicial do movimento

    void Update()
    {
        // Move a nuvem na dire��o atual
        MoveCloud();

        // Verifica se a nuvem alcan�ou os limites
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
        // Realiza o movimento baseado na dire��o e velocidade
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void ChangeDirection(Vector3 newDirection)
    {
        // Altera a dire��o do movimento
        direction = newDirection;
    }
}
