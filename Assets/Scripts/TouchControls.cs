using UnityEngine;
using UnityEngine.UI;

public class TouchControls : MonoBehaviour {

	public GameManager gm;
	public Player player;
	public GameObject joystickCenter;

    float cameraDistance = 1;

	void Awake() {
		//joystickCenter = GameObject.FindObjectOfType<Image>();
	}

	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        //GameObject.getc
        cameraDistance = Vector3.Distance(Camera.main.transform.position, Vector3.zero);
    }
	
	// Update is called once per frame
	void Update () {
		if (player != null && Player.isAlive) {
	        if (gm.inputType == 1)
			{
                //touch screen
                //Vector2 initialClickPos;

                Vector3 mousePos = Input.mousePosition;
                //Vector3 screenPos = Camera.main.ScreenToWorldPoint

				float startX = 0;
                float startY = 0;
				Vector2 vec;


				if (Input.GetMouseButtonDown(0)) {
					startX = Input.mousePosition.x;
					startY = Input.mousePosition.y;
					Debug.Log("startX: " + startX);
					Debug.Log("startY: " + startY);
				}
				else if (Input.GetMouseButton(0)) {
                    //worldVec is spot in the world where player is clicking
                    Vector2 worldVec = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraDistance));

                    //worldVec becomes direction player will move in
                    worldVec.x = worldVec.x - player.transform.position.x;
                    worldVec.y = worldVec.y - player.transform.position.y;

                    //limit vector to reflect keyboard controls
                    worldVec = Vector2.ClampMagnitude(worldVec, 1);

                    //move the player
                    player.move(worldVec.x, worldVec.y);

                    //optional include to show joystick position
                    //joystickCenter.transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y
                }

                if (Input.GetMouseButtonUp(1) && Player.isCharged)
                {

                    Debug.Log("ATTACK FUCK U GODDAMM");
                    gm.playerAttack();
                    Debug.Log("gm: " + gm);
                }
                

                // Mouse is for testing
                //, but turns out you don't need to change anything!!
                /*
                if (Input.touches[0].phase == TouchPhase.Began)
                {
                    Debug.Log("touched!");
                }

                /*
                foreach (Touch touch in Input.touches {
                    if (touch.phase == TouchPhase.Began) {
                                        //is in joystick range?
                                        startX = touch.rawPosition.x;
                                        startY = touch.rawPosition.y;
                                        Debug.Log("startX: " + startX);
                                        Debug.Log("startY: " + startY);
                                    }
                                    else if (touch.phase == TouchPhase.Moved)
                                    {
                                        vec = new Vector2(touch.rawPosition.x - startX, touch.rawPosition.y - startY);
                                        Debug.Log("x: " + vec.x);
                                        Debug.Log("y: " + vec.y);
                                    }
                                }
                                */
            }
			//end touchscreen controls
		}
	}

    public void respawn(Player newPlayer)
    {
        Debug.Log("respawn touch controls");
        /*
        while (player == null)
        {
            player = GameObject.FindWithTag("Player").GetComponent<Player>();
        }
    */
        player = newPlayer;
        
        Debug.Log("player obj: " + GameObject.FindWithTag("Player"));
        Debug.Log("player thing: " + GameObject.FindWithTag("Player").GetComponent<Player>());
        Debug.Log("player: " + player);
    }
}