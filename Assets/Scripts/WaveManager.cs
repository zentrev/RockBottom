using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
public class WaveManager : Singleton<WaveManager>
{
    [SerializeField] [Range(1, 15)] int m_waves = 5;

    public bool NextWaveAvalibe { get; set; } = true;
    public int CurentWave { get; set; } = 0;
    [SerializeField] GameObject[] m_enemys = null;
    [SerializeField] Transform[] m_spawns = null;
    [SerializeField] Transform[] m_targets = null;

    [SerializeField] [Range(1, 15)] int m_minEnemys = 1;
    [SerializeField] [Range(1, 25)] int m_maxEnemys = 15;
    [SerializeField] AnimationCurve m_enemyCount = null;

    [SerializeField] Transform m_dynamicRoot = null;

    private List<GameObject> m_aliveAgents = new List<GameObject>();

    void Start()
    {
        NextWave();
    }

    public void NextWave()
    {
        CurentWave++;
        NextWaveAvalibe = false;
        int enemyWaveCount = Mathf.RoundToInt(m_enemyCount.Evaluate(m_waves / CurentWave) * m_maxEnemys);
        enemyWaveCount = Mathf.Clamp(enemyWaveCount, m_minEnemys, m_maxEnemys);

        for(int i = 0; i < enemyWaveCount; i++)
        {
            GameObject enemyToSpawn = m_enemys[Random.Range(0, m_enemys.Length)];
            Transform spawnLocation = m_spawns[Random.Range(0, m_spawns.Length)];
            GameObject spawnedEnemy = Instantiate(enemyToSpawn, spawnLocation.position, spawnLocation.rotation, m_dynamicRoot);
            if(spawnedEnemy.TryGetComponent(out NavmeshNavigation nav))
            {
                nav.Target = m_targets[Random.Range(0, m_targets.Length)];
            }

            m_aliveAgents.Add(spawnedEnemy);
        }
    }

    public void RemoveMe(GameObject enemy)
    {
        if(m_aliveAgents.Contains(enemy)) m_aliveAgents.Remove(enemy);
        if(m_aliveAgents.Count == 0)
        {
            NextWaveAvalibe = true;
            NextWave();
        }
    }
}
