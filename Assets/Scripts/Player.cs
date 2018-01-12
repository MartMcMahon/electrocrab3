using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	public float moveSpeed = 1;
	public static int chargedReq = 5;
    public int ChargedReq { set; get; }
    public static int startingLives = 3;
    private static int lives;
    public static int Lives { get { return lives; }
    set
        {
            lives = value;
            livesText.text = "lives: " + lives;
        } }

	private static int foods;
	public static int Foods { get{return foods;} 
		set {
			foods = value;
			foodsText.text = "food: " + foods;
            Debug.Log("set foods");
            GameManager.setChargedText(foods >= chargedReq);
            Player.isCharged = foods >= chargedReq;
        } }

	public static bool isCharged;

    public static bool isAlive;

    Rigidbody2D rb;
	FoodManager fm;
    GameManager gm;

    public ParticleSystem pc;

    static Text livesText;
    static Text foodsText;
    static Text chargedTextGobj;

	LayerMask enemyLayer;

	void Awake()
	{
		pc = gameObject.transform.GetChild(0).GetComponent<ParticleSystem>();

        livesText = GameObject.Find("LivesText").GetComponent<Text>();
        foodsText = GameObject.Find("FoodsText").GetComponent<Text>();
    }

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		fm = FindObjectOfType<FoodManager>();
		gm = FindObjectOfType<GameManager>();        

		enemyLayer = LayerMask.NameToLayer("Enemies");

		isCharged = false;
		isAlive = true;
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.layer == enemyLayer && (isAlive))
		{
            isAlive = false;
			StartCoroutine(death());
			//death();
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		fm.returnFood(other.GetComponent<Food>());
        eatFood();
		//Destroy(other.gameObject);
	}

	public void move(float x, float y)
	{
        Vector2 movePos = new Vector2(x + transform.position.x, y + transform.position.y);
        //rb.MovePosition(Vector2.Lerp(transform.position, new Vector2(x, y), moveSpeed * Time.deltaTime));
        rb.MovePosition(Vector2.Lerp(transform.position, movePos, moveSpeed * Time.deltaTime));
    }
	void eatFood()
	{
		Player.Foods += 1;
        Debug.Log("eat");

        /*
		if (foods >= chargedReq)
		{
			isCharged = true;
		}
        */
	}

	IEnumerator death()
	{
		gm.sendStopMessage();

		yield return new WaitForSeconds(1);
		if (lives == 0)
		{
			Destroy(gameObject);
			gm.gameOver();
		}
		else
		{
			Lives -= 1;
            Debug.Log("death foods: " + foods);
            Debug.Log("death isCharged: " + Player.isCharged);
            gm.newLife(foods);
            Destroy(gameObject);
        }
	}
}
