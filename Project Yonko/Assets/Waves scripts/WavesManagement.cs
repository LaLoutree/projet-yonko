using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WavesManagement : MonoBehaviour
{
    [SerializeField] public float zombie_number = 15f;
    [SerializeField] public int zombieRemains = 0;
    public int round = 0;
    public Transform[] zombieSpawnsLocation;
    private float TimeBetweenWaves = 5f;
    private float countdown = 5f;
    private float multiplier = 1;
    public GameObject zombie;
    [SerializeField] public List<GameObject> deadPlayers = new List<GameObject>();
    [SerializeField] public List<GameObject> alivePlayers = new List<GameObject>();
    void Update()
    {
        if (zombieRemains <= 0)
        {
            if (countdown <= 0)
            {
                zombie_number *= multiplier;
                SpawnWaves((int)zombie_number);
                multiplier *= 1.2f;
                round++;
                countdown = TimeBetweenWaves;
            }
            else
            {
                countdown -= Time.deltaTime;
            }
            RespawnDeadPlayers(deadPlayers);
        }
    }
    
    void SpawnWaves(int number)
    {
        for (int i = 0; i < number; i++)
        {
            Vector3 randomSpawnAround = zombieSpawnsLocation[Random.Range(0, zombieSpawnsLocation.Length)].position;
            Instantiate(zombie, randomSpawnAround, Quaternion.identity);
            zombieRemains++;
        }
    }

    void RespawnDeadPlayers(List<GameObject> deadPlayer)
    {
        foreach (GameObject player in deadPlayer)
        {
            Vector3 spawnPoint = new Vector3(165,1,135);
            player.transform.position = spawnPoint;
            alivePlayers.Add(player);
        }
        deadPlayers.Clear();
    }
}
