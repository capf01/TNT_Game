using UnityEngine;
using System.Collections;
using TarodevController;

public class GameManager : MonoBehaviour
{
    public float shakeDuration = 0.5f; // Dura��o do shake
    public float shakeMagnitude = 0.1f; // Intensidade do shake
    public float dampingSpeed = 1.0f; // Suaviza��o do shake

    private CameraFollow cameraFollow; // Refer�ncia ao script de controle da c�mera
    [SerializeField] private GameObject _screenTransition1;

    void Start()
    {
        // Obt�m a refer�ncia ao script CameraFollow
        cameraFollow = FindObjectOfType<CameraFollow>();
    }

    public void TriggerShake()
    {
        if (cameraFollow != null)
        {
            cameraFollow.DisableCameraMovement(); // Desativa o movimento da c�mera durante o shake
        }

        StartCoroutine(Shake());
    }

    public void StartScreenTransition(int screenTransition)
    {
        if (screenTransition == 1)
        {
            _screenTransition1.SetActive(false);
            _screenTransition1.SetActive(true);
        }
    }

    private IEnumerator Shake()
    {
        Vector3 originalPosition = Camera.main.transform.position;
        float shakeTimer = shakeDuration;

        while (shakeTimer > 0)
        {
            float xOffset = Random.Range(-shakeMagnitude, shakeMagnitude);
            float yOffset = Random.Range(-shakeMagnitude, shakeMagnitude);
            Camera.main.transform.position = new Vector3(originalPosition.x + xOffset, originalPosition.y + yOffset, originalPosition.z);

            shakeTimer -= Time.deltaTime;
            yield return null;
        }

        // Restaura a posi��o da c�mera e ativa novamente o movimento ap�s o shake
        Camera.main.transform.position = originalPosition;
        if (cameraFollow != null)
        {
            cameraFollow.EnableCameraMovement(); // Restaura o controle de movimento da c�mera
        }
    }
}

