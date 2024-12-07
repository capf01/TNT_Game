using UnityEngine;

public class ExplosionAnimation : MonoBehaviour
{
    public Sprite[] idleSprites; // Sprites alternados (2 sprites para o estado inicial)
    public float idleSwitchInterval = 0.5f; // Intervalo de troca entre os sprites
    public float idleDuration = 2f; // Tempo total de altern�ncia antes da anima��o final
    public Sprite[] animationSprites; // Sprites para a anima��o final (10 sprites)
    public float animationFrameInterval = 0.1f; // Intervalo entre os quadros da anima��o final

    private SpriteRenderer spriteRenderer; // SpriteRenderer do pr�prio objeto
    private int idleSpriteIndex = 0; // �ndice atual dos sprites idle
    private int animationSpriteIndex = 0; // �ndice atual dos sprites da anima��o final
    private float idleTimer = 0f; // Cron�metro para o tempo de altern�ncia
    private float animationTimer = 0f; // Cron�metro para os quadros da anima��o final
    private float totalIdleTime = 0f; // Cron�metro total do estado inicial
    private bool isAnimating = false; // Indica se est� na anima��o final

    void Start()
    {
        // Obt�m o SpriteRenderer do objeto
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

            // Transi��o para a anima��o final ap�s idleDuration
            if (totalIdleTime >= idleDuration)
            {
                isAnimating = true;
                animationTimer = 0f;
                animationSpriteIndex = 0;
            }
        }
        else
        {
            // Executa a anima��o final
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
                    // Para no �ltimo sprite da anima��o final
                    enabled = false;
                }
            }
        }
    }
}
