using UnityEngine;
using System.Collections;

public class ControllerConfiguration : MonoBehaviour {

	private Animation _animation;
	private GameController _gameController;
	private Transform _currentSelectionTranform;
	private Transform _profileOneTransform;
	private Transform _profileTwoTransform;
	private Transform _customTransform;
	void OnEnable()
	{
		_animation = GetComponent<Animation>();
		_animation.Play(_animation.clip.name);
	}

	void Start ()
	{
		_gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		_currentSelectionTranform =  transform.FindChild("ProfileContainer").transform.FindChild("CurrentProfileSelection").transform;
		_profileOneTransform = transform.FindChild("ProfileContainer").transform.FindChild("cs_transform_1").transform;
		_profileTwoTransform = transform.FindChild("ProfileContainer").transform.FindChild("cs_transform_2").transform;
		_customTransform = transform.FindChild("ProfileContainer").transform.FindChild("cs_transform_3").transform;


	}

	// Update is called once per frame
	void Update ()
	{

		if (Input.GetKeyDown(KeyCode.Escape) )
		{
			if (_gameController.menuActive == MenuActive.CONTROL)
			{
				//_animation[_animation.clip.name].time = _animation[_animation.clip.name].length;
				//_animation[_animation.clip.name].speed = -1;
				//_animation.Play(_animation.clip.name);
			}
		}

		UpdateCurrentSelection();

	}


	void UpdateCurrentSelection()
	{
		_currentSelectionTranform.position = Vector3.Lerp(_currentSelectionTranform.position, ReturnTransformPosition(), Time.deltaTime * 10.0f);
	}

	private Vector3 ReturnTransformPosition()
	{
		return (_gameController.controllerProfile == ControllerProfile.WASD) ? _profileOneTransform.position : (_gameController.controllerProfile == ControllerProfile.TGFH) ? _profileTwoTransform.position : _customTransform.position;
	}


	void OnDisable()
	{
		_animation[_animation.clip.name].time = 0;
		_animation[_animation.clip.name].speed = 1;
	}
}
