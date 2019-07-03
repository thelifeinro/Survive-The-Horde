using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public GameObject[] npcs;
    public Transform spawnLocation;
    public int maxOnMap;

    // Update is called once per frame
    void Update()
    {
        //always count the 'Healthy' tagged objects
        int count = CountNPCs();
        int healthies = PlayerStats.instance.CountHealthyPpl();

        if (healthies < 0)
            return;

        if (count > Mathf.Min(healthies, maxOnMap))
        {
            RemoveNPCs(count - Mathf.Min(healthies, maxOnMap));
            return;
        }

        if(count < maxOnMap)
        {
            SpawnNPCs(Mathf.Min(healthies, maxOnMap) - count);
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    private int CountNPCs()
    {
        return GameObject.FindGameObjectsWithTag("Healthy").Length;
    }

    private void SpawnNPCs(int count) {
        if (count == 0)
            return;
        for (int i = 0; i < count; i++)
            Instantiate(GetRandomToSpawn(), spawnLocation.position, spawnLocation.rotation);
        //Debug.Log("NPC Manager spawned " + count);
    }

    private void RemoveNPCs(int count)
    {
        //remove some of them if necessary (eg: went to mission)
        GameObject[] existingNPCs = GameObject.FindGameObjectsWithTag("Healthy");
        for(int i=0; i < count; i++)
        {
            Destroy(existingNPCs[i]);
        }
        //Debug.Log("NPC Manager removed " + count);
    }

    private GameObject GetRandomToSpawn()
    {
        int i = Random.Range(0, npcs.Length);
        return npcs[i];
    }

}
