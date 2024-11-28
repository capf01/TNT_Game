using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerFocusPinkLemonade : MonoBehaviour
{
    public float slowMovementFactor = 0.5f; // Fator de redução da velocidade do jogador
    public GameObject fieldOfViewEffect; // Efeito visual para restringir o campo de visão
    public KeyCode actionKey = KeyCode.E; // Tecla para ativar/desativar a habilidade

    private float originalSpeed; // Velocidade original do jogador
    private PlayerControl playerControl; // Referência ao script de controle do jogador
    private bool isRevealing = false; // Estado da habilidade
    private SpriteRenderer[] allRevealableObjects; // Cache de todos os objetos reveláveis
    private SpriteRenderer[] allFalseWalls; // Cache de todas as paredes falsas

    void Start()
    {
        // Obter referência ao controle do jogador
        playerControl = GetComponent<PlayerControl>();
        if (playerControl != null)
        {
            originalSpeed = playerControl.velocidade;
        }

        // Encontrar todos os objetos reveláveis e paredes falsas no início
        allRevealableObjects = FindObjectsOfType<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(actionKey))
        {
            if (isRevealing)
            {
                DeactivateVisionReveal();
            }
            else
            {
                ActivateVisionReveal();
            }
        }
    }

    void ActivateVisionReveal()
    {
        isRevealing = true;

        // Ativar o campo de visão restrito
        if (fieldOfViewEffect != null)
        {
            fieldOfViewEffect.SetActive(true);
        }

        // Reduzir a velocidade do jogador
        if (playerControl != null)
        {
            playerControl.velocidade *= slowMovementFactor;
        }

        // Revelar todos os itens e paredes falsas
        foreach (SpriteRenderer renderer in allRevealableObjects)
        {
            GameObject obj = renderer.gameObject;
            if (obj.CompareTag("InvisibleItem"))
            {
                // Tornar visível e ajustar o Collider2D
                renderer.enabled = true;
                renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 1f);
                Collider2D collider = obj.GetComponent<Collider2D>();
                if (collider != null)
                {
                    collider.isTrigger = false; // Não atravessável
                }
            }
            else if (obj.CompareTag("FalseWall"))
            {
                // Tornar semitransparente e atravessável
                renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 0.5f);
                Collider2D collider = obj.GetComponent<Collider2D>();
                if (collider != null)
                {
                    collider.isTrigger = true; // Atravessável
                }
            }
        }
    }

    void DeactivateVisionReveal()
    {
        isRevealing = false;

        // Desativar o campo de visão restrito
        if (fieldOfViewEffect != null)
        {
            fieldOfViewEffect.SetActive(false);
        }

        // Restaurar a velocidade do jogador
        if (playerControl != null)
        {
            playerControl.velocidade = originalSpeed;
        }

        // Restaurar o estado original de todos os itens e paredes falsas
        foreach (SpriteRenderer renderer in allRevealableObjects)
        {
            GameObject obj = renderer.gameObject;
            if (obj.CompareTag("InvisibleItem"))
            {
                // Tornar invisível e ajustar o Collider2D para ser atravessável
                renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 0f);
                Collider2D collider = obj.GetComponent<Collider2D>();
                if (collider != null)
                {
                    collider.isTrigger = true; // Atravessável quando invisível
                }
            }
            else if (obj.CompareTag("FalseWall"))
            {
                // Tornar opaco e sólido
                renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 1f);
                Collider2D collider = obj.GetComponent<Collider2D>();
                if (collider != null)
                {
                    collider.isTrigger = false; // Não atravessável
                }
            }
        }
    }
}
