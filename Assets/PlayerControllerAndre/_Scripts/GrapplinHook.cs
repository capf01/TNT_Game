using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;

public class GrapplinHook : MonoBehaviour
{
    [SerializeField] private float grappleLength;
    [SerializeField] private LayerMask grappleLayer;
    [SerializeField] private LineRenderer rope;
    private PlayerController _playerController;

    private Vector3 grapplePoint;
    private DistanceJoint2D joint;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        joint = gameObject.GetComponent<DistanceJoint2D>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        _playerController = GetComponent<PlayerController>();

        joint.enabled = false;
        rope.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Definindo a direção do Raycast com base na velocidade horizontal
        Vector2 rayDirection;
        if (_playerController.Speed.x > 0)
        {
            rayDirection = (Vector2.up + Vector2.right).normalized; // Direita
        }
        else if (_playerController.Speed.x < 0)
        {
            rayDirection = (Vector2.up + Vector2.left).normalized; // Esquerda
        }
        else
        {
            rayDirection = Vector2.up; // Caso o jogador esteja parado
        }

        Vector3 rayOrigin = transform.position;

        // Visualização do Raycast no editor
        Debug.DrawRay(rayOrigin, rayDirection * grappleLength, Color.red);

        if (Input.GetKeyDown(KeyCode.Delete))
        {
            // Realiza o Raycast na direção calculada
            RaycastHit2D hit = Physics2D.Raycast(origin: rayOrigin, direction: rayDirection, distance: grappleLength, layerMask: grappleLayer);

            if (hit.collider != null)
            {
                Debug.Log("Acertou a parede");
                _playerController._grappling = true;
                grapplePoint = hit.point;
                grapplePoint.z = 0;

                // Configurar o DistanceJoint2D
                joint.connectedAnchor = grapplePoint;
                joint.autoConfigureDistance = false;
                joint.distance = Vector2.Distance(transform.position, grapplePoint);
                joint.maxDistanceOnly = true; // Permite balanço sem puxar diretamente
                joint.enabled = true;

                // Configurar o LineRenderer
                rope.SetPosition(0, grapplePoint);
                rope.SetPosition(1, transform.position);
                rope.enabled = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.Delete))
        {
            joint.enabled = false;
            rope.enabled = false;
            _playerController._grappling = false;
        }

        if (rope.enabled == true)
        {
            rope.SetPosition(1, transform.position);
            rope.startWidth = 0.2f;
            rope.endWidth = 0.2f;
        }
    }
}
