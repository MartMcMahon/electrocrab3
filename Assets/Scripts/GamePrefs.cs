using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePrefs : MonoBehaviour {

	public int controls = 2;
	public Text ButtText;

	void Awake()
	{
		ButtText = GetComponent<Text>();
		ButtText.text = "Controls: " + controls;
	}

	public void onControlsClick()
	{
		switchControls();	}

	public void switchControls()
	{
		if (controls == 1)
		{
			controls = 2;
			ButtText.text = "Controls: keybaord";
		}
		else
		{
			controls = 1;
			ButtText.text = "Controls: touch";
		}
	}
}
