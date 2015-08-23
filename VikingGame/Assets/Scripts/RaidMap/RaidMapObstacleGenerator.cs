using UnityEngine;
using System.Collections.Generic;

public class RaidMapObstacleGenerator : MonoBehaviour {
    public static RaidMapObstacleGenerator Instance;

    List<ObstacleMetaData> ObstacleMetaList;

    float budgetUnitLength = -1,
        budget = -1,
        remainingBudget = -1,
        finalAttempts = 0,
        maxFinalAttemps = 3;
    WeightedRandomizer randomizer;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        PopulateMetaList();

        List<int> weights = new List<int>();
        ObstacleMetaList.ForEach(x => weights.Add(x.Weight));

        randomizer = new WeightedRandomizer(weights);
    }

    void PopulateMetaList()
    {
        ObstacleMetaList = new List<ObstacleMetaData>() {
            new ObstacleMetaData(7.5f, 1, ObjectPool.Instance.Acquire<Boulder>),
            new ObstacleMetaData(5, 2, ObjectPool.Instance.Acquire<Target>)
        };
    }

    public void SpawnObstacles()
    {
        if (budget <= 0)
        {
            budget = RaidMapGen.Instance.Length * 2;
            budgetUnitLength = RaidMapGen.Instance.Length / budget;
        }

        remainingBudget = budget;
        
        while (remainingBudget > 0)
        {
            ObstacleMetaData data = ObstacleMetaList[randomizer.GetRandomIndex()];
            if (data.Cost <= remainingBudget)
            {
                Obstacle obstacle = data.SpawnObstacle();
                remainingBudget -= data.Cost;

                float xPos = Random.Range(-RaidMapGen.Instance.PlayableAreaWidth * 0.5f, RaidMapGen.Instance.PlayableAreaWidth * 0.5f),
                      yPos = budgetUnitLength * (budget - remainingBudget) - 1;

                obstacle.transform.position = new Vector3(xPos, yPos, 0);
                obstacle.gameObject.SetActive(true);

            }
            else
            {
                finalAttempts++;

                if (finalAttempts >= maxFinalAttemps)
                {
                    break;
                }
            }
        }
        
    }
}