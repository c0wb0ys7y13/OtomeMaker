using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(TextManager)), CanEditMultipleObjects]
public class TextManagerEditor : Editor 
{
	public TextManager thisScript;
	public SerializedProperty isFinalBoxProp;
	public SerializedProperty TypeWritterFrequencyProp;
	public SerializedProperty NumberOfStatementsProp;
	public SerializedProperty[] ScriptProp;

	void OnEnable()
	{
		thisScript = (TextManager) target;

		isFinalBoxProp = serializedObject.FindProperty("isFinalBox");
		TypeWritterFrequencyProp = serializedObject.FindProperty("TypeWritterFrequency");
		NumberOfStatementsProp = serializedObject.FindProperty("NumberOfStatements");
		ScriptProp = new SerializedProperty[thisScript.Script.Length];
		for(int i = 0; i < thisScript.Script.Length; i++)
		{
			ScriptProp[i] = serializedObject.FindProperty(string.Format("{0}.Array.data[{1}]", "Script", i));
		}
	}

	void Update()
	{

	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		isFinalBoxProp.boolValue = EditorGUILayout.Toggle("Is Final Box Prop: ", isFinalBoxProp.boolValue);

		TypeWritterFrequencyProp.floatValue = EditorGUILayout.FloatField("Typewritter Frequency:",TypeWritterFrequencyProp.floatValue, GUILayout.MaxHeight(18));
		NumberOfStatementsProp.intValue = EditorGUILayout.IntField("Number of Blurbs:",NumberOfStatementsProp.intValue, GUILayout.MaxHeight(18));
		//set minimum values
		if(NumberOfStatementsProp.intValue < 0) 
			NumberOfStatementsProp.intValue = 0;
		serializedObject.ApplyModifiedProperties();

		if(NumberOfStatementsProp.intValue != ScriptProp.Length)
		{
			string[] tempStringArray = thisScript.Script;
			thisScript.Script = new string[thisScript.NumberOfStatements];
			for(int i = 0; i < thisScript.Script.Length && i < tempStringArray.Length; i++)
				thisScript.Script[i] = tempStringArray[i];
			ScriptProp = new SerializedProperty[thisScript.Script.Length];
			for(int i = 0; i < thisScript.Script.Length; i++)
				ScriptProp[i] = serializedObject.FindProperty(string.Format("{0}.Array.data[{1}]", "Script", i));
		}
		serializedObject.ApplyModifiedProperties();

		for(int i = 0; i < thisScript.Script.Length; i++)
		{
			ScriptProp[i] = serializedObject.FindProperty(string.Format("{0}.Array.data[{1}]", "Script", i));
			ScriptProp[i].stringValue = EditorGUILayout.TextArea(ScriptProp[i].stringValue, GUILayout.MaxHeight(45), GUILayout.MaxWidth(305));
		}
		serializedObject.ApplyModifiedProperties();
		serializedObject.Update();
	}
}
