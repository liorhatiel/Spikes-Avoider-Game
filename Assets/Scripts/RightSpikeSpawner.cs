using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightSpikeSpawner : SpikeSpawner
{

    // This class inherted from other class -> The SpikeSpawner Class.

    void Start()
    {
        SpawnRandomly();                       // The player move to the right at the beginning, so at the beginning we spawn spikes randomly on the right wall.
    }


}
