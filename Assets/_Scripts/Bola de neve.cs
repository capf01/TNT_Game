using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boladeneve : MonoBehaviour
{
    public Transform pontoA;
    public Transform pontoB;
    public float velocidade = 1f;
    private Vector3 direcao;
    public float kikadaForce = 0f;

    void Start()
    {
        direcao = (pontoB.position - pontoA.position).normalized;
        
    }
    void Update()
    {
        transform.position += direcao * velocidade * Time.deltaTime;
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Parede":
                Destroy(transform.gameObject); 
                break;

        }    
    }
}
