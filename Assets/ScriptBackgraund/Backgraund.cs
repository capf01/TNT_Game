using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backgraund : MonoBehaviour
{
    public float velocidadeDoBackgraund;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        movimentarBackgraund(); 
    }
    private void movimentarBackgraund()
    {
        Vector2 deslocamento = new Vector2(Time.time * velocidadeDoBackgraund, 0);
        GetComponent<Renderer>().material.mainTextureOffset = deslocamento;
    }
}
