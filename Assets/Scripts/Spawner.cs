using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public Transform[] SpawnPoints01;
	public Transform[] SpawnPoints02;
	public GameObject EnemyPrefab;
	public float DelayTimePeriod = 1f;
	float TimePeriodCurrent = 5f;
	bool isOne = true;

	void Update ()
	{
		if(Time.time >= TimePeriodCurrent)
		{
			SpawnBlocks();
			TimePeriodCurrent += Time.time + DelayTimePeriod;
			DelayTimePeriod *= .99f;
		}
	}

	void SpawnBlocks ()
	{
		if(isOne)
		{
			int randomIndex = Random.Range(0, SpawnPoints01.Length);

			for (int i = 0; i < SpawnPoints01.Length; i++) 
			{
				if(randomIndex != i)
				{
					Instantiate(EnemyPrefab, SpawnPoints01[i].position, Quaternion.identity);
				}
			}

			isOne = false;
		}

		else if(!isOne)
		{
			int randomIndex = Random.Range(0, SpawnPoints02.Length);

			for (int i = 0; i < SpawnPoints02.Length; i++) 
			{
				if(randomIndex != i)
				{
					Instantiate(EnemyPrefab, SpawnPoints02[i].position, Quaternion.identity);
				}
			}

			isOne = true;
		}
	}
}
