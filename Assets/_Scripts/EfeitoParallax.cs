using System.Collections.Generic; // Para usar listas
using UnityEngine;

public class EfeitoParallax : MonoBehaviour
{
    public List<Transform> objetosDeFundo; // Lista de objetos de fundo
    public float intensidadeParallax = 0.5f; // Intensidade do efeito de paralaxe
    private Vector3 posicaoAnteriorCamera; // Para armazenar a posição anterior da câmera

    void Start()
    {
        // Armazena a posição inicial da câmera
        posicaoAnteriorCamera = Camera.main.transform.position;
    }

    void Update()
    {
        // Para cada objeto na lista de objetosDeFundo
        for (int i = 0; i < objetosDeFundo.Count; i++)
        {
            // Calcula o efeito de paralaxe baseado na posição da câmera
            float parallax = (posicaoAnteriorCamera.x - Camera.main.transform.position.x) * (i * intensidadeParallax + 1);
            
            // Determina a nova posição X do fundo
            float novaPosicaoFundoX = objetosDeFundo[i].position.x + parallax;

            // Cria a nova posição do fundo, mantendo os valores Y e Z
            Vector3 novaPosicao = new Vector3(novaPosicaoFundoX, objetosDeFundo[i].position.y, objetosDeFundo[i].position.z);

            // Aplica a nova posição
            objetosDeFundo[i].position = novaPosicao;
        }

        // Atualiza a posição anterior da câmera
        posicaoAnteriorCamera = Camera.main.transform.position;
    }
}
