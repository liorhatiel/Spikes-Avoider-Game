using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeSpawner : MonoBehaviour
{

    [SerializeField] GameObject[] spikes;
    GameManager gameManager;
    protected int maxSpikes;


    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }


    // This funcion will clear the spikes on each wall.
    public void Clear()
    {
        foreach (GameObject spike in spikes)
        {
            spike.SetActive(false);
        }
    }

    // This fuction will create new random spikes on the counter wall.
    public void SpawnRandomly()
    {
        maxSpikes = Random.Range(3, 5);
        int enabledSpikes = 0;
        int lengthOfSpikes = spikes.Length;


        // Set active spikes randomly.
        while (enabledSpikes < maxSpikes)
        {
            int randomIndex = Random.Range(0, spikes.Length);
            if (!(spikes[randomIndex].activeSelf))
            {
                spikes[randomIndex].SetActive(true);
                enabledSpikes++;
            }
        }

        // Make sure that the player will always have place to jump ON HIS FIRST JUMP.
        if (gameManager.score == 0)
        {
            spikes[3].SetActive(false);
            spikes[4].SetActive(false);

            // Set active one other spike that the WHILE loop doesn't active.
            for (int i = 0; i < lengthOfSpikes; i++)
            {
                if (!(spikes[i].activeSelf) && i != 3 && i != 4)
                {
                    spikes[i].SetActive(true);
                    break;
                }
            }
        }
     
    }


}



