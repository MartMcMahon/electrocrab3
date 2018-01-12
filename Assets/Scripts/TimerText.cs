using UnityEngine;
using UnityEngine.UI;

public class TimerText : MonoBehaviour
{
	public Text t;
	float updateInterval = 1f;
	float timeToUpdate = 0;

	void Awake()
	{
		t = GetComponent<Text>();
	}

	// Update is called once per frame
	void Update()
	{
		if (Time.deltaTime > timeToUpdate)
		{
			t.text = (Time.time - GameManager.gameStartTime).ToString("##");
			timeToUpdate = updateInterval;
		}
		else
		{
			timeToUpdate -= Time.deltaTime;
		}
	}
}
