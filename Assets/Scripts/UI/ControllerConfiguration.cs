using UnityEngine;
using System.Collections;

public class ControllerConfiguration : MonoBehaviour {

	Animation _animation;
	GameController _gameController;

	void OnEnable()
	{
		_animation = GetComponent<Animation>();
		_animation.Play(_animation.clip.name);
	}

	void Start ()
	{
		_gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}

	// Update is called once per frame
	void Update ()
	{

		if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
		{
			if (_gameController.menuActive == MenuActive.CONTROL)
			{
				_animation[_animation.clip.name].time = _animation[_animation.clip.name].length;
				_animation[_animation.clip.name].speed = -1;
				_animation.Play(_animation.clip.name);
			}
		}
	}

	void OnDisable()
	{
		_animation[_animation.clip.name].time = 0;
		_animation[_animation.clip.name].speed = 1;
	}
}
