﻿using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	// Use this for initialization
	#region---PUBLIC MEMBERS----
	public float CameraMoveSpeed;
	public float CameraLookSpeed;
	public float CameraLookAngle;
	public float bobAmplitude = 0.2f;
	public float bobFrequency = 0.8f;
	#endregion---/ PUBLIC MEMBERS---

	#region---PRIVATE MEMBERS----
	private CharacterController _characterController;
	private GameObject _child;
	private float _lookInput = 0;
	private float _headBoxX;
	private float _headBobY;
	private float _strafeInput;
	private float _forwardInput;
	private bool _isMoving;
	private GameController _gameSceneManager;
	private Vector3 _headBobPos = Vector3.zero;
	private Vector3 _targetHeadBob = Vector3.zero;

	#endregion---/PRIVATE MEMBERS---

	void Start ()
	{
		Init();
	}

	void Init()
	{
		_characterController = GetComponent<CharacterController>();
		_child = transform.FindChild("Main Camera").transform.gameObject;
		_gameSceneManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}

	// Update is called once per frame
	void Update ()
	{
		RegisterInput(_gameSceneManager.controllerProfile);
		Move();
		Look();
		HeadBob();
	}

	void Move()
	{
		float strafe = _strafeInput * CameraMoveSpeed;
		float forward = _forwardInput * CameraMoveSpeed;
		_isMoving = strafe != 0 || forward != 0;
		Vector3 movement = new Vector3(strafe, 0, forward);
		movement = transform.rotation * movement;
		_characterController.Move(movement * Time.deltaTime);
	}

	void RegisterInput(ControllerProfile _cf)
	{
		if (_cf == ControllerProfile.WASD)
		{
			_forwardInput = (Input.GetKey(KeyCode.W)) ? _forwardInput = 1 : (Input.GetKey(KeyCode.S)) ? _forwardInput = -1 : _forwardInput = 0;
			_strafeInput = (Input.GetKey(KeyCode.D)) ? _strafeInput = 1 : (Input.GetKey(KeyCode.A)) ? _strafeInput = -1 : _strafeInput = 0;
		} else if (_cf == ControllerProfile.TGFH)
		{
			_forwardInput = (Input.GetKey(KeyCode.T)) ? _forwardInput = 1 : (Input.GetKey(KeyCode.G)) ? _forwardInput = -1 : _forwardInput = 0;
			_strafeInput = (Input.GetKey(KeyCode.H)) ? _strafeInput = 1 : (Input.GetKey(KeyCode.F)) ? _strafeInput = -1 : _strafeInput = 0;
		}
	}

	void Look()
	{
		transform.Rotate(0, Input.GetAxis(Constants.X_LOOK) * CameraLookSpeed, 0);
		_lookInput -= Input.GetAxis(Constants.Y_LOOK) * CameraLookSpeed;
		_lookInput = Mathf.Clamp(_lookInput , -CameraLookAngle, CameraLookAngle);
		_child.transform.localRotation = Quaternion.Euler(_lookInput, 0, 0);
	}

	void HeadBob()
	{
		if (_isMoving)
		{
			_targetHeadBob.x = Mathf.PingPong (bobFrequency * Time.time, bobAmplitude);
			_targetHeadBob.y = Mathf.PingPong (bobFrequency * Time.time, bobAmplitude / 2f);
			_targetHeadBob.x -= bobAmplitude / 2f;
			_targetHeadBob.y -= bobAmplitude / 4f;
			_headBobPos.x = Mathf.Lerp (_headBobPos.x, _targetHeadBob.x, 2.5f * Time.deltaTime);
			_headBobPos.y = Mathf.Lerp (_headBobPos.y, _targetHeadBob.y, 2.5f * Time.deltaTime);
			_headBobPos.z = 0;
			_child.transform.position = transform.position  + transform.TransformDirection(_headBobPos);
		} else
		{
			_targetHeadBob = Vector3.zero;
		}
	}
}
