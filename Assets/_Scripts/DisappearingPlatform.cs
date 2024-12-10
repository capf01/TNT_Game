using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingPlatform : MonoBehaviour
{
    public float toggleInterval = 3f; // Intervalo em segundos para alternar entre visível e invisível
    private Collider2D platformCollider; // Colisor da plataforma
    private SpriteRenderer platformRenderer; // Renderer da plataforma

    void Start()
    {
        platformCollider = GetComponent<Collider2D>();
        platformRenderer = GetComponent<SpriteRenderer>();

        if (platformCollider == null || platformRenderer == null)
        {
             return;
        }

        StartCoroutine(ToggleVisibility());
    }

    IEnumerator ToggleVisibility()
    {
        while (true)
        {
            // Alterna a visibilidade e o estado do colisor
            bool isVisible = platformRenderer.enabled;
            platformRenderer.enabled = !isVisible;
            platformCollider.enabled = !isVisible;

            yield return new WaitForSeconds(toggleInterval);
        }
    }
}
