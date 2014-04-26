using UnityEngine;
using System.Collections;

public class MasterSceneManager : MonoBehaviour 
{
	//all backgrounds
	public GameObject[] Backgrounds;
	//The background I am currently looking at
	public GameObject CurrentBackground;
	//The background I was looking at
	public GameObject PreviousBackground;
	//The background I will view next
	public GameObject NextBackground;
	//The camera in the scene
	private GameObject SceneCamera;
	//all of the text boxes
	public GameObject[] textBoxes;
	public GameObject CurrentTextBox;
	public GameObject PreviousTextBox;
	public GameObject NextTextBox;

	// Use this for initialization
	void Start () 
	{
		Backgrounds = GameObject.FindGameObjectsWithTag("Background");
		SceneCamera = GameObject.FindGameObjectWithTag("MainCamera");
		textBoxes = GameObject.FindGameObjectsWithTag("TextBox");

		InnitializeFirstBackground();
	}

	void InnitializeFirstBackground()
	{
		PreviousBackground = null;
		CurrentBackground = Backgrounds[0];
		foreach(GameObject loopObjs in Backgrounds)
		{
			if(loopObjs.transform.position.x < CurrentBackground.transform.position.x)
			{
				CurrentBackground = loopObjs;
			}
		}
		SceneCamera.transform.position = CurrentBackground.transform.position + Vector3.back * 100;
		SceneCamera.camera.orthographicSize = (CurrentBackground.GetComponent<SpriteRenderer>().bounds.max.y -  CurrentBackground.GetComponent<SpriteRenderer>().bounds.min.y) / 2;

		PreviousTextBox = null;
		CurrentTextBox = textBoxes[0];
		foreach(GameObject loopObjs in textBoxes)
		{
			if(loopObjs.transform.position.x < CurrentTextBox.transform.position.x)
			{
				CurrentTextBox = loopObjs;
			}
		}
		CurrentTextBox.GetComponent<TextManager>().CurrentTextBox = true;
		LocateNextBackground();
	}
	
	// Update is called once per frame
	void Update () 
	{

		if(Input.GetKeyDown(KeyCode.Mouse0) && 
		  CurrentTextBox.GetComponent<TextManager>().AllBlurbsCompleted)
		{
			ChangeScenes();
		}
	}

	void ChangeScenes()
	{
		//set the scene order
		PreviousBackground = CurrentBackground;
		CurrentBackground = NextBackground;
		PreviousTextBox = CurrentTextBox;
		CurrentTextBox = NextTextBox;
		CurrentTextBox.GetComponent<TextManager>().CurrentTextBox = true;
		PreviousTextBox.GetComponent<TextManager>().CurrentTextBox = false;
		LocateNextBackground();

		//move the camera
		SceneCamera.transform.position = CurrentBackground.transform.position + Vector3.back;

		//fit the cameras size to that of the background - fit to sideways screen
		SceneCamera.camera.orthographicSize = (CurrentBackground.GetComponent<SpriteRenderer>().bounds.max.y -  CurrentBackground.GetComponent<SpriteRenderer>().bounds.min.y) / 2;
	}

	void LocateNextBackground()
	{
		//fix this, this is NOT correctly selecting the next background or textbox



		//find the next background to switch to
		NextBackground = Backgrounds[0];
		foreach(GameObject objs in Backgrounds)
		{
			if(	NextBackground.transform.position.x <= CurrentBackground.transform.position.x ||
				(objs.transform.position.x > CurrentBackground.transform.position.x &&
			 	objs.transform.position.x < NextBackground.transform.position.x))
			{
				NextBackground = objs;
			}
		}

		//find the next text box as well
		NextTextBox = textBoxes[0];
		foreach(GameObject objs in textBoxes)
		{
			if( NextTextBox.transform.position.x <= CurrentTextBox.transform.position.x ||
			   (objs.transform.position.x > CurrentTextBox.transform.position.x &&
			 	objs.transform.position.x < NextTextBox.transform.position.x))
			{
				NextTextBox = objs;
			}
		}

	}
}
