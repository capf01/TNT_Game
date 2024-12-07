using UnityEngine;

public class ExplosionAnimation : MonoBehaviour
{
    public Sprite[] idleSprites; // Sprites alternados (2 sprites para o estado inicial)
    public float idleSwitchInterval = 0.5f; // Intervalo de troca entre os sprites
    public float idleDuration = 2f; // Tempo total de alternância antes da animação final
    public Sprite[] animationSprites; // Sprites para a animação final (10 sprites)
    public float animationFrameInterval = 0.1f; // Intervalo entre os quadros da animação final

    private SpriteRenderer spriteRenderer; // SpriteRenderer do próprio objeto
    private int idleSpriteIndex = 0; // Índice atual dos sprites idle
    private int animationSpriteIndex = 0; // Índice atual dos sprites da animação final
    private float idleTimer = 0f; // Cronômetro para o tempo de alternância
    private float animationTimer = 0f; // Cronômetro para os quadros da animação final
    private float totalIdleTime = 0f; // Cronômetro total do estado inicial
    private bool isAnimating = false; // Indica se está na animação final

    void Start()
    {
        // Obtém o SpriteRenderer do objeto
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Configura o sprite inicial (idleSprites[0], se existir)
        if (idleSprites.Length > 0)
        {
            spriteRenderer.sprite = idleSprites[0];
        }
    }

    void Update()
    {
        if (!isAnimating)
        {
            // Acumula o tempo total no estado inicial
            totalIdleTime += Time.deltaTime;
            idleTimer += Time.deltaTime;

            // Alterna entre os sprites idle
            if (idleTimer >= idleSwitchInterval)
            {
                idleTimer = 0f;
                idleSpriteIndex = (idleSpriteIndex + 1) % idleSprites.Length;
                spriteRenderer.sprite = idleSprites[idleSpriteIndex];
            }

            // Transição para a animação final após idleDuration
            if (totalIdleTime >= idleDuration)
            {
                isAnimating = true;
                animationTimer = 0f;
                animationSpriteIndex = 0;
            }
        }
        else
        {
            // Executa a animação final
            animationTimer += Time.deltaTime;
            if (animationTimer >= animationFrameInterval)
            {
                animationTimer = 0f;

                if (animationSpriteIndex < animationSprites.Length)
                {
                    spriteRenderer.sprite = animationSprites[animationSpriteIndex];
                    animationSpriteIndex++;
                }
                else
                {
                    // Para no último sprite da animação final
                    enabled = false;
                }
            }
        }
    }
}
