using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minerals : MonoBehaviour
{
    [SerializeField] private GameObject[] Ores; // Array containing all types of Ores, the object
    [SerializeField] private float[] MineralProb; // Probabilidades de cada mineral (inicializo en start o desde unity?)
    [SerializeField] private float[] CheckpointProb; // Probablidades que tienen al empezar la capa actual 
    [SerializeField] private int DepthPerShift; // Determina cada cuanta profundidad cambia el mineral predominante
    [SerializeField] private (float min, float max) GenerationPeriod; // Tiempo en ms entre generación de minerales
    [SerializeField] private (float X, float Y) Spread; // Ambas en tupla pq pq no
    

    void Start(){
        // Valores de prueba, maybe setearlos desde el editor de unity
        MineralProb = {0.6, 0.3, 0.1, 0}; 
        GenerationPeriod = (200, 5000);

        // Bucle de generación de minerales
        StartCoroutine(GenerationLoop());
    }

    /** <summary>
        Corutina que se encarga de generar cada x tiempo el mineral correspondiente. 
        El tiempo de espera se reduce conforme a la profundidad
    */
    IEnumerator GenerationLoop(){
        while (true)
        {
            GenerateOre(WhichOre(Manager.Deepness));

            int waitTime = GenerationPeriod.max - (Manager.Deepness) / 10;
            yield return WaitForSeconds( Math.min(waitTime, GenerationPeriod.min) );

            AdjustProbs(Manager.Deepness);
        }
    }

    /** Devuelve, según la profundidad aportada y las probabilidades de aparición
        de cada mineral, que Ore debe generarse
    */
    GameObject WhichOre(int depthLevel){

        // Adjust probabilities based on depth level
        float[] adjustedProbabilities = AdjustProbs(depthLevel);

        // Randomly select a mineral based on adjusted probabilities
        float randomValue = Random.value;
        float cumulativeProbability = 0.0f;
        for (int i = 0; i < mineralPrefabs.Length; i++)
        {
            cumulativeProbability += adjustedProbabilities[i];
            if (randomValue <= cumulativeProbability)
            {
                return Ores[i];
            }
        }
        return Ores[mineralPrefabs.Length - 1]; // Default to last mineral
    }

    /** Ajusta probabilidades de aparición de los minerales cada vez que generamos uno
    */
    void AdjustProbs(int depthLevel){
        int numMinerals = MineralProb.Length;
        int lastMineral = numMinerals - 1;
        
        // Capa en la que nos encontramos, corresponde a un mineral
        int layer = Math.Min(depthLevel / depthPerShift, lastMineral);

        int begDepth = DepthPerShift * layer;
        int endDepth = DepthPerShift * (layer + 1);

        // Sacamos un numero del 0 al 1 que indica que porcentaje de la capa ha sido atravesado
        // Si queréis utilizarlo para otra cosa, se declara arriba y se calcula así
        float posInLayer = (depthLevel - begDepth) / (endDepth - begDepth);
        
        int lastValue = MineralProb[layer];
        MineralProb[layer] = CheckpointProb[layer] - (CheckpointProb[layer] * posInLayer) / 2 ;
        TraspassProbability(layer, MineralProb[layer] - lastValue); // Se traspasa el cambio de prob

        // Se inicializa el siguiente mineral que no ha aparecido todavía para que la función traspase 
        // probabilidad correctamente
        if(layer + 2 < numMinerals){
            MineralProb[layer + 2] = 0.1;
            MineralProb[layer - 1] -= 0.1;
        }

        TraspassProbability(layer - 1, MineralProb[layer - 1]);
        MineralProb[layer - 1] = 0;

    }

    /** Esta función coge la probabilidad del mineral del indice aportado 
        y la reparte entre los dos siguientes, proporcionalmente a la que 
        tienen acutalmente. 
    */
    void TraspassProbability(int index, float prob){
        if (index + 1 >= MineralProb.Length)
            return;
            
        float nextProb = MineralProb[index + 1];
        if (index + 2 >= MineralProb.Length)
        {
            MineralProb[index + 1] += prob;
            return;
        }

        float nextNextProb = MineralProb[index + 2];
        float totalNextProb = nextProb + nextNextProb;

        MineralProb[index + 1] += nextProb * prob / totalNextProb;
        MineralProb[index + 2] += nextNextProb * prob / totalNextProb;
    }

    // USAR ESTA O LA DE ARRIBA
    // void TraspassProbability(int index, float prob){
    //     float x = 0; float y = 0;

    //     if (index + 1 < MineralProb.Length){
    //         x = MineralProb[index + 1];

    //         if( index + 2 < MineralProb.Length){
    //             y = MineralProb[index + 2];
    //             MineralProb[index + 2] += y * prob / (x + y);
    //         }

    //         MineralProb[index + 1] += x * prob / (x + y);
    //     }
    // }
    
    void GenerateOre(GameObject ore)
    {
        // Generar posición aleatoria dentro de una esfera alrededor del punto de transformación
        Vector3 randomPosition = Random.insideUnitSphere * Spread + transform.position;

        // Crear y destruir el clon de mineral
        GameObject clone = Instantiate(ore, randomPosition, Quaternion.identity);
        Destroy(clone, 30f / (GetComponentInParent<Player>().Speed / 10f));
    }
}