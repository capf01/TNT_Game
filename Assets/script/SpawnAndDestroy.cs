using UnityEngine;
using System.Collections;

public class SpawnAndDestroy : MonoBehaviour
{
    public GameObject objectToSpawn; // Objeto a ser instanciado
    public float spawnInterval = 5f; // Intervalo entre os spawns
    public float destroyAfter = 10f; // Tempo ap�s o qual o objeto ser� destru�do

    private void Start()
    {
        // Come�a a cria��o do objeto
        StartCoroutine(SpawnObject());
    }

    private IEnumerator SpawnObject()
    {
        while (true)
        {
            // Instancia o objeto na posi��o do objeto com o script anexado
            GameObject spawnedObject = Instantiate(objectToSpawn, transform.position, Quaternion.identity);

            // Destr�i o objeto ap�s 'destroyAfter' segundos
            Destroy(spawnedObject, destroyAfter);

            // Espera o intervalo de tempo antes de criar o pr�ximo objeto
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
