using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LevelSelector : MonoBehaviour
{
    public Button continueButton; // Bot�o de continuar
    public Button resetOrNewGameButton; // Bot�o de resetar ou novo jogo
    public Button mainMenuButton; // Bot�o para retornar ao menu principal
    public Button pauseButton; // Bot�o de pausa
    public GameObject pauseMenu;  // O painel de pausa
    private bool isPaused = false; // Para controlar o estado de pausa
    private string currentLevel; // Para armazenar o n�vel atual do jogo

    void Start()
    {
        // Garantir que haja apenas um EventSystem e um AudioListener na cena
        EnsureSingleEventSystem();
        EnsureSingleAudioListener();

        // Configura os bot�es
        if (continueButton != null)
        {
            continueButton.onClick.AddListener(ContinueGame);
        }

        if (resetOrNewGameButton != null)
        {
            resetOrNewGameButton.onClick.AddListener(ResetOrNewGame);
        }

        if (mainMenuButton != null)
        {
            mainMenuButton.onClick.AddListener(ReturnToMainMenu);
        }

        if (pauseButton != null)
        {
            pauseButton.onClick.AddListener(PauseGame); // Chama a fun��o de pausa ao clicar no bot�o
        }

        // Carregar o n�vel atual ao iniciar (Se n�o houver n�vel salvo, iniciar Level1)
        if (PlayerPrefs.HasKey("LastLevel"))
        {
            currentLevel = PlayerPrefs.GetString("LastLevel");
        }
        else
        {
            currentLevel = "Level1"; // Padr�o para Level1
        }
    }

    // Fun��o chamada para continuar o jogo
    public void ContinueGame()
    {
        if (!string.IsNullOrEmpty(currentLevel))
        {
            // Retoma o jogo a partir do n�vel salvo
            Time.timeScale = 1f;  // Garante que o tempo n�o esteja pausado
            isPaused = false;     // Marca o jogo como n�o pausado
            SceneManager.LoadScene(currentLevel); // Recarrega a cena salva
            Debug.Log("Carregando o n�vel: " + currentLevel); // Adicionado para debugging
        }
        else
        {
            Debug.LogWarning("Nenhum n�vel salvo encontrado!");
        }
    }

    // Fun��o chamada para resetar o progresso ou iniciar novo jogo
    public void ResetOrNewGame()
    {
        // Reseta o progresso e garante que o tempo do jogo seja retomado
        PlayerPrefs.SetInt("UnlockedLevels", 1); // Reseta o progresso
        PlayerPrefs.Save();

        // Certifique-se de que o jogo n�o comece pausado
        Time.timeScale = 1f;  // Retoma o tempo
        isPaused = false;     // Marca o jogo como n�o pausado

        // Carrega o primeiro n�vel
        SceneManager.LoadScene("Level1");
        currentLevel = "Level1"; // Atualiza o n�vel atual
    }

    // Fun��o para retornar ao menu principal
    public void ReturnToMainMenu()
    {
        // Se o jogo estiver pausado, retoma antes de ir para o menu
        if (isPaused)
        {
            ResumeGame(); // Retoma o jogo, removendo o pause
        }

        // Carrega a cena do menu principal
        SceneManager.LoadScene("Menu"); // Carrega o menu principal
    }

    // Fun��o chamada para pausar o jogo
    void PauseGame()
    {
        if (!isPaused) // S� pausa se n�o estiver pausado
        {
            // Salva o nome da cena atual para continuar depois
            currentLevel = SceneManager.GetActiveScene().name;
            PlayerPrefs.SetString("LastLevel", currentLevel); // Salva o n�vel atual
            PlayerPrefs.Save();

            // Carrega a cena de pausa de forma aditiva
            SceneManager.LoadScene("Pause", LoadSceneMode.Additive);

            // Pausa o tempo do jogo
            Time.timeScale = 0f;
            isPaused = true; // Marca o jogo como pausado
        }
    }

    // Fun��o para retomar o jogo
    public void ResumeGame()
    {
        if (isPaused) // S� retoma o jogo se estiver pausado
        {
            // Descarrega a cena de pausa
            SceneManager.UnloadSceneAsync("Pause");

            // Retoma o tempo do jogo
            Time.timeScale = 1f;
            isPaused = false; // Marca o jogo como n�o pausado
        }
    }

    // Fun��o para garantir que exista apenas um EventSystem na cena
    private void EnsureSingleEventSystem()
    {
        EventSystem[] eventSystems = FindObjectsOfType<EventSystem>();
        if (eventSystems.Length > 1)
        {
            for (int i = 1; i < eventSystems.Length; i++)
            {
                Destroy(eventSystems[i].gameObject);
            }
        }
    }

    // Fun��o para garantir que exista apenas um AudioListener na cena
    private void EnsureSingleAudioListener()
    {
        AudioListener[] audioListeners = FindObjectsOfType<AudioListener>();
        if (audioListeners.Length > 1)
        {
            for (int i = 1; i < audioListeners.Length; i++)
            {
                audioListeners[i].enabled = false;
            }
        }
    }

    // Atualiza o estado do jogo com a tecla "P" para pausar e "O" para despausar
    void Update()
    {
        // Verifica se o jogador pressionou a tecla "P" para pausar
        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();  // Pausa o jogo
        }

        // Verifica se o jogador pressionou a tecla "O" para despausar
        if (Input.GetKeyDown(KeyCode.O))
        {
            ResumeGame();  // Retoma o jogo
        }
    }
}
