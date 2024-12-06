using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotationSpeed = 50f; // Velocidade de rotação em graus por segundo

    void Update()
    {
        // Aplica a rotação no eixo Z
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}