using UnityEngine;

public class FloatEffect : MonoBehaviour
{
    [Header("Configurações de Flutuação")]
    [SerializeField] private float altura = 0.5f; // Altura máxima de flutuação
    [SerializeField] private float velocidade = 2f; // Velocidade da flutuação

    private Vector3 posicaoInicialLocal;

    private void Start()
    {
        // Armazena a posição inicial local do objeto
        posicaoInicialLocal = transform.localPosition;
    }

    private void Update()
    {
        // Calcula a nova posição local com base em uma onda seno
        float novaAltura = Mathf.Sin(Time.time * velocidade) * altura;
        transform.localPosition = posicaoInicialLocal + new Vector3(0, novaAltura, 0);
    }
}
