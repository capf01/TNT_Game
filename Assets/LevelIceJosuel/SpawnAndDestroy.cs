using UnityEngine;
using System.Collections;

public class SpawnAndDestroy : MonoBehaviour
{
    public GameObject objectToSpawn; // Objeto a ser instanciado
    public float spawnInterval = 5f; // Intervalo entre os spawns
    public float destroyAfter = 10f; // Tempo após o qual o objeto será destruído

    private void Start()
    {
        // Começa a criação do objeto
        StartCoroutine(SpawnObject());
    }

    private IEnumerator SpawnObject()
    {
        while (true)
        {
            // Instancia o objeto na posição do objeto com o script anexado
            GameObject spawnedObject = Instantiate(objectToSpawn, transform.position, Quaternion.identity);

            // Destrói o objeto após 'destroyAfter' segundos
            Destroy(spawnedObject, destroyAfter);

            // Espera o intervalo de tempo antes de criar o próximo objeto
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
