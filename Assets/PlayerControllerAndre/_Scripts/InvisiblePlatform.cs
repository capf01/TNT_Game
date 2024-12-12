using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;

public class InvisiblePlatform : MonoBehaviour
{
    public HyperFocus hyperFocusObject; // Referência ao objeto com o script HyperFocus
    private SpriteRenderer spriteRenderer;
    private TilemapRenderer tilemapRenderer;
    private BoxCollider2D boxCollider;
    private TilemapCollider2D tilemapCollider;

    public float activationDelay = 0.5f; // Delay em segundos para ativar os componentes

    private Coroutine activationCoroutine; // Referência à corrotina atual

    void Awake()
    {
        // Obtém os componentes do SpriteRenderer e BoxCollider2D da plataforma
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        tilemapRenderer = GetComponent<TilemapRenderer>();
        tilemapCollider = GetComponent<TilemapCollider2D>();
    }

    void Update()
    {
        if (hyperFocusObject == null) return;

        bool isActive = hyperFocusObject._hyperFocusOn;

        if (isActive)
        {
            // Inicia o processo de ativação com delay
            if (activationCoroutine == null)
            {
                activationCoroutine = StartCoroutine(ActivateWithDelay());
            }
        }
        else
        {
            // Interrompe a corrotina de ativação, se estiver em execução
            if (activationCoroutine != null)
            {
                StopCoroutine(activationCoroutine);
                activationCoroutine = null;
            }

            // Desativa os componentes imediatamente
            if (spriteRenderer != null)
                spriteRenderer.enabled = false;

            if (boxCollider != null)
                boxCollider.enabled = false;

            if (tilemapRenderer != null)
                tilemapRenderer.enabled = false;

            if (tilemapCollider != null)
                tilemapCollider.enabled = false;
        }
    }

    private IEnumerator ActivateWithDelay()
    {
        // Espera pelo tempo definido em activationDelay
        yield return new WaitForSeconds(activationDelay);

        // Ativa os componentes
        if (spriteRenderer != null)
            spriteRenderer.enabled = true;

        if (boxCollider != null)
            boxCollider.enabled = true;

        if (tilemapRenderer != null)
            tilemapRenderer.enabled = true;

        if (tilemapCollider != null)
            tilemapCollider.enabled = true;

        activationCoroutine = null; // Limpa a referência à corrotina
    }
}
