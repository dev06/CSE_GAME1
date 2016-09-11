using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GameOverText : MonoBehaviour {

	// Use this for initialization
	public float writeSpeed;
	private Text _text;
	private bool _start;
	private bool _execute;
	private GameController _gameController;
	private int _textIndex;
	private string _message;
	void Start ()
	{
		_message = "Game Over...";
		_text = GetComponent<Text>();
		_gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}

	// Update is called once per frame
	void Update ()
	{
		_start = _gameController.menuActive == MenuActive.RETRY;

		if (_start)
		{
			if (_execute == false)
			{

				InvokeRepeating("WriteText", 1.0f, writeSpeeda);
				_execute = true;
			}
		}

		if (_textIndex > _message.Length - 1)
		{
			_text.text = "";
			_textIndex = 0;
		}


	}

	void WriteText()
	{
		_textIndex++;
		_text.text = _message.Substring(0, _textIndex);
	}


}
