using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineralsManager : MonoBehaviour
{
    // public static Minerals instance;
    [field: SerializeField] GameObject[] minerals;

    // Array de probabilidades actuales de cada mineral
    [field: SerializeField] float[] mineralProbs;

    // Matriz, un set de probabilidades por cada inicio de capa
    // Si la capa actual sobrepasa el array, se usará el último set de probs
    [field: SerializeField] float[,] checkpointProbs;
    [field: SerializeField] int layerDepth;
    [field: SerializeField] float maxWait;
    [field: SerializeField] float minWait;
    [field: SerializeField] float spreadX;
    [field: SerializeField] float spreadY;

    void Start(){
        initProbs();
        for (int i = 0; i < mineralProbs.Length; i++){
            mineralProbs[i] = checkpointProbs[0,i];
        }

        // Bucle de generación de minerales
        StartCoroutine(GenerationLoop());
    }
    void initProbs(){
        checkpointProbs = new float[,] {
            {0.6f, 0.3f, 0.1f,   0f},
            {0.4f, 0.5f, 0.1f,   0f},
            {0.2f, 0.5f, 0.3f,   0f},
            {0.1f, 0.3f, 0.6f, 0.1f},
            {0.1f, 0.1f, 0.6f, 0.3f},
            {  0f,   0f, 0.4f, 0.6f} 
        };
    }

    /** Corutina que se encarga de generar cada x tiempo el mineral correspondiente. 
        El tiempo de espera se reduce conforme a la profundidad
    */
    IEnumerator GenerationLoop(){
        while (true)
        {
            AdjustProbs(Manager.instance.Deepness);
            GenerateOre(WhichOre());

            float waitTime = Math.Min(maxWait - (Manager.instance.Deepness / layerDepth), minWait);
            yield return new WaitForSeconds(waitTime);
        }
    }

    /** Devuelve, según la profundidad aportada y las probabilidades de aparición
        de cada mineral, que mineral debe generarse
    */
    GameObject WhichOre(){
        // Selecciona aleatoriamente un mineral a generar basado en las probabildades
        float randomValue = UnityEngine.Random.value;
        float cumulativeProbability = 0.0f;
        for (int i = 0; i < mineralProbs.Length; i++)
        {
            cumulativeProbability += mineralProbs[i];
            if (randomValue <= cumulativeProbability)
            {
                return minerals[i];
            }
        }
        return minerals[mineralProbs.Length - 1]; // Default to last mineral
    }

    /** Ajusta probabilidades de aparición de los minerales cada vez que generamos uno
    */
    void AdjustProbs(float depth){
        int lastMineral = mineralProbs.Length - 1;

        int numLayers = checkpointProbs.GetLength(0);
        int lastLayer = numLayers - 1;
       
        // Capa en la que nos encontramos, corresponde a un mineral
        if(depth == 0) {return;}
        
        int layer = (int) depth / layerDepth;
    
        if (layer >= lastLayer) {return;}
 
        int begDepth = layerDepth * layer;
        int endDepth = layerDepth * (layer + 1);

        // Sacamos un numero del 0 al 1 que indica que porcentaje de la capa ha sido atravesado
        // Si queréis utilizarlo para otra cosa, se declara arriba y se calcula así
        float posInLayer = (depth - begDepth) / (endDepth - begDepth);

        for (int i = 0; i < mineralProbs.Length; i++){
            mineralProbs[i] = checkpointProbs[layer + 1, i] * posInLayer + checkpointProbs[layer, i] * (1 - posInLayer);
        }

    }
    
    void GenerateOre(GameObject ore)
    {
        // Generar posición aleatoria dentro de una esfera alrededor del punto de transformación
        Vector3 randomPosition = Vector3.Scale(UnityEngine.Random.insideUnitSphere, new Vector3(spreadX, spreadY, 10f));

        randomPosition += transform.position - new Vector3(0,24,0);

        // Crear y destruir el clon de mineral
        GameObject clone = Instantiate(ore, randomPosition, Quaternion.identity);
    }
}
