using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Collectible : MonoBehaviour
{
    // quantidade total de itens com cada tag
    public static int totalCollectibles;
    public static int totalHiddenCollectibles;

    // imagens de notifica��o na UI
    public Image collectibleTrophyUI; // trof�u para Collectibles
    public Image hiddenCollectibleTrophyUI; // trof�u para HiddenCollectibles

    // vari�veis para controlar os logs
    private bool hasLoggedCollectibles = false;
    private bool hasLoggedHiddenCollectibles = false;

    // chamado quando o objeto for carregado na cena
    private void Start()
    {
        // carregar os totais de itens restantes salvos
        totalCollectibles = PlayerPrefs.GetInt("TotalCollectibles", GameObject.FindGameObjectsWithTag("Collectible").Length);
        totalHiddenCollectibles = PlayerPrefs.GetInt("TotalHiddenCollectibles", GameObject.FindGameObjectsWithTag("HiddenCollectible").Length);

        // ocultar trof�us na UI inicialmente
        if (collectibleTrophyUI != null)
            collectibleTrophyUI.gameObject.SetActive(false);

        if (hiddenCollectibleTrophyUI != null)
            hiddenCollectibleTrophyUI.gameObject.SetActive(false);

        Debug.Log($"In�cio: {totalCollectibles} Collectibles e {totalHiddenCollectibles} HiddenCollectibles.");
    }

    // para o player coletar os itens
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // verificar coleta de "Collectible"
        if (collision.CompareTag("Collectible"))
        {
            totalCollectibles--; // decrementar
            Destroy(collision.gameObject); // remover item
            SaveCollectibleState("Collectible", collision.gameObject.transform.position);
            Debug.Log($"Item coletado. Restam {totalCollectibles} Collectibles.");
        }
        // verificar coleta de "HiddenCollectible"
        else if (collision.CompareTag("HiddenCollectible"))
        {
            totalHiddenCollectibles--; // decrementar
            Destroy(collision.gameObject); // remover item
            SaveCollectibleState("HiddenCollectible", collision.gameObject.transform.position);
            Debug.Log($"Colet�vel escondido encontrado. Restam {totalHiddenCollectibles} HiddenCollectibles.");
        }

        // verificar se todos os itens de cada categoria foram coletados
        CheckCompletion();
    }

    // salvar o estado do item coletado (marcando como coletado)
    private void SaveCollectibleState(string tag, Vector3 position)
    {
        // salvar um valor indicando que o item foi coletado
        PlayerPrefs.SetInt(tag + "Collected_" + position.x + "_" + position.y, 1);

        // salvar quantidade restante de cada categoria
        PlayerPrefs.SetInt(tag + "Total", totalCollectibles);
    }

    // verificar se todos os itens de uma categoria foram coletados
    private void CheckCompletion()
    {
        // verificar se todos os "Collectibles" foram coletados
        if (totalCollectibles <= 0 && !hasLoggedCollectibles)
        {
            Debug.Log("Parab�ns! Todos os Collectibles coletados.");
            ShowTrophy(collectibleTrophyUI); // mostrar trof�u para Collectibles
            hasLoggedCollectibles = true; // impedir que o log apare�a novamente
        }

        // verificar se todos os "HiddenCollectibles" foram coletados
        if (totalHiddenCollectibles <= 0 && !hasLoggedHiddenCollectibles)
        {
            Debug.Log("Parab�ns! Todos os HiddenCollectibles coletados.");
            ShowTrophy(hiddenCollectibleTrophyUI); // mostrar trof�u para HiddenCollectibles
            hasLoggedHiddenCollectibles = true; // impedir que o log apare�a novamente
        }
    }

    // mostrar trof�u e programar desaparecimento
    private void ShowTrophy(Image trophyUI)
    {
        // verificar se a refer�ncia n�o � nula antes de acessar
        if (trophyUI != null)
        {
            Debug.Log($"Exibindo trof�u: {trophyUI.name}");
            trophyUI.gameObject.SetActive(true); // exibir trof�u
            StartCoroutine(HideTrophyAfterDelay(trophyUI, 2f)); // ocultar ap�s 2s
        }
    }

    // corrotina para ocultar o trof�u depois de um tempo
    private IEnumerator HideTrophyAfterDelay(Image trophyUI, float delay)
    {
        yield return new WaitForSeconds(delay); // esperar pelo tempo especificado
        trophyUI.gameObject.SetActive(false); // ocultar trof�u
    }

    // verificar se o item j� foi coletado antes de exibi-lo
    private void OnEnable()
    {
        Vector3 position = transform.position;

        // Verificar se o item foi coletado antes, se sim, destruir o item
        if (PlayerPrefs.GetInt("Collected_" + position.x + "_" + position.y, 0) == 1)
        {
            Destroy(gameObject); // se o item j� foi coletado
        }
    }
}
