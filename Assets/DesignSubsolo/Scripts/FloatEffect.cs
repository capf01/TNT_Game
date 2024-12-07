using UnityEngine;

public class FloatEffect : MonoBehaviour
{
    [Header("Configurações de Flutuação")]
    [SerializeField] private float altura = 0.5f; // Altura máxima de flutuação
    [SerializeField] private float velocidade = 2f; // Velocidade da flutuação

    private Vector3 posicaoInicial;

    private void Start()
    {
        // Armazena a posição inicial do objeto
        posicaoInicial = transform.position;
    }

    private void Update()
    {
        // Calcula a nova posição com base em uma onda seno
        float novaAltura = Mathf.Sin(Time.time * velocidade) * altura;
        transform.position = posicaoInicial + new Vector3(0, novaAltura, 0);
    }
}
