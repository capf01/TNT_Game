using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    public GameObject[] objectsToMove; // Array com os 8 objetos
    public float explosionDistance = 5f; // Dist�ncia m�xima que os objetos devem se afastar
    public float moveSpeed = 5f; // Velocidade do afastamento
    public float explosionDuration = 1f; // Dura��o da explos�o (opcional, pode ser removido se n�o for usado)

    private Vector3[] targetPositions; // Posi��es finais dos objetos
    public bool isExploding = false;

    void Start()
    {
        // Calcula as posi��es finais para cada objeto
        targetPositions = new Vector3[objectsToMove.Length];
        for (int i = 0; i < objectsToMove.Length; i++)
        {
            float angle = i * (360f / objectsToMove.Length);
            float radian = angle * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(Mathf.Cos(radian), Mathf.Sin(radian), 0f);
            targetPositions[i] = transform.position + direction * explosionDistance;
        }
    }

    public void TriggerExplosion()
    {
        isExploding = true;
    }

    void Update()
    {
        if (isExploding)
        {
            bool allObjectsReachedTarget = true;

            for (int i = 0; i < objectsToMove.Length; i++)
            {
                // Move o objeto em dire��o ao seu alvo
                objectsToMove[i].transform.position = Vector3.MoveTowards(
                    objectsToMove[i].transform.position,
                    targetPositions[i],
                    moveSpeed * Time.deltaTime
                );

                // Verifica se o objeto ainda n�o atingiu o alvo
                if (Vector3.Distance(objectsToMove[i].transform.position, targetPositions[i]) > 0.01f)
                {
                    allObjectsReachedTarget = false;
                }
            }

            // Para a explos�o quando todos os objetos atingirem o alvo
            if (allObjectsReachedTarget)
            {
                isExploding = false;
            }
        }
    }
}
