//Devan Patel
//Applications and Scripting
//Sep.12.2016
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GameOverText : MonoBehaviour {

	public float writeSpeed;

	#region---- PRIVATE MEMBERS----
	private Text _text;
	private bool _start;
	private bool _execute;
	private GameController _gameController;
	private int _textIndex;
	private string _message;
	#endregion---- /PRIVATE MEMBERS----
	void Start ()
	{
		_message = "Game Over...";
		_text = GetComponent<Text>();
		_gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}

	void Update ()
	{
		_start = _gameController.menuActive == MenuActive.RETRY;

		if (_start)
		{
			if (_execute == false)
			{

				InvokeRepeating("WriteText", 1.0f, writeSpeed);
				_execute = true;
			}
		}

		if (_textIndex > _message.Length - 1)
		{
			_text.text = "";
			_textIndex = 0;
		}
	}

	/// <summary>
	/// Writes the text
	/// </summary>
	private void WriteText()
	{
		_textIndex++;
		_text.text = _message.Substring(0, _textIndex);
	}


}
