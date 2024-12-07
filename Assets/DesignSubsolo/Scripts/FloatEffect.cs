using UnityEngine;

public class FloatEffect : MonoBehaviour
{
    [Header("Configura��es de Flutua��o")]
    [SerializeField] private float altura = 0.5f; // Altura m�xima de flutua��o
    [SerializeField] private float velocidade = 2f; // Velocidade da flutua��o

    private Vector3 posicaoInicial;

    private void Start()
    {
        // Armazena a posi��o inicial do objeto
        posicaoInicial = transform.position;
    }

    private void Update()
    {
        // Calcula a nova posi��o com base em uma onda seno
        float novaAltura = Mathf.Sin(Time.time * velocidade) * altura;
        transform.position = posicaoInicial + new Vector3(0, novaAltura, 0);
    }
}
