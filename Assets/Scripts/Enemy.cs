using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public float moveSpeed = 1;
	public int EnemyType = 1;
	public float attackInterval;
	public Vector2 overtakeMag = new Vector2(1.5f, 1.5f);

	public Rigidbody2D rb;
	public Player player;

	public float attackTimer;
	public bool alive;
	IEnumerator attackCoroutine;

	void Awake () {
		rb = GetComponent<Rigidbody2D>();
		player = GameObject.FindWithTag("Player").GetComponent<Player>();
		attackTimer = 0;
		alive = true;
	}

	void Start()
	{
		
		//Debug.Log("player.transform.position: " + player.transform.position);
		attackCoroutine = movementCoroutine(player.transform.position);

	}

	public void beginAttacking()
	{
		attackInterval = Random.Range(2f, 8f);
		InvokeRepeating("attack", 0f, attackInterval);
	}

	public void kill()
	{
		attackTimer = 0;
		CancelInvoke();
		StopAllCoroutines();
		gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update()
	{
		attackTimer++;
	}

	void FixedUpdate()
	{
		//Ray2D ray = new Ray2D();
		//Physics2D.Raycast(transform.position, Player.;
	}

	public void move()
	{
        
	}

	public void attack()
	{
		StopCoroutine(attackCoroutine);
		//Debug.Log("attack stopped");
		attackCoroutine = movementCoroutine(player.transform.position);
		StartCoroutine(attackCoroutine);
		//Debug.Log("attack started");
	}

	IEnumerator movementCoroutine(Vector2 target)
	{
		rb.velocity = Vector3.zero;
		//Debug.Log("player.transform.position: " + player.transform.position);
		while (Vector2.Distance(transform.position, target) > .01) 
		{
			rb.velocity = new Vector3((target.x - transform.position.x) * moveSpeed, (target.y - transform.position.y) * moveSpeed, 0);;
			//transform.position = Vector2.Lerp(transform.position, target, moveSpeed * Time.deltaTime);	
			yield return null;
		}
	}
}
