using UnityEngine;
using TarodevController;

public class CheckpointFlag : MonoBehaviour
{
    public Sprite activatedSprite; // Sprite que ser� exibido ao ativar o checkpoint
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D _collider;
    private ParticleSystem particles; // Part�culas como objeto filho
    public bool isActivated = false; // Impede reativa��o repetida

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<BoxCollider2D>();
        particles = GetComponentInChildren<ParticleSystem>();

        if (particles != null)
            particles.Stop(); // Certifica que as part�culas est�o desativadas inicialmente
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController s = collision.gameObject.GetComponent<PlayerController>();

        if (!isActivated && collision.CompareTag("Player") && s != null)
        {
            s._checkpoint = gameObject.transform.position;
            isActivated = true; // Marca o checkpoint como ativado
            _collider.enabled = false;
            spriteRenderer.sprite = activatedSprite; // Troca o sprite

            if (particles != null)
                particles.Play(); // Ativa o sistema de part�culas
        }
    }
}
