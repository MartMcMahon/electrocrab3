using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

	public static GameManager gm;
	public int inputType;

    public static float gameStartTime = 0;

	public Player player;
	public ParticleSystem pc;

	public EnemyManager em;

    public Image deathScreen;
    public float deathFlashTime;
    Color deathColor = new Color(1f, 0f, 0f, 1f);
    bool flash = false;

    public float gameOverTextTime;
    public static bool isGameOver = false;
    public GameObject[] gameOverStuff;

    public static GameObject chargedText;

    void Awake()
	{
		if (gm == null)
		{
			gm = this;
		}
		else if (gm != this)
		{
			Destroy(gameObject);
		}

//input stuff
#if UNITY_ANDROID
		inputType = 1;
#else
        inputType = 2;
        Debug.Log("uh, hello??");
#endif
		player = Instantiate(player, Vector2.zero, Quaternion.identity);
		
        //Enemy Manager
		em = gameObject.GetComponent<EnemyManager>();

        //get image to flash when player is killed
        deathScreen = GameObject.FindGameObjectWithTag("Death").GetComponent<Image>();

        //get game over stuff
        //gameOverText = GameObject.FindGameObjectWithTag("GameOverText").GetComponent<Text>();

        //get permenane reference to chargedText
        chargedText = GameObject.Find("ChargedText");

        NewGame();
	}

	// Use this for initialization
	void Start () 
	{

	}

	public void sendStopMessage()
	{
		SendMessage("StopSystem");
	}

	void NewGame()
	{
		foreach (GameObject go in gameOverStuff) 
		{
			go.SetActive(false);
		}

		Player.isCharged = false;
		readyAttack();
        Player.Lives = Player.startingLives;
        Player.Foods = 0;

        gameStartTime = Time.time;
        isGameOver = false;
    }

	public void newLife(int oldFood)
	{
        SendMessage("killEmAll");
		player = Instantiate(player, Vector2.zero, Quaternion.identity);
        Player.Foods = oldFood;
        Debug.Log("newLife oldFood: " + oldFood);
        Debug.Log("newLife ischaged: " + Player.isCharged);
        SendMessage("respawn", player);
        Player.isAlive = true;
		
		//get Particle System ready for player attack
		readyAttack();

        Player.isCharged = Player.Foods >= Player.chargedReq;
        //Debug.Log("isCharged: " + Player.isCharged);
        //Debug.Log("Player.Foods >= Player.chargedReq: " + Player.Foods >= Player.chargedReq);
/*       
        if (Player.Foods >= 5)
        {
            Player.isCharged = true;
        }
        else
        {
            Player.isCharged = false;
        }
        */
	}

	void readyAttack() {
		pc = player.pc;
	}
	
	// Update is called once per frame
	void Update () {

        if (flash)
        {
            deathScreen.color = deathColor;
            flash = false;
        }

        if (deathScreen.color != Color.clear)
        {
            deathScreen.color = Color.Lerp(deathScreen.color, Color.clear, deathFlashTime * Time.deltaTime);
        }

        //get input
        if (Player.isAlive) {
/* moved to TouchControls script
	        if (inputType == 1)
			{
				//touch screen
				//Vector2 initialClickPos;

				float startX = 0;
				float startY = 0;
				Vector2 vec;

				foreach (Touch touch in Input.touches)
				{
					if (touch.phase == TouchPhase.Began)
					{
						//is in joystick range?

						startX = touch.rawPosition.x;
						startY = touch.rawPosition.y;
					}
					else if (touch.phase == TouchPhase.Moved)
					{
						vec = new Vector2(touch.rawPosition.x - startX, touch.rawPosition.y - startY);
						Debug.Log("x: " + vec.x);
						Debug.Log("y: " + vec.y);
					}
				}
			}
			*/
			//end touchscreen controls

			if (inputType == 2)
			{
				player.move(Input.GetAxis("Horizontal") + player.transform.position.x, Input.GetAxis("Vertical") + player.transform.position.y);

				if (Input.GetMouseButtonDown(0) && Player.isCharged)
				{
                    playerAttack();
				}
			}
		}
		//if isAlive
	}

    public void playerAttack()
    {
        //player attack
        Player.Foods -= 5;
        pc.Play();

        //killemall
        em.killEmAll();

        //check if still charged
        
        if (Player.Foods < Player.chargedReq)
        {
            Player.isCharged = false;
        }
    }

	public void gameOver() {
		isGameOver = true;
        Player.Foods = 0;
		gameOverStuff[0].GetComponent<Text>().text = (Time.time - gameStartTime).ToString();
		foreach (GameObject go in gameOverStuff) 
		{
			go.SetActive(true);
		}
	}

    public static void setChargedText(bool isFed)
    {
        chargedText.SetActive(isFed);
    }

    public static void QuitGame()
    {
        SceneManager.UnloadSceneAsync("gamescene");
    }
}