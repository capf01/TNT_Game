using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LevelSelector : MonoBehaviour
{
    public Button continueButton; // Botão de continuar
    public Button resetOrNewGameButton; // Botão de resetar ou novo jogo
    public Button mainMenuButton; // Botão para retornar ao menu principal
    public Button pauseButton; // Botão de pausa
    public GameObject pauseMenu;  // O painel de pausa
    private bool isPaused = false; // Para controlar o estado de pausa
    private string currentLevel; // Para armazenar o nível atual do jogo

    void Start()
    {
        // Garantir que haja apenas um EventSystem e um AudioListener na cena
        EnsureSingleEventSystem();
        EnsureSingleAudioListener();

        // Configura os botões
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
            pauseButton.onClick.AddListener(PauseGame); // Chama a função de pausa ao clicar no botão
        }

        // Carregar o nível atual ao iniciar (Se não houver nível salvo, iniciar Level1)
        if (PlayerPrefs.HasKey("LastLevel"))
        {
            currentLevel = PlayerPrefs.GetString("LastLevel");
        }
        else
        {
            currentLevel = "Level1"; // Padrão para Level1
        }
    }

    // Função chamada para continuar o jogo
    public void ContinueGame()
    {
        if (!string.IsNullOrEmpty(currentLevel))
        {
            // Retoma o jogo a partir do nível salvo
            Time.timeScale = 1f;  // Garante que o tempo não esteja pausado
            isPaused = false;     // Marca o jogo como não pausado
            SceneManager.LoadScene(currentLevel); // Recarrega a cena salva
            Debug.Log("Carregando o nível: " + currentLevel); // Adicionado para debugging
        }
        else
        {
            Debug.LogWarning("Nenhum nível salvo encontrado!");
        }
    }

    // Função chamada para resetar o progresso ou iniciar novo jogo
    public void ResetOrNewGame()
    {
        // Reseta o progresso e garante que o tempo do jogo seja retomado
        PlayerPrefs.SetInt("UnlockedLevels", 1); // Reseta o progresso
        PlayerPrefs.Save();

        // Certifique-se de que o jogo não comece pausado
        Time.timeScale = 1f;  // Retoma o tempo
        isPaused = false;     // Marca o jogo como não pausado

        // Carrega o primeiro nível
        SceneManager.LoadScene("Level1");
        currentLevel = "Level1"; // Atualiza o nível atual
    }

    // Função para retornar ao menu principal
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

    // Função chamada para pausar o jogo
    void PauseGame()
    {
        if (!isPaused) // Só pausa se não estiver pausado
        {
            // Salva o nome da cena atual para continuar depois
            currentLevel = SceneManager.GetActiveScene().name;
            PlayerPrefs.SetString("LastLevel", currentLevel); // Salva o nível atual
            PlayerPrefs.Save();

            // Carrega a cena de pausa de forma aditiva
            SceneManager.LoadScene("Pause", LoadSceneMode.Additive);

            // Pausa o tempo do jogo
            Time.timeScale = 0f;
            isPaused = true; // Marca o jogo como pausado
        }
    }

    // Função para retomar o jogo
    public void ResumeGame()
    {
        if (isPaused) // Só retoma o jogo se estiver pausado
        {
            // Descarrega a cena de pausa
            SceneManager.UnloadSceneAsync("Pause");

            // Retoma o tempo do jogo
            Time.timeScale = 1f;
            isPaused = false; // Marca o jogo como não pausado
        }
    }

    // Função para garantir que exista apenas um EventSystem na cena
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

    // Função para garantir que exista apenas um AudioListener na cena
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
