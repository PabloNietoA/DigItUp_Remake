using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minerals : MonoBehaviour
{
    [field: SerializeField] public GameObject[] Ores { get; set;}
    [field: SerializeField] public float[] MineralProbs { get; set;}
    [field: SerializeField] public float[] CheckpointProbs { get; set;}
    [field: SerializeField] public int DepthPerShift { get; set;}
    [field: SerializeField] public int MaxWait{ get; set;}
    [field: SerializeField] public int MinWait{ get; set;}
    [field: SerializeField] public float SpreadX { get; set;}
    [field: SerializeField] public float SpreadY { get; set;}


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
            // GenerateOre(WhichOre());

            // Print probabilidades cada vez que se cambian
            for(int i = 0; i < MineralProbs.Length; i++)
            {
                Console.Write(MineralProbs[i]);
            }

            int waitTime = MaxWait - ( (int) Manager.Deepness) / 10;
            yield return new WaitForSeconds(Math.Min(waitTime, MinWait));
        }
    }

    /** Devuelve, según la profundidad aportada y las probabilidades de aparición
        de cada mineral, que Ore debe generarse
    */
    GameObject WhichOre(){
        // Randomly select a mineral based on adjusted probabilities
        float randomValue = UnityEngine.Random.value;
        float cumulativeProbability = 0.0f;
        for (int i = 0; i < MineralProbs.Length; i++)
        {
            cumulativeProbability += MineralProbs[i];
            if (randomValue <= cumulativeProbability)
            {
                return Ores[i];
            }
        }
        return Ores[MineralProbs.Length - 1]; // Default to last mineral
    }

    /** Ajusta probabilidades de aparición de los minerales cada vez que generamos uno
    */
    void AdjustProbs(float depthLevel){
        int numMinerals = MineralProbs.Length;
        int lastMineral = numMinerals - 1;
        
        // Capa en la que nos encontramos, corresponde a un mineral
        int layer = (int) Math.Min(depthLevel / DepthPerShift, lastMineral);

        int begDepth = DepthPerShift * layer;
        int endDepth = DepthPerShift * (layer + 1);

        // Sacamos un numero del 0 al 1 que indica que porcentaje de la capa ha sido atravesado
        // Si queréis utilizarlo para otra cosa, se declara arriba y se calcula así
        float posInLayer = (depthLevel - begDepth) / (endDepth - begDepth);
        
        float lastValue = MineralProbs[layer];
        MineralProbs[layer] = CheckpointProbs[layer] - (CheckpointProbs[layer] * posInLayer) / 2 ;
        TraspassProbability(layer, MineralProbs[layer] - lastValue); // Se traspasa el cambio de prob

        // Se inicializa el siguiente mineral que no ha aparecido todavía para que la función traspase 
        // probabilidad correctamente
        if(layer + 2 < lastMineral && layer > 0){
            MineralProbs[layer + 2] = 0.1f;
            MineralProbs[layer - 1] -= 0.1f;
        }

        if(layer > 0){
            TraspassProbability(layer - 1, MineralProbs[layer - 1]);
            MineralProbs[layer - 1] = 0;
        }
    }

    /** Esta función coge la probabilidad del mineral del indice aportado 
        y la reparte entre los dos siguientes, proporcionalmente a la que 
        tienen acutalmente. 
    */
    void TraspassProbability(int index, float prob){
        if (index + 1 >= MineralProbs.Length)
            return;
            
        float nextProb = MineralProbs[index + 1];
        if (index + 2 >= MineralProbs.Length)
        {
            MineralProbs[index + 1] += prob;
            return;
        }

        float nextNextProb = MineralProbs[index + 2];
        float totalNextProb = nextProb + nextNextProb;

        MineralProbs[index + 1] += nextProb * prob / totalNextProb;
        MineralProbs[index + 2] += nextNextProb * prob / totalNextProb;
    }
    
    // void GenerateOre(GameObject ore)
    // {
    //     // Generar posición aleatoria dentro de una esfera alrededor del punto de transformación
    //     Vector3 randomPosition = Vector3.Scale(UnityEngine.Random.insideUnitSphere, new Vector3(SpreadX, SpreadY, 10f));

    //     // Crear y destruir el clon de mineral
    //     GameObject clone = Instantiate(ore, randomPosition, Quaternion.identity);
    // }
}