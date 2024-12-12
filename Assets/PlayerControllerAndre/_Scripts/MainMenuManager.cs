using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TarodevController;

public class MainMenuManager : MonoBehaviour
{
    [Header("Menu Objects")]
    [SerializeField] private ScriptableStats _stats;
    [SerializeField] private SelectManager _selectManager;
    [SerializeField] private GameObject _gameStoy;
    [SerializeField] private GameObject _boyWhite;
    [SerializeField] private GameObject _girlWhite;
    [SerializeField] private GameObject _boyBlack;
    [SerializeField] private GameObject _girlBlack;

    [Header("First Selected Options")]
    [SerializeField] private GameObject _menuFirst;
    [SerializeField] private GameObject _menuSecond;
    [SerializeField] private GameObject _menuThird;
    [SerializeField] private GameObject _controlFirst;
    [SerializeField] private GameObject _playerSelectFirst;
    [SerializeField] private GameObject _creditsFirst;

    [Header("Animation Settings")]
    [SerializeField] private Transform _object1; // Primeiro objeto para animar
    [SerializeField] private Transform _object2; // Segundo objeto para animar
    [SerializeField] private Transform _object3; // Terceiro objeto para animar
    [SerializeField] private Transform _object4; // Quarto objeto para animar
    [SerializeField] private AnimationCurve _curve; // Curva de animação
    [SerializeField] private float _distance = 100f; // Distância de movimento
    [SerializeField] private float _duration = 1f; // Duração da animação
    [SerializeField] private float _selectedDelay = 1f;

    [Header("Transitions")]
    [SerializeField] private GameObject _transition1;
    [SerializeField] private GameObject _transition2;
    [SerializeField] private GameObject _transition3;
    [SerializeField] private GameObject _transition4;

