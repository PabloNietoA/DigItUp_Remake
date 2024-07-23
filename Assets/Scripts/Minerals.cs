using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minerals : MonoBehaviour
{
    [SerializeField] private GameObject[] Ores; // Array containing all types of Ores, the object
    [SerializeField] private float[] MineralProb; // Probabilidades de cada mineral (inicializo en start o desde unity?)
    [SerializeField] private float[] CheckpointProb; // Probablidades que tienen al empezar la capa actual 
    [SerializeField] private int DepthPerShift; // Determina cada cuanta profundidad cambia el mineral predominante
    [SerializeField] private (int min, int max) GenerationPeriod; // Tiempo en ms entre generación de minerales
    [SerializeField] private (float X, float Y) Spread; // Ambas en tupla pq pq no
    

    void Start(){
        // Valores de prueba, maybe setearlos desde el editor de unity
        // MineralProb = [0.6, 0.3, 0.1, 0]; 
        // GenerationPeriod = (200, 5000);

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
            AdjustProbs(Manager.Deepness);
            GenerateOre(WhichOre());

            int waitTime = (int) GenerationPeriod.max - (Manager.Deepness) / 10;
            Thread.Sleep(Math.Min(waitTime, GenerationPeriod.min) );
        }
    }

    /** Devuelve, según la profundidad aportada y las probabilidades de aparición
        de cada mineral, que Ore debe generarse
    */
    GameObject WhichOre(){
        // Randomly select a mineral based on adjusted probabilities
        float randomValue = UnityEngine.Random.value;
        float cumulativeProbability = 0.0f;
        for (int i = 0; i < MineralProb.Length; i++)
        {
            cumulativeProbability += MineralProb[i];
            if (randomValue <= cumulativeProbability)
            {
                return Ores[i];
            }
        }
        return Ores[MineralProb.Length - 1]; // Default to last mineral
    }

    /** Ajusta probabilidades de aparición de los minerales cada vez que generamos uno
    */
    void AdjustProbs(float depthLevel){
        int numMinerals = MineralProb.Length;
        int lastMineral = numMinerals - 1;
        
        // Capa en la que nos encontramos, corresponde a un mineral
        int layer = (int) Math.Min(depthLevel / DepthPerShift, lastMineral);

        int begDepth = DepthPerShift * layer;
        int endDepth = DepthPerShift * (layer + 1);

        // Sacamos un numero del 0 al 1 que indica que porcentaje de la capa ha sido atravesado
        // Si queréis utilizarlo para otra cosa, se declara arriba y se calcula así
        float posInLayer = (depthLevel - begDepth) / (endDepth - begDepth);
        
        float lastValue = MineralProb[layer];
        MineralProb[layer] = CheckpointProb[layer] - (CheckpointProb[layer] * posInLayer) / 2 ;
        TraspassProbability(layer, MineralProb[layer] - lastValue); // Se traspasa el cambio de prob

        // Se inicializa el siguiente mineral que no ha aparecido todavía para que la función traspase 
        // probabilidad correctamente
        if(layer + 2 < numMinerals){
            MineralProb[layer + 2] = 0.1f;
            MineralProb[layer - 1] -= 0.1f;
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
    
    void GenerateOre(GameObject ore)
    {
        // Generar posición aleatoria dentro de una esfera alrededor del punto de transformación
        Vector3 randomPosition = UnityEngine.Random.insideUnitSphere * Spread;  //+ transform.position

        // Crear y destruir el clon de mineral
        GameObject clone = Instantiate(ore, randomPosition, Quaternion.identity);
        Destroy(clone, 30f / (GetComponentInParent<Player>().Speed / 10f));
    }
}