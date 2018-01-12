using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
	public Enemy shark;
	public int startingEnemyCount = 1;
	public int maxEnemies = 20;

	public float spawnInterval = 10f;
	public float spawnDelay = 4f;

	static List<Enemy> sharkPool;
	static List<Enemy> inUse;
	Player player;

	static EnemyManager instance;

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			Destroy(this.gameObject);
		}
	}

	// Use this for initialization
	void Start()
	{
		sharkPool = new List<Enemy>();
		inUse = new List<Enemy>();
		for (int c = 0; c<maxEnemies; c++)
		{
			shark = Instantiate(shark, Vector3.zero, Quaternion.identity);
			shark.gameObject.SetActive(false);
			sharkPool.Add(shark);
		}
		//player = GameObject.FindWithTag("Player").GetComponent<Player>();
		InvokeRepeating("spawnShark", 0f, spawnInterval);
	}

	public void spawnShark()
	{
		shark = getShark();
		//StartCoroutine(food.moveDown());
	}

	public Enemy getShark()
	{
		shark = sharkPool[0];
		shark.gameObject.SetActive(true);
		sharkPool.RemoveAt(0);
		inUse.Add(shark);
		shark.transform.position = new Vector2(Random.Range(-10, 10), 8);
		shark.beginAttacking();
		return shark;
	}

	public static void returnEnemy(Enemy s)
	{
		s.kill();
		//s.gameObject.SetActive(false);
		s.transform.position = Vector2.zero;
		inUse.Remove(s);
		sharkPool.Add(s);
	}

	public void killEmAll()
	{
		CancelInvoke();
		foreach (Enemy s in inUse)
		{
			s.kill();
			sharkPool.Add(s);
		}
		inUse.Clear();
		/*
		foreach (Enemy s in inUse)
		{
			
			returnEnemy(s);
		}
		*/
		//while (inUse.c
		InvokeRepeating("spawnShark", 4f, spawnInterval);
	}

	public void StopSystem()
	{
		Debug.Log("stop enemy system!");
		CancelInvoke();
        StopAllCoroutines();

        foreach (Enemy s in inUse)
		{
			s.StopAllCoroutines();
			s.CancelInvoke();
            s.kill();
            sharkPool.Add(s);
		}        inUse.Clear();	}
}