using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TarodevController;

public class DebuffsPlatafoma : MonoBehaviour
{
    [SerializeField] private ScriptableStats _stats;
    [SerializeField] private float plataformDecelerationFactor = 0.2f;
    [SerializeField] private float plataformAcceleration = 20;
    [SerializeField] private float plataformMaxSpeed = 7; // Metade da velocidade normal, por exemplo

    private float _originalAcceleration;
    private float _originalDeceleration;
    private float _originalMaxSpeed;

    private void Start()
    {
        // Armazena os valores originais
        _originalAcceleration = _stats.Acceleration;
        _originalDeceleration = _stats.GroundDeceleration;
        _originalMaxSpeed = _stats.MaxSpeed;
    }

    private void DeslizandoOn()
    {
        _stats.Acceleration = plataformAcceleration;
        _stats.GroundDeceleration = _originalDeceleration * plataformDecelerationFactor;
        _stats.MaxSpeed = plataformMaxSpeed;
    }

    private void DeslizandoOff()
    {
        _stats.Acceleration = _originalAcceleration;
        _stats.GroundDeceleration = _originalDeceleration;
        _stats.MaxSpeed = _originalMaxSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DeslizandoOn();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DeslizandoOff();
        }
    }
}
