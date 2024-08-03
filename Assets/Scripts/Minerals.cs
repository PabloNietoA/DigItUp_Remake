using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minerals : MonoBehaviour
{
    public static Minerals instance;
    [field: SerializeField] GameObject[] minerals;
    [field: SerializeField] float[] mineralProbs;
    [field: SerializeField] float[] checkpointProbs;
    [field: SerializeField] int depthPerShift;
    [field: SerializeField] int maxWait;
    [field: SerializeField] int minWait;
    [field: SerializeField] float spreadX;
    [field: SerializeField] float spreadY;


    void Start(){
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
            AdjustProbs(Manager.instance.Deepness);
            // GenerateOre(WhichOre());

            // Print probabilidades cada vez que se cambian
            // for(int i = 0; i < MineralProbs.Length; i++)
            // {
            //     Console.Write(MineralProbs[i]);
            // }

            int waitTime = maxWait - ( (int) Manager.instance.Deepness) / 10;

            yield return new WaitForSeconds(Math.Min(waitTime, minWait));
        }
    }

    /** Devuelve, según la profundidad aportada y las probabilidades de aparición
        de cada mineral, que Ore debe generarse
    */
    GameObject WhichOre(){
        // Randomly select a mineral based on adjusted probabilities
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
    void AdjustProbs(float depthLevel){
        int numMinerals = mineralProbs.Length;
        int lastMineral = numMinerals - 1;
        
        // Capa en la que nos encontramos, corresponde a un mineral
        int layer = (int) Math.Min(depthLevel / depthPerShift, lastMineral);

        int begDepth = depthPerShift * layer;
        int endDepth = depthPerShift * (layer + 1);

        // Sacamos un numero del 0 al 1 que indica que porcentaje de la capa ha sido atravesado
        // Si queréis utilizarlo para otra cosa, se declara arriba y se calcula así
        float posInLayer = (depthLevel - begDepth) / (endDepth - begDepth);
        
        float lastValue = mineralProbs[layer];
        mineralProbs[layer] = checkpointProbs[layer] - (checkpointProbs[layer] * posInLayer) / 2 ;
        TraspassProbability(layer, mineralProbs[layer] - lastValue); // Se traspasa el cambio de prob

        // Se inicializa el siguiente mineral que no ha aparecido todavía para que la función traspase 
        // probabilidad correctamente
        if(layer + 2 < lastMineral && layer > 0){
            mineralProbs[layer + 2] = 0.1f;
            mineralProbs[layer - 1] -= 0.1f;
        }

        if(layer > 0){
            TraspassProbability(layer - 1, mineralProbs[layer - 1]);
            mineralProbs[layer - 1] = 0;
        }
    }

    /** Esta función coge la probabilidad del mineral del indice aportado 
        y la reparte entre los dos siguientes, proporcionalmente a la que 
        tienen acutalmente. 
    */
    void TraspassProbability(int index, float prob){
        if (index + 1 >= mineralProbs.Length)
            return;
            
        float nextProb = mineralProbs[index + 1];
        if (index + 2 >= mineralProbs.Length)
        {
            mineralProbs[index + 1] += prob;
            return;
        }

        float nextNextProb = mineralProbs[index + 2];
        float totalNextProb = nextProb + nextNextProb;

        mineralProbs[index + 1] += nextProb * prob / totalNextProb;
        mineralProbs[index + 2] += nextNextProb * prob / totalNextProb;
    }
    
    // void GenerateOre(GameObject ore)
    // {
    //     // Generar posición aleatoria dentro de una esfera alrededor del punto de transformación
    //     Vector3 randomPosition = Vector3.Scale(UnityEngine.Random.insideUnitSphere, new Vector3(SpreadX, SpreadY, 10f));

    //     // Crear y destruir el clon de mineral
    //     GameObject clone = Instantiate(ore, randomPosition, Quaternion.identity);
    // }
}
