using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TarodevController;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenuManager : MonoBehaviour
{
    [Header("Menu Objects")]
    [SerializeField] private GameObject _pauseMenu;
    private MenuManager _menuManager;
    private bool _isPaused;
    private PlayerInput _input;

    [Header("Player Scripts to Deactivate on Pause")]
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private PlayerInput _playerInput;

    [Header("First Selected Options")]
    [SerializeField] private GameObject _pauseMenuFirst;
    public bool onPauseMenu;

    protected virtual void Awake()
    {
        _input = GetComponent<PlayerInput>();
        _menuManager = GetComponent<MenuManager>();
    }

    void Start()
    {
        _pauseMenu.SetActive(false);
        //EventSystem.current.SetSelectedGameObject(_pauseMenuFirst);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_input.FrameInput.Esc && !_menuManager.onPauseMenu)
        {
            if (!_isPaused)
            {
                Pause();
            }
            else
            {
                Unpause();
            }
        }
    }

    #region Pause/Unpause Functions

    public void Pause()
    {
        _isPaused = true;
        onPauseMenu = true;
        StartCoroutine(Teste());
        Time.timeScale = 0;

        //if (_playerController != null) _playerController.enabled = false;
        if (_playerInput != null) _playerInput.enabled = false;

        OpenMainMenu();
    }

    public void Unpause()
    {
        _isPaused = false;
        StartCoroutine(UnpauseDelay());
        Time.timeScale = 1f;

        if (_playerController != null) _playerController.enabled = true;
        if (_playerInput != null) _playerInput.enabled = true;

        CloseMenu();
    }

    IEnumerator UnpauseDelay()
    {
        yield return new WaitForSeconds(0.1f);
        onPauseMenu = false;
    }

    #endregion

    #region Canvas Activations

    private void OpenMainMenu()
    {
        _pauseMenu.SetActive(true);
    }

    private void CloseMenu()
    {
        _pauseMenu.SetActive(false);
    }

    IEnumerator Teste()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        //EventSystem.current.SetSelectedGameObject(_pauseMenuFirst);
    }

    #endregion

    #region Pause Menu Button Actions

    public void MainMenuPress()
    {
        //SceneManager.LoadScene(0);
        Application.Quit();
    }

    public void OnResumePress()
    {
        Unpause();
    }

    #endregion
}