using UnityEngine;
using System.Collections;

namespace TarodevController
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform _player; // Refer�ncia ao jogador
        [SerializeField] private Transform[] _screenBounds; // Array de bounds para cada tela
        [SerializeField] private float _transitionSpeed = 2f; // Velocidade da transi��o entre telas

        private Transform _currentScreen; // Tela atual
        private bool _isTransitioning; // Se a c�mera est� em transi��o
        private bool _isShaking = false; // Se o shake est� ativo

        private void Start()
        {
            if (_player == null)
            {
                var player = FindObjectOfType<PlayerController>();
                if (player != null) _player = player.transform;
            }

            // Inicializar com a primeira tela
            if (_screenBounds.Length > 0)
            {
                _currentScreen = _screenBounds[0];
            }
        }

        private void Update()
        {
            if (!_player || _isTransitioning || _isShaking) return;

            // Verificar se o jogador saiu dos limites da tela atual
            if (!IsPlayerWithinBounds(_currentScreen))
            {
                ChangeToNextScreen();
                return;
            }

            // Manter a c�mera dentro dos limites da tela atual
            Vector3 targetPosition = new Vector3(
                Mathf.Clamp(_player.position.x, _currentScreen.position.x - _currentScreen.localScale.x / 2 + CameraHalfWidth(), _currentScreen.position.x + _currentScreen.localScale.x / 2 - CameraHalfWidth()),
                Mathf.Clamp(_player.position.y, _currentScreen.position.y - _currentScreen.localScale.y / 2 + CameraHalfHeight(), _currentScreen.position.y + _currentScreen.localScale.y / 2 - CameraHalfHeight()),
                -10
            );

            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * _transitionSpeed);
        }

        public void DisableCameraMovement()
        {
            _isShaking = true; // Desativa a movimenta��o da c�mera durante o shake
        }

        public void EnableCameraMovement()
        {
            _isShaking = false; // Restaura a movimenta��o da c�mera
        }

        private void ChangeToNextScreen()
        {
            _isTransitioning = true;

            // Determinar a pr�xima tela com base na posi��o do jogador
            Transform nextScreen = GetNextScreen();
            if (nextScreen == null)
            {
                _isTransitioning = false;
                return;
            }

            _currentScreen = nextScreen;

            // Fazer a transi��o suave da c�mera
            StartCoroutine(SmoothTransitionWithPlayer());
        }

        private Transform GetNextScreen()
        {
            foreach (var screen in _screenBounds)
            {
                if (screen == _currentScreen) continue;

                // Verificar se o jogador est� dentro dos limites da pr�xima tela
                if (IsPlayerWithinBounds(screen))
                {
                    return screen;
                }
            }

            return null;
        }

        private IEnumerator SmoothTransitionWithPlayer()
        {
            Vector3 targetPosition;

            while (!IsPlayerWithinBounds(_currentScreen))
            {
                targetPosition = new Vector3(
                    Mathf.Clamp(_player.position.x, _currentScreen.position.x - _currentScreen.localScale.x / 2 + CameraHalfWidth(), _currentScreen.position.x + _currentScreen.localScale.x / 2 - CameraHalfWidth()),
                    Mathf.Clamp(_player.position.y, _currentScreen.position.y - _currentScreen.localScale.y / 2 + CameraHalfHeight(), _currentScreen.position.y + _currentScreen.localScale.y / 2 - CameraHalfHeight()),
                    -10
                );

                transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * _transitionSpeed);
                yield return null;
            }

            _isTransitioning = false;
        }

        private bool IsPlayerWithinBounds(Transform bounds)
        {
            return _player.position.x >= bounds.position.x - bounds.localScale.x / 2 &&
                   _player.position.x <= bounds.position.x + bounds.localScale.x / 2 &&
                   _player.position.y >= bounds.position.y - bounds.localScale.y / 2 &&
                   _player.position.y <= bounds.position.y + bounds.localScale.y / 2;
        }

        private float CameraHalfWidth()
        {
            return Camera.main.orthographicSize * Camera.main.aspect;
        }

        private float CameraHalfHeight()
        {
            return Camera.main.orthographicSize;
        }
    }

}
