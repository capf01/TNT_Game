using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PowerFocusPinkLemonade : MonoBehaviour
{
    public float slowMovementFactor = 0.5f; // Fator de redução da velocidade do jogador
    public GameObject fieldOfViewEffect; // Efeito visual para restringir o campo de visão
    public KeyCode actionKey = KeyCode.E; // Tecla para ativar/desativar a habilidade

    public Light2D spotLight; // Spot Light 2D no jogador
    public Light2D globalLight; // Global Light 2D

    private float originalSpeed; // Velocidade original do jogador
    private PlayerControl playerControl; // Referência ao script de controle do jogador
    private bool isRevealing = false; // Estado da habilidade
    private SpriteRenderer[] allRevealableObjects; // Cache de todos os objetos reveláveis

    void Start()
    {
        // Obter referência ao controle do jogador
        playerControl = GetComponent<PlayerControl>();
        if (playerControl != null)
        {
            originalSpeed = playerControl.velocidade;
        }

        // Encontrar todos os objetos reveláveis no início
        allRevealableObjects = FindObjectsOfType<SpriteRenderer>();

        // Desativar as luzes no início
        if (spotLight != null)
        {
            spotLight.enabled = false;
        }
        if (globalLight != null)
        {
            globalLight.enabled = false;
        }
    }

    void Update()
    {
        // Verifica se a tecla foi pressionada para ativar/desativar a habilidade
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

        // Ativar as luzes
        if (spotLight != null)
        {
            spotLight.enabled = true;
        }
        if (globalLight != null)
        {
            globalLight.enabled = true;
        }

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

        // Revelar todos os itens invisíveis e paredes falsas
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
                // Tornar a parede falsa semitransparente e atravessável
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

        // Desativar as luzes
        if (spotLight != null)
        {
            spotLight.enabled = false;
        }
        if (globalLight != null)
        {
            globalLight.enabled = false;
        }

        // Restaurar a velocidade do jogador
        if (playerControl != null)
        {
            playerControl.velocidade = originalSpeed;
        }

        // Tornar invisíveis os itens revelados e restaurar as paredes falsas
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
                // Tornar a parede falsa opaca e não atravessável
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
