using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
	public FoodManager fm;
	public float moveSpeed = 1;

    SpriteRenderer spr;

	void Awake()
	{
		fm = FindObjectOfType<FoodManager>();
        spr = GetComponent<SpriteRenderer>();
	}

    void Start()
    {
        changeSprite();
    }

    public void changeSprite()
    {
        spr.sprite = fm.sprites[Random.Range(0, fm.sprites.Length)];
    }

	public IEnumerator moveDown()
	{
		//Debug.Log("starting first food corot");
		//1Debug.Log("transform.position.y: " + transform.position.y);
		while (transform.position.y > -6)
		{
			//Debug.Log("y pos > -6");
			transform.Translate(0, -moveSpeed, 0);
			yield return null;
		}

		fm.returnFood(this);
		//yield return this;
	}
}
