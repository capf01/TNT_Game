using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DisappearingTilemap : MonoBehaviour
{
    public float toggleInterval = 3f; // Intervalo em segundos para alternar entre visível e invisível
    private TilemapRenderer tilemapRenderer; // Renderer do Tilemap
    private TilemapCollider2D tilemapCollider; // Colisor do Tilemap

    void Start()
    {
        tilemapRenderer = GetComponentInChildren<TilemapRenderer>();
        tilemapCollider = GetComponentInChildren<TilemapCollider2D>();

        if (tilemapRenderer == null || tilemapCollider == null)
        {
            Debug.LogError("O Tilemap precisa ter um TilemapRenderer e um TilemapCollider2D anexados.");
            return;
        }

        StartCoroutine(ToggleVisibility());
    }


    IEnumerator ToggleVisibility()
    {
        while (true)
        {
            // Alterna a visibilidade e o estado do colisor
            bool isVisible = tilemapRenderer.enabled;
            tilemapRenderer.enabled = !isVisible;
            tilemapCollider.enabled = !isVisible;

            yield return new WaitForSeconds(toggleInterval);
        }
    }
}
