using UnityEngine;
using System.Collections;

public class TextManager : MonoBehaviour 
{
	//the actual text displaying component
	public TextMesh myTextMesh;
	//the number of indivitual statements made in this text bot
	public int NumberOfStatements;
	//The array containing all text to be shown in the box
	public string[] Script;
	//the statement we are currently displaying, script array value
	private int CurrentBlurb;
	//The character count the type writter effect is currently up to
	private int BlurbTypeWritter;
	//a bool that signifies all statements have been completed
	public bool AllBlurbsCompleted;
	//a bool that signifies that the statement we are currently on has finished
	public bool CurrentBlurbCompleted;
	//the time frequency letters are added to the screen
	public float TypeWritterFrequency;
	//a timer that counts up to help display the type writter effect
	private float TypeWritterTimer;
	//a bool that indicates that the camera is currently viewing this textbox
	public bool CurrentTextBox = false;
	//Mark this as the final box
	public bool isFinalBox = false;

	// Use this for initialization
	void Start () 
	{
		myTextMesh.text = "";
		CurrentBlurb = 0;
		BlurbTypeWritter = 0;
		AllBlurbsCompleted = false;
		CurrentBlurbCompleted = false;
		TypeWritterTimer = 0;
		//CurrentTextBox = false; //do not innitialize this variable
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!CurrentTextBox || AllBlurbsCompleted)
			return;

		//skip to the end of the current blurb, or skip to the next
		if(Input.GetKeyDown(KeyCode.Mouse0) && BlurbTypeWritter > 0 && !isFinalBox)
		{
			if(CurrentBlurbCompleted == false && AllBlurbsCompleted == false)
			{
				BlurbTypeWritter = Script[CurrentBlurb].Length - 1;
				CurrentBlurbCompleted = true;
			}
			else if(CurrentBlurb < Script.Length - 1)
			{
				CurrentBlurb++;
				TypeWritterTimer = 0;
				BlurbTypeWritter = 0;
				CurrentBlurbCompleted = false;
			}
			else if(CurrentBlurb == Script.Length - 1 && CurrentBlurbCompleted == true)
			{
				AllBlurbsCompleted = true;
			}
		}

		//show the correct amount of text
		if(BlurbTypeWritter < Script[CurrentBlurb].Length)
			myTextMesh.text = Script[CurrentBlurb].Remove(BlurbTypeWritter);
		else
			myTextMesh.text = Script[CurrentBlurb];

		//count up the typewritter OR set the blurb as being completed
		TypeWritterTimer += Time.deltaTime;
		if(TypeWritterTimer > TypeWritterFrequency)
		{
			TypeWritterTimer -= TypeWritterFrequency;
			if(BlurbTypeWritter < Script[CurrentBlurb].Length)
				BlurbTypeWritter++;
			else
				CurrentBlurbCompleted = true;
		}
	}
}
