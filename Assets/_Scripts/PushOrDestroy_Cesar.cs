using UnityEngine;

public class PushOrDestroy_Cesar : MonoBehaviour
{
    public float pushForce = 5f; // Força aplicada ao empurrar objetos
    public KeyCode destroyKey = KeyCode.E; // Tecla para destruir paredes
    public float raycastDistance = 4f; // Distância do Raycast

    private void Update()
    {
      
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pushable"))
        {
            PushObject(collision);
        }
    }

    private void PushObject(Collision2D collision)
    {
        if (Input.GetKey(KeyCode.W))
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 pushDirection = (collision.transform.position - transform.position).normalized;
                rb.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
                Debug.Log("Empurrando objeto!");
            }
        }
    }
}
