using UnityEngine;

public class DestructibleWall : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider;
    private ParticleSystem _particleSystem;

    private void Awake()
    {
        // Obt�m os componentes necess�rios
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Attack"))
        {
            // Desativa o SpriteRenderer e o BoxCollider
            _spriteRenderer.enabled = false;
            _boxCollider.enabled = false;

            // Ativa o ParticleSystem
            if (_particleSystem != null)
            {
                _particleSystem.gameObject.SetActive(true);
                _particleSystem.Play();

                // Aguarda a anima��o das part�culas antes de destruir o objeto
                Destroy(gameObject, 2);
            }
            else
            {
                // Caso n�o tenha ParticleSystem, destr�i imediatamente
                Destroy(gameObject);
            }
        }
    }
}
