using UnityEngine;
using System.Collections;

namespace TarodevController
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform _player; // Referência ao jogador
        [SerializeField] private Transform[] _screenBounds; // Array de bounds para cada tela
        [SerializeField] private float _transitionSpeed = 2f; // Velocidade da transição entre telas

        private Transform _currentScreen; // Tela atual
        private bool _isTransitioning; // Se a câmera está em transição
        private bool _isShaking = false; // Se o shake está ativo

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

            // Manter a câmera dentro dos limites da tela atual
            Vector3 targetPosition = new Vector3(
                Mathf.Clamp(_player.position.x, _currentScreen.position.x - _currentScreen.localScale.x / 2 + CameraHalfWidth(), _currentScreen.position.x + _currentScreen.localScale.x / 2 - CameraHalfWidth()),
                Mathf.Clamp(_player.position.y, _currentScreen.position.y - _currentScreen.localScale.y / 2 + CameraHalfHeight(), _currentScreen.position.y + _currentScreen.localScale.y / 2 - CameraHalfHeight()),
                -10
            );

            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * _transitionSpeed);
        }

        public void DisableCameraMovement()
        {
            _isShaking = true; // Desativa a movimentação da câmera durante o shake
        }

        public void EnableCameraMovement()
        {
            _isShaking = false; // Restaura a movimentação da câmera
        }

        private void ChangeToNextScreen()
        {
            _isTransitioning = true;

            // Determinar a próxima tela com base na posição do jogador
            Transform nextScreen = GetNextScreen();
            if (nextScreen == null)
            {
                _isTransitioning = false;
                return;
            }

            _currentScreen = nextScreen;

            // Fazer a transição suave da câmera
            StartCoroutine(SmoothTransitionWithPlayer());
        }

        private Transform GetNextScreen()
        {
            foreach (var screen in _screenBounds)
            {
                if (screen == _currentScreen) continue;

                // Verificar se o jogador está dentro dos limites da próxima tela
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
