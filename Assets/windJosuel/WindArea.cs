using UnityEngine;

public class WindArea : MonoBehaviour
{
    public Vector2 windForce; // Direção e força do vento

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            if (player != null)
            {
                player.SetWindForce(windForce);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            if (player != null)
            {
                player.SetWindForce(Vector2.zero); // Remove o vento quando sair da área
            }
        }
    }
}
