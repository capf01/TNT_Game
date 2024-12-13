using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SelectManager : MonoBehaviour
{
    [Header("Text Animation Settings")]
    public string color1Hex = "#FF5733"; // Primeira cor (vermelha)
    public string color2Hex = "#33FF57"; // Segunda cor (verde)
    public float colorChangeSpeed = 1.0f; // Velocidade da troca de cores

    [Header("Object Animation Settings")]
    public float moveDistanceX = 2.0f; // Distância para mover no eixo X
    public float moveDurationX = 1.0f; // Duração do movimento no eixo X
    public float bounceHeight = 0.5f; // Altura do bounce
    public float bounceDuration = 0.5f; // Duração da animação de bounce

    private Vector3 initialLocalPosition;
    private Coroutine colorCoroutine;
    private Coroutine moveAndBounceCoroutine;

    public bool _playerSelected;

    public void StartColorCycle(TextMeshProUGUI text)
    {
        if (text != null)
        {
            // Inicia a alternância de cores, sem movimento
            if (colorCoroutine == null)
            {
                colorCoroutine = StartCoroutine(AlternateTextColor(text));
            }
        }
    }

    public void StopColorCycle(TextMeshProUGUI text)
    {
        if (text != null)
        {
            // Para a alternância de cores e reseta para branco
            if (colorCoroutine != null)
            {
                StopCoroutine(colorCoroutine);
                colorCoroutine = null;
            }
            text.color = Color.white;
        }
    }

    public void SelectPlayer(GameObject player)
    {
        if (player != null)
        {
            // Ativar o Animator
            Animator animator = player.GetComponent<Animator>();
            if (animator != null)
            {
                animator.enabled = true;
            }

            // Alterar a cor para White
            Image image = player.GetComponent<Image>();
            if (image != null)
            {
                image.color = Color.white;
            }
        }
    }

    public void DeselectPlayer(GameObject player)
    {
        if (player != null && !_playerSelected)
        {
            // Desativar o Animator
            Animator animator = player.GetComponent<Animator>();
            if (animator != null)
            {
                animator.enabled = false;
            }

            // Alterar a cor para 3F3F3F
            Image image = player.GetComponent<Image>();
            if (image != null)
            {
                image.color = new Color(0.247f, 0.247f, 0.247f); // Hexadecimal 3F3F3F
            }
        }
    }

    public void OnSelectOption(TextMeshProUGUI text)
    {
        Debug.Log("Selecionou");
        if (text != null && text.transform.parent != null)
        {
            Transform parentObject = text.transform.parent;

            if (initialLocalPosition == Vector3.zero)
            {
                initialLocalPosition = parentObject.localPosition; // Armazena a posição inicial local do pai
            }

            // Inicia a alternância de cores
            if (colorCoroutine == null)
            {
                colorCoroutine = StartCoroutine(AlternateTextColor(text));
            }

            // Inicia as animações de movimento no eixo X e bounce juntas
            if (moveAndBounceCoroutine == null)
            {
                moveAndBounceCoroutine = StartCoroutine(MoveAndBounce(parentObject));
            }
        }
    }

    public void OnDeselectOption(TextMeshProUGUI text)
    {
        if (text != null && text.transform.parent != null)
        {
            Transform parentObject = text.transform.parent;

            // Para a alternância de cores e reseta para branco
            if (colorCoroutine != null)
            {
                StopCoroutine(colorCoroutine);
                colorCoroutine = null;
            }
            text.color = Color.white;

            // Retorna o objeto pai para a posição inicial (apenas no eixo X)
            if (moveAndBounceCoroutine != null)
            {
                StopCoroutine(moveAndBounceCoroutine);
                moveAndBounceCoroutine = null;
            }
            StartCoroutine(MoveObjectX(parentObject, initialLocalPosition.x));
        }
    }

    private IEnumerator AlternateTextColor(TextMeshProUGUI text)
    {
        Color color1, color2;
        if (!ColorUtility.TryParseHtmlString(color1Hex, out color1) ||
            !ColorUtility.TryParseHtmlString(color2Hex, out color2))
        {
            Debug.LogError("Invalid hex color codes provided.");
            yield break;
        }

        float t = 0f;
        while (true)
        {
            t += Time.deltaTime * colorChangeSpeed;
            text.color = Color.Lerp(color1, color2, Mathf.PingPong(t, 1));
            yield return null;
        }
    }

    private IEnumerator MoveAndBounce(Transform targetObject)
    {
        float elapsedTime = 0f;
        Vector3 startLocalPosition = targetObject.localPosition;
        Vector3 targetLocalPositionX = new Vector3(startLocalPosition.x + moveDistanceX, startLocalPosition.y, startLocalPosition.z);

        while (elapsedTime < Mathf.Max(moveDurationX, bounceDuration))
        {
            float tX = Mathf.Clamp01(elapsedTime / moveDurationX);
            float tBounce = Mathf.Clamp01(elapsedTime / bounceDuration);

            // Movimento no eixo X
            float newX = Mathf.Lerp(startLocalPosition.x, targetLocalPositionX.x, tX);

            // Movimento no eixo Y (Bounce)
            float bounceOffset = Mathf.Sin(tBounce * Mathf.PI) * bounceHeight;
            float newY = startLocalPosition.y + bounceOffset;

            // Aplica as novas posições locais
            targetObject.localPosition = new Vector3(newX, newY, startLocalPosition.z);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Garante a posição final
        targetObject.localPosition = new Vector3(targetLocalPositionX.x, startLocalPosition.y, startLocalPosition.z);
    }

    private IEnumerator MoveObjectX(Transform targetObject, float targetX)
    {
        float elapsedTime = 0f;
        Vector3 startLocalPosition = targetObject.localPosition;
        Vector3 targetLocalPosition = new Vector3(targetX, startLocalPosition.y, startLocalPosition.z);

        while (elapsedTime < moveDurationX)
        {
            float t = elapsedTime / moveDurationX;
            targetObject.localPosition = Vector3.Lerp(startLocalPosition, targetLocalPosition, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Garante a posição final
        targetObject.localPosition = targetLocalPosition;
    }
}
