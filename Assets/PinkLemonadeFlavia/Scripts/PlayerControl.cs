using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float velocidade;        // Velocidade de movimento
    public float forcaPulo;        // Força do pulo
    public Transform verificadorDeChao;  // Ponto para verificar se o personagem está no chão
    public LayerMask Ground;       // Camada de chão (definida nas configurações de Unity)
    private bool estaNoChao;       // Se o personagem está no chão
    private Rigidbody2D rb;        // Referência para o Rigidbody2D do player
    private bool viradoParaDireita = true;  // Controle da direção em que o personagem está virado

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Obtem o Rigidbody2D
    }

    void Update()
    {
        Mover();   // Chama a função de movimento
        Pular();   // Chama a função de pulo
        Virar();   // Chama a função de virada
    }

    void Mover()
    {
        float entradaMovimento = Input.GetAxisRaw("Horizontal");  // Recebe o input horizontal (A/D ou setas)
        rb.velocity = new Vector2(entradaMovimento * velocidade, rb.velocity.y);  // Atualiza a velocidade do personagem
    }

    void Pular()
    {
        estaNoChao = Physics2D.OverlapCircle(verificadorDeChao.position, 0.2f, Ground);
        if (Input.GetButtonDown("Jump") && estaNoChao)
        {
            rb.velocity = new Vector2(rb.velocity.x, forcaPulo);
        }
    }

    void Virar()
    {
        float entradaMovimento = Input.GetAxis("Horizontal");

        if (entradaMovimento > 0 && !viradoParaDireita)
        {
            Flipar();  
        }
        else if (entradaMovimento < 0 && viradoParaDireita)
        {
            Flipar();
        }
    }

    void Flipar()
    {
        viradoParaDireita = !viradoParaDireita;
        Vector3 escalaLocal = transform.localScale;
        escalaLocal.x *= -1;
        transform.localScale = escalaLocal;
    }
}
