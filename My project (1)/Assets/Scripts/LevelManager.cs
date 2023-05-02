using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance = null;
    public Barrack enemyBarrack;

    public Stage currentStage;
    public Level currentLevel;

    public bool levelClear;
    public int num_Stage;
    public int num_Level;


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
    public List<Stage> list_Stage = new List<Stage>();


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
        num_Stage = 0;
        num_Level = 0;

        for (int i = 0; i < list_Stage.Count; i++)
        {
            list_Stage[i].name_stage = "Stage: " + (i + 1);
            for (int j = 0; j < list_Stage[i].list_Levels.Count; j++)
            {
                list_Stage[i].list_Levels[j].name_level = "Stage: " + (i + 1) + "/ " + "Level: " + (j + 1);

            }

        }

    }

    private void Update()
    {
        currentStage = list_Stage[num_Stage];
        currentLevel = list_Stage[num_Stage].list_Levels[num_Level];

    }

    void GoTo_NextStage()
    {
        ++num_Stage;
    }

    void GoTo_NextLevel()
    {
        ++num_Level;
    }
}

[System.Serializable]
public class Stage
{
    public string name_stage;
    
    public List<Level> list_Levels;
}

[System.Serializable]
public class Level
{
    public string name_level;

    public int num_spawns;
    public float termOfSpawn;
    //public SpawnMonsters[] spawnMonster;
    public List<SpawnMonsters> list_Monster;
    public Transform bossMonster;
}

[System.Serializable]
public class SpawnMonsters
{
    public SerializableDictionary<Transform, int> monsterType;
}

//https://catlikecoding.com/unity/tutorials/editor/custom-list/
