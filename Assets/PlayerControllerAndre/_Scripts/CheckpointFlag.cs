using UnityEngine;

public class CheckpointFlag : MonoBehaviour
{
    public Sprite activatedSprite; // Sprite que será exibido ao ativar o checkpoint
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D collider;
    private ParticleSystem particles; // Partículas como objeto filho
    private bool isActivated = false; // Impede reativação repetida

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
        particles = GetComponentInChildren<ParticleSystem>();

        if (particles != null)
            particles.Stop(); // Certifica que as partículas estão desativadas inicialmente
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isActivated && collision.CompareTag("Player"))
        {
            isActivated = true; // Marca o checkpoint como ativado
            collider.enabled = false;
            spriteRenderer.sprite = activatedSprite; // Troca o sprite

            if (particles != null)
                particles.Play(); // Ativa o sistema de partículas
        }
    }
}