    [Header("Audio Manager")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioSource _audioSource2;
    [SerializeField] private AudioClip _story;
    [SerializeField] private AudioClip _select;
    [SerializeField] private AudioClip _playerSelected;
    [SerializeField] private AudioClip _openCan;

    private bool _canPress = true;

    void Start()
    {
        EventSystem.current.SetSelectedGameObject(_menuFirst);
    }

    #region Menu Button Actions

    public void SelectItem()
    {
        PlayAudio(_select, 0.1f, 1.5f);
    }

    public void StartGame()
    {
        if (_canPress)
        {
            PlayAudio(_select, 0.3f, 1.8f);
            StartCoroutine(AnimateObjects2(Vector3.left));
            EventSystem.current.SetSelectedGameObject(_playerSelectFirst);
            StartCoroutine(MenuDelay());
        }

        Cursor.lockState = CursorLockMode.Locked; // Trava o cursor no centro da tela
        Cursor.visible = false; // Oculta o cursor
    }

    public void CloseSelectionPlayerMenu()
    {
        if (_canPress)
        {
            StartCoroutine(AnimateObjects2(Vector3.right));
            EventSystem.current.SetSelectedGameObject(_menuFirst);
            StartCoroutine(MenuDelay());
        }
    }

    public void OpenControlMenu()
    {
        if (_canPress)
        {
            StartCoroutine(AnimateObjects(Vector3.left));
            EventSystem.current.SetSelectedGameObject(_controlFirst);
            StartCoroutine(MenuDelay());
        }
    }

    public void CloseControlMenu()
    {
        if (_canPress)
        {
            StartCoroutine(AnimateObjects(Vector3.right));
            EventSystem.current.SetSelectedGameObject(_menuSecond);
            StartCoroutine(MenuDelay());
        }
    }

    public void OpenCreditsMenu()
    {
        if (_canPress)
        {
            StartCoroutine(AnimateObjects4(Vector3.left));
            EventSystem.current.SetSelectedGameObject(_creditsFirst);
            StartCoroutine(MenuDelay());
        }
    }

    public void CloseCreditsMenu()
    {
        if (_canPress)
        {
            StartCoroutine(AnimateObjects4(Vector3.right));
            EventSystem.current.SetSelectedGameObject(_menuThird);
            StartCoroutine(MenuDelay());
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SelectBoyWhite()
    {
        if (_boyWhite != null && _canPress)
        {
            PlayAudio(_playerSelected, 0.2f, 1.5f);
            _selectManager._playerSelected = true;
            EventSystem.current.SetSelectedGameObject(null);

            Transform currentParent = _boyWhite.transform.parent;
            // Verifica se o objeto tem um pai
            if (currentParent.parent != null)
            {
                _boyWhite.transform.SetParent(currentParent.parent, false);
                MoverObjeto(_boyWhite, Vector3.right, 220f, 500f);
            }

            StartCoroutine(AnimateObjects3(Vector3.right));
            _stats.isMan = true;
            _stats.isWhite = true;
        }
    }

    public void SelectBoyBlack()
    {
        if (_boyBlack != null && _canPress)
        {
            PlayAudio(_playerSelected, 0.2f, 1.5f);
            _selectManager._playerSelected = true;
            EventSystem.current.SetSelectedGameObject(null);

            Transform currentParent = _boyBlack.transform.parent;
            // Verifica se o objeto tem um pai
            if (currentParent.parent != null)
            {
                _boyBlack.transform.SetParent(currentParent.parent, false);
                MoverObjeto(_boyBlack, Vector3.right, 388f, 500f);
            }

            StartCoroutine(AnimateObjects3(Vector3.right));
            _stats.isMan = true;
            _stats.isWhite = false;
        }
    }

    public void SelectGirlWhite()
    {
        if (_girlWhite != null && _canPress)
        {
            PlayAudio(_playerSelected, 0.2f, 1.5f);
            _selectManager._playerSelected = true;
            EventSystem.current.SetSelectedGameObject(null);

            Transform currentParent = _girlWhite.transform.parent;
            // Verifica se o objeto tem um pai
            if (currentParent.parent != null)
            {
                _girlWhite.transform.SetParent(currentParent.parent, false);
                MoverObjeto(_girlWhite, Vector3.left, 338f, 500f);
            }
            StartCoroutine(AnimateObjects3(Vector3.left));
            _stats.isMan = false;
            _stats.isWhite = true;
        }
    }

    public void SelectGirlBlack()
    {
        if (_girlBlack != null && _canPress)
        {
            PlayAudio(_playerSelected, 0.2f, 1.5f);
            _selectManager._playerSelected = true;
            EventSystem.current.SetSelectedGameObject(null);

            Transform currentParent = _girlBlack.transform.parent;
            // Verifica se o objeto tem um pai
            if (currentParent.parent != null)
            {
                _girlBlack.transform.SetParent(currentParent.parent, false);
                MoverObjeto(_girlBlack, Vector3.left, 170f, 500f);
            }
            StartCoroutine(AnimateObjects3(Vector3.left));
            _stats.isMan = false;
            _stats.isWhite = false;
        }
    }

    public void MoverObjeto(GameObject objeto, Vector3 direcao, float distancia, float velocidade)
    {
        StartCoroutine(MoverObjetoCoroutine(objeto, direcao, distancia, velocidade));
    }

    private IEnumerator MoverObjetoCoroutine(GameObject objeto, Vector3 direcao, float distancia, float velocidade)
    {
        if (objeto == null)
        {
            Debug.LogWarning("Objeto inválido!");
            yield break;
        }

        Vector3 posicaoInicial = objeto.transform.position;
        Vector3 posicaoAlvo = posicaoInicial + direcao.normalized * distancia;

        float distanciaPercorrida = 0f;

        while (distanciaPercorrida < distancia)
        {
            float movimento = velocidade * Time.deltaTime;
            distanciaPercorrida += movimento;

            // Evita ultrapassar o destino
            objeto.transform.position = Vector3.MoveTowards(objeto.transform.position, posicaoAlvo, movimento);

            if (Vector3.Distance(objeto.transform.position, posicaoAlvo) < 0.01f)
            {
                objeto.transform.position = posicaoAlvo;
                break;
            }

            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        _transition2.SetActive(true);
        StartCoroutine(FadeOutAudio(1));
        yield return new WaitForSeconds(1f);
        _transition3.SetActive(true);
        PlayAudio(_openCan, 1f, 1f);
        yield return new WaitForSeconds(2f);
        PlayAudio2(_story, 0.2f, 1f);
        _gameStoy.SetActive(true);
    }

    public void StartGameFinally()
    {
        StartCoroutine(StartGameFinallyCoroutine());
    }

    IEnumerator StartGameFinallyCoroutine()
    {
        PlayAudio(_playerSelected, 0.2f, 1.5f);
        _transition4.SetActive(true);
        StartCoroutine(FadeOutAudio(1));
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(1);
    }

    public void PlayAudio(AudioClip clip, float volume, float pitch)
    {
        if (_audioSource != null)
        {
            // Configura o volume e o pitch
            _audioSource.volume = volume;
            _audioSource.pitch = pitch;

            // Atribui o áudio e toca
            _audioSource.clip = clip;
            _audioSource.Play();
        }
        else
        {
            Debug.LogError("AudioSource não foi encontrado.");
        }
    }

    public void PlayAudio2(AudioClip clip, float volume, float pitch)
    {
        if (_audioSource2 != null)
        {
            // Configura o volume e o pitch
            _audioSource.volume = volume;
            _audioSource.pitch = pitch;

            // Atribui o áudio e toca
            _audioSource.clip = clip;
            _audioSource.Play();
        }
        else
        {
            Debug.LogError("AudioSource não foi encontrado.");
        }
    }

    public IEnumerator FadeOutAudio(float duration)
    {
        if (_audioSource2 != null && _audioSource2.isPlaying)
        {
            float startVolume = _audioSource2.volume;

            // Faz o fade out diminuindo o volume
            while (_audioSource2.volume > 0)
            {
                _audioSource2.volume -= startVolume * Time.deltaTime / duration;
                yield return null;
            }

            // Após o fade out, para o áudio e reseta o volume para o valor inicial
            _audioSource2.Stop();
            _audioSource2.volume = startVolume;
        }
    }

    #endregion

    IEnumerator MenuDelay()
    {
        _canPress = false;
        yield return new WaitForSeconds(_selectedDelay);
        _canPress = true;
    }

    private IEnumerator AnimateObjects(Vector3 direction)
    {
        Vector3 startPos1 = _object1.position;
        Vector3 targetPos1 = startPos1 + direction * _distance;

        Vector3 startPos2 = _object2.position;
        Vector3 targetPos2 = startPos2 + direction * _distance;

        float elapsedTime = 0f;

        while (elapsedTime < _duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / _duration;
            float curveValue = _curve.Evaluate(t);

            _object1.position = Vector3.Lerp(startPos1, targetPos1, curveValue);
            _object2.position = Vector3.Lerp(startPos2, targetPos2, curveValue);

            yield return null;
        }

        // Certificar-se de que os objetos terminam exatamente no alvo
        _object1.position = targetPos1;
        _object2.position = targetPos2;
    }

    private IEnumerator AnimateObjects2(Vector3 direction)
    {
        Vector3 startPos1 = _object1.position;
        Vector3 targetPos1 = startPos1 + direction * _distance;

        Vector3 startPos2 = _object3.position;
        Vector3 targetPos2 = startPos2 + direction * _distance;

        float elapsedTime = 0f;

        while (elapsedTime < _duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / _duration;
            float curveValue = _curve.Evaluate(t);

            _object1.position = Vector3.Lerp(startPos1, targetPos1, curveValue);
            _object3.position = Vector3.Lerp(startPos2, targetPos2, curveValue);

            yield return null;
        }

        // Certificar-se de que os objetos terminam exatamente no alvo
        _object1.position = targetPos1;
        _object3.position = targetPos2;
    }

    private IEnumerator AnimateObjects3(Vector3 direction)
    {
        Vector3 startPos1 = _object3.position;
        Vector3 targetPos1 = startPos1 + direction * _distance;

        float elapsedTime = 0f;

        while (elapsedTime < _duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / _duration;
            float curveValue = _curve.Evaluate(t);

            _object3.position = Vector3.Lerp(startPos1, targetPos1, curveValue);

            yield return null;
        }

        // Certificar-se de que os objetos terminam exatamente no alvo
        _object3.position = targetPos1;
    }

    private IEnumerator AnimateObjects4(Vector3 direction)
    {
        Vector3 startPos1 = _object1.position;
        Vector3 targetPos1 = startPos1 + direction * _distance;

        Vector3 startPos2 = _object4.position;
        Vector3 targetPos2 = startPos2 + direction * _distance;

        float elapsedTime = 0f;

        while (elapsedTime < _duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / _duration;
            float curveValue = _curve.Evaluate(t);

            _object1.position = Vector3.Lerp(startPos1, targetPos1, curveValue);
            _object4.position = Vector3.Lerp(startPos2, targetPos2, curveValue);

            yield return null;
        }

        // Certificar-se de que os objetos terminam exatamente no alvo
        _object1.position = targetPos1;
        _object4.position = targetPos2;
    }
}
