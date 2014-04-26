using UnityEngine;
using System.Collections;

public class ChoiceButtonBehavior : MonoBehaviour 
{
	public string TargetScene;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnMouseDown()
	{
		Application.LoadLevel(TargetScene);
	}
}
