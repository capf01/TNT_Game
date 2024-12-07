using UnityEngine;

public class Destroy_Cesar : MonoBehaviour
{
    public KeyCode destroyKey = KeyCode.E; // Tecla para destruir o objeto
    public float raycastDistance = 3f; // Dist�ncia do Raycast
    public LayerMask layerMask; // M�scara para filtrar objetos destrut�veis

    void Update()
    {
        CheckForDestruction();
    }

    private void CheckForDestruction()
    {
        // Visualiza o Raycast no editor
        Debug.DrawRay(transform.position, transform.right * raycastDistance, Color.red);

        // Dire��o do Raycast baseada na orienta��o do GameObject
        Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;

        // Realiza o Raycast
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, raycastDistance, layerMask);

        if (hit.collider != null)
        {
            Debug.Log($"Objeto detectado: {hit.collider.gameObject.name}");

            // Verifica se o objeto tem a tag "Destructible"
            if (hit.collider.CompareTag("Destructible"))
            {
                Debug.Log("Objeto destru�vel detectado!");

                // Destruir o objeto ao pressionar a tecla
                if (Input.GetKeyDown(destroyKey))
                {
                    Debug.Log($"Destruindo objeto: {hit.collider.gameObject.name}");
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }
}
