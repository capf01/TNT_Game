using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TarodevController;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{
    [Header("Menu Objects")]
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private WheelHotbarController _wheelHotBarController;
    private PauseMenuManager _pauseMenuManager;
    private bool _isPaused;
    private PlayerInput _input;

    [Header("Player Scripts to Deactivate on Pause")]
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private PlayerInput _playerInput;

    [Header("First Selected Options")]
    [SerializeField] private GameObject _pauseMenuFirst;
    public bool onPauseMenu;
    public bool firstStage;

    protected virtual void Awake()
    {
        _input = GetComponent<PlayerInput>();
        _pauseMenuManager = GetComponent<PauseMenuManager>();
    }

    void Start()
    {
        _pauseMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(_pauseMenuFirst);
    }

    // Update is called once per frame
    void Update()
    {
        if (_input.FrameInput.Start && !_pauseMenuManager.onPauseMenu && !firstStage)
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
        Time.timeScale = 0.05f;

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
        _wheelHotBarController.StartAnimation();
        //EventSystem.current.SetSelectedGameObject(_pauseMenuFirst);
    }

    private void CloseMenu()
    {
        _wheelHotBarController.StartAnimation();
        _pauseMenu.SetActive(false);
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