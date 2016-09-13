//Devan Patel
//Applications and Scripting
//Sep.12.2016
using UnityEngine;
using System.Collections;

public class ControllerConfiguration : MonoBehaviour {
	#region ---- PRIVATE MEMBERS------
	private Animation _animation;
	private GameController _gameController;
	private Transform _currentSelectionTranform;
	private Transform _profileOneTransform;
	private Transform _profileTwoTransform;
	private Transform _customTransform;
	#endregion ---- /PRIVATE MEMBERS------
	void OnEnable()
	{
		_animation = GetComponent<Animation>();
		if (_animation.clip != null)
		{
			_animation.Play(_animation.clip.name);
			Debug.Log("Playing...");
		}
	}

	void Start ()
	{
		_gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		_currentSelectionTranform =  transform.FindChild("ProfileContainer").transform.FindChild("CurrentProfileSelection").transform;
		_profileOneTransform = transform.FindChild("ProfileContainer").transform.FindChild("cs_transform_1").transform;
		_profileTwoTransform = transform.FindChild("ProfileContainer").transform.FindChild("cs_transform_2").transform;
		_customTransform = transform.FindChild("ProfileContainer").transform.FindChild("cs_transform_3").transform;


	}

	void Update ()
	{
		UpdateCurrentSelection();
	}

	/// <summary>
	/// Updates the current selection in the UI
	/// </summary>
	private void UpdateCurrentSelection()
	{
		_currentSelectionTranform.position = Vector3.Lerp(_currentSelectionTranform.position, ReturnTransformPosition(), Time.deltaTime * 10.0f);
	}

	/// <summary>
	/// Returns the transform direction for the selection
	/// </summary>
	private Vector3 ReturnTransformPosition()
	{
		return (_gameController.controllerProfile == ControllerProfile.WASD) ? _profileOneTransform.position : (_gameController.controllerProfile == ControllerProfile.TGFH) ? _profileTwoTransform.position : _customTransform.position;
	}


	void OnDisable()
	{
		if (_animation.clip != null)
		{
			_animation[_animation.clip.name].time = 0;
			_animation[_animation.clip.name].speed = 1;
		}
	}
}
