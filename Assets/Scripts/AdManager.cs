using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour {

	public string AdUnitId = "";

	private AdRequest request;
	private InterstitialAd interstitial;

	// Use this for initialization
	void Start () {
		// Initialize an InterstitialAd.
		interstitial = new InterstitialAd(AdUnitId);
		// Create an empty ad request.
		request = new AdRequest.Builder().Build();
		// Load the interstitial with the request.
		Debug.Log("load ad");
		interstitial.LoadAd(request);
	}

	public void showAd()
	{
		Debug.Log("show ad");
		interstitial.Show();
	}
}
