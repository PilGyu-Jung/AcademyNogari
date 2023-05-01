using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance = null;
    public Barrack enemyBarrack;

    public Stage currentStage;

    public static LevelManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    public List<Stage> StageList = new List<Stage>();


    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        currentStage = StageList[0];
        currentStage.num_Stage = 0;
        currentStage.num_Levels = 0;
        //enemyBarrack.coolT = currentStage
    }
}

[System.Serializable]
public class Stage
{
    public int num_Stage;
    public int num_Levels;
    public Level[] levels;
}

[System.Serializable]
public class Level
{
    public int num_spawns;
    public float termOfSpawn;
    public SpawnMonsters[] spawnMonster;
    public Transform bossMonster;
}

[System.Serializable]
public class SpawnMonsters
{
    public SerializableDictionary<Transform, int> monsterType;
}

