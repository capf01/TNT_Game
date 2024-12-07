using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotationSpeed = 50f; // Velocidade de rota��o em graus por segundo

    void Update()
    {
        // Aplica a rota��o no eixo Z
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}