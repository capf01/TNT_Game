using System.Collections;
using UnityEngine;

public class TransparentWall : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Coroutine transparencyCoroutine;
    public float fadeSpeed = 1f; // Velocidade do fade (quanto maior, mais rápido)

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (transparencyCoroutine != null)
                StopCoroutine(transparencyCoroutine);
            transparencyCoroutine = StartCoroutine(ChangeTransparency(0f));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (transparencyCoroutine != null)
                StopCoroutine(transparencyCoroutine);
            transparencyCoroutine = StartCoroutine(ChangeTransparency(1f));
        }
    }

    private IEnumerator ChangeTransparency(float targetAlpha)
    {
        Color color = spriteRenderer.color;
        while (!Mathf.Approximately(color.a, targetAlpha))
        {
            color.a = Mathf.MoveTowards(color.a, targetAlpha, fadeSpeed * Time.deltaTime);
            spriteRenderer.color = color;
            yield return null;
        }
    }
}
