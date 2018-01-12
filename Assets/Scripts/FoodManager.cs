using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
	public Food food;
	public int startingFoodCount = 6;
	public float spawnInterval = 4f;
	public float spawnDelay = 4f;

    public Sprite[] sprites;

	static List<Food> foodPool;
	static List<Food> inUse;
	Player player;

	static FoodManager instance;

	void Awake()
	{
		foodPool = new List<Food>();
		inUse = new List<Food>();
		for (int c = 0; c < startingFoodCount; c++)
		{
			food = Instantiate(food, Vector3.zero, Quaternion.identity);
			food.gameObject.SetActive(false);
			foodPool.Add(food);
		}
	}

	// Use this for initialization
	void Start()
	{
		player = GameObject.FindWithTag("Player").GetComponent<Player>();
		InvokeRepeating("spawnFood", 0f, spawnInterval);
	}

	public void spawnFood()
	{
		food = getFood();
        StartCoroutine(food.moveDown());
	}

	public Food getFood()
	{
		food = foodPool[0];
		food.gameObject.SetActive(true);
		foodPool.RemoveAt(0);
		inUse.Add(food);
		food.transform.position = new Vector2(Random.Range(-10, 10), 8);
        food.changeSprite();
		return food;
	}

	public void returnFood(Food f)
	{
		f.gameObject.SetActive(false);
		//f.transform.position = Vector2.zero;
		inUse.Remove(f);
		foodPool.Add(f);
	}

	public void StopSystem()
	{
		Debug.Log("stop food system!");
		CancelInvoke();
		StopAllCoroutines();

        foreach (Food f in inUse)
        {
            f.StopAllCoroutines();
            f.CancelInvoke();
            f.gameObject.SetActive(false);
            foodPool.Add(f);
        }
        inUse.Clear();
    }

	public void respawn()
	{
        Debug.Log("respawn foodmanager");
        InvokeRepeating("spawnFood", spawnDelay, spawnInterval);
	}
}