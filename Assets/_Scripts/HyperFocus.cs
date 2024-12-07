using UnityEngine;
using UnityEngine.Rendering.Universal;

public class HyperFocus : MonoBehaviour
{
    public Light2D targetLight1; // Referência ao primeiro Light2D (cor)
    public Light2D targetLight2; // Referência ao segundo Light2D (intensidade)
    public float duration = 2f; // Tempo total da transição (em segundos)
    private float elapsedTime = 0f; // Tempo decorrido
    public bool _hyperFocusOn;

    void Update()
    {
        // Incrementa ou decrementa o tempo baseado no estado de _hyperFocusOn
        if (_hyperFocusOn)
        {
            elapsedTime += Time.deltaTime;
        }
        else
        {
            elapsedTime -= Time.deltaTime;
        }

        // Garante que elapsedTime fique dentro do intervalo [0, duration]
        elapsedTime = Mathf.Clamp(elapsedTime, 0f, duration);

        // Calcula o progresso da transição
        float t = Mathf.Clamp01(elapsedTime / duration);

        // Atualiza a cor do primeiro Light2D (branco ? preto)
        if (targetLight1 != null)
        {
            targetLight1.color = Color.Lerp(Color.white, Color.black, t);
        }

        // Atualiza a intensidade do segundo Light2D (0 ? 1)
        if (targetLight2 != null)
        {
            targetLight2.intensity = Mathf.Lerp(0f, 1f, t);
        }
    }
}
