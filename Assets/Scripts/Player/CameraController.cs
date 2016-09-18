//Devan Patel
//Applications and Scripting
//Sep.12.2016
using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	#region---PUBLIC MEMBERS----
	public float CameraMoveSpeed;
	public float CameraLookSpeed;
	public float CameraLookAngle;
	public float bobAmplitude = 0.2f;
	public float bobFrequency = 0.8f;
	#endregion---/ PUBLIC MEMBERS---

	#region---PRIVATE MEMBERS----
	private float _lookInput = 0;
	private float _headBoxX;
	private float _headBobY;
	private float _lookHorizontalInput;
	private float _lookVerticalInput;
	private float _strafeInput;
	private float _forwardInput;
	private float _recoil;
	private float _recoilVel;
	private bool _isMoving;
	private Vector3 _headBobPos = Vector3.zero;
	private Vector3 _targetHeadBob = Vector3.zero;
	private CharacterController _cc;
	private Rigidbody _rb;
	private Vector3 _velocity;
	private GameObject _child;
	private GameObject _bulletLeft;
	private GameObject _bulletRight;
	private GameObject _dummyLeft;
	private GameObject _dummyRight;
	private GameObject[] _weaponBarrels;
	private GameController _gameSceneManager;


	#endregion---/PRIVATE MEMBERS---

	void Start ()
	{
		Init();
		CameraMoveSpeed = Constants.PlayerMovementSpeed;

	}
	/// <summary>
	///	Init all the components.
	/// </summary>
	private void Init()
	{
		_rb = GetComponent<Rigidbody>();
		_child = transform.FindChild("Main Camera").transform.gameObject;
		_cc = GetComponent<CharacterController>();
		_gameSceneManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		_dummyLeft = _child.transform.FindChild("DummyLeft").gameObject;
		_dummyRight = _child.transform.FindChild("DummyRight").gameObject;
		GameObject[] _weaponBarrels_local = { transform.FindChild("barrel (1)").gameObject, transform.FindChild("barrel").gameObject };
		_weaponBarrels = _weaponBarrels_local;

	}

	// Update is called once per frame
	void Update ()
	{
		if (_gameSceneManager.menuActive == MenuActive.GAME)
		{
			RegisterInput(_gameSceneManager.controllerProfile);
			if (_gameSceneManager.TogglePlayerMovement)
			{
				Move();
				Look();
				HeadBob();
				_recoil = Mathf.SmoothDamp(_recoil, 0, ref _recoilVel, .1f);
			}
		}

	}
	/// <summary>
	/// Moves the Players
	/// </summary>
	private void Move()
	{
		float strafe = _strafeInput * CameraMoveSpeed ;
		float forward = _forwardInput * CameraMoveSpeed ;
		Vector3 _movement = new Vector3(strafe, -5.0f, forward);
		_movement = transform.rotation * _movement;
		if (Mathf.Abs(forward) > 0)
		{
			_velocity.z = forward;
		} else {
			_velocity.z = 0;
		}
		if (Mathf.Abs(strafe) > 0)
		{
			_velocity.x = strafe;
		} else {
			_velocity.x = 0;
		}

		_isMoving = strafe != 0 || forward != 0;
		_cc.Move(_movement * Time.deltaTime);


	}
	/// <summary>
	/// Registers the custom input based on the controller profile
	/// </summary>
	/// <param name="_cf"></param>
	private void RegisterInput(ControllerProfile _cf)
	{

		if (_cf == ControllerProfile.WASD)
		{
			_forwardInput = (Input.GetKey(KeyCode.W)) ? _forwardInput = 1 : (Input.GetKey(KeyCode.S)) ? _forwardInput = -1 : _forwardInput = 0;
			_strafeInput = (Input.GetKey(KeyCode.D)) ? _strafeInput = 1 : (Input.GetKey(KeyCode.A)) ? _strafeInput = -1 : _strafeInput = 0;
			_lookHorizontalInput = Mathf.Clamp(Input.GetAxis("Mouse X"), -1, 1);
			_lookVerticalInput = Input.GetAxis("Mouse Y");
		} else if (_cf == ControllerProfile.TGFH)
		{
			_forwardInput = (Input.GetKey(KeyCode.T)) ? _forwardInput = 1 : (Input.GetKey(KeyCode.G)) ? _forwardInput = -1 : _forwardInput = 0;
			_strafeInput = (Input.GetKey(KeyCode.H)) ? _strafeInput = 1 : (Input.GetKey(KeyCode.F)) ? _strafeInput = -1 : _strafeInput = 0;
			_lookHorizontalInput = (Input.GetKey(KeyCode.RightArrow)) ? _lookHorizontalInput = 1 : (Input.GetKey(KeyCode.LeftArrow)) ? _lookHorizontalInput = -1 : _lookHorizontalInput = 0;
			_lookVerticalInput = (Input.GetKey(KeyCode.UpArrow)) ? _lookVerticalInput = 1 : (Input.GetKey(KeyCode.DownArrow)) ? _lookVerticalInput = -1 : _lookVerticalInput = 0;
		} else if (_cf == ControllerProfile.CUSTOM)
		{
			KeyCode[] customInput = _gameSceneManager.customKey;
			_forwardInput = (Input.GetKey(customInput[1])) ? _forwardInput = 1 : (Input.GetKey(customInput[3])) ? _forwardInput = -1 : _forwardInput = 0;
			_strafeInput = (Input.GetKey(customInput[2])) ? _strafeInput = 1 : (Input.GetKey(customInput[0])) ? _strafeInput = -1 : _strafeInput = 0;
			if (_gameSceneManager.ToggleMouseControl == false)
			{
				_lookHorizontalInput = (Input.GetKey(customInput[6])) ? _lookHorizontalInput = 1 : (Input.GetKey(customInput[4])) ? _lookHorizontalInput = -1 : _lookHorizontalInput = 0;
				_lookVerticalInput = (Input.GetKey(customInput[5])) ? _lookVerticalInput = 1 : (Input.GetKey(customInput[7])) ? _lookVerticalInput = -1 : _lookVerticalInput = 0;
			} else
			{
				_lookHorizontalInput = Mathf.Clamp(Input.GetAxis("Mouse X"), -1, 1);
				_lookVerticalInput = Input.GetAxis("Mouse Y");
			}
		}
	}

	/// <summary>
	///	Manages the rotation for the player
	/// </summary>
	private void Look()
	{
		if (_gameSceneManager.onContainer == false)
		{
			transform.Rotate(0, _lookHorizontalInput * CameraLookSpeed, 0);
			_lookInput -= _lookVerticalInput * CameraLookSpeed;
			_lookInput = Mathf.Clamp(_lookInput , -CameraLookAngle, CameraLookAngle);
			_child.transform.localRotation = Quaternion.Euler(_lookInput, 0, 0);
		}
	}

	/// <summary>
	///	Bobs the camera
	/// </summary>
	private void HeadBob()
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
			_child.transform.position = (transform.position + Vector3.up) + transform.TransformDirection(_headBobPos);
		} else
		{
			_targetHeadBob = Vector3.zero;
		}
		AdjustBarrels(_headBobPos);
	}
	/// <summary>
	/// Adjusts the barrels or bobs the gun barrels
	/// </summary>
	/// <param name="_headBobPos"></param>
	private void AdjustBarrels(Vector3 _headBobPos)
	{
		_weaponBarrels[0].transform.position = (_dummyLeft.transform.position + _dummyLeft.transform.forward / 2 + (transform.forward * (_recoil / 20.0f))) + transform.TransformDirection(_headBobPos) ;
		_weaponBarrels[0].transform.rotation = _dummyLeft.transform.rotation * Quaternion.Euler(new Vector3(_recoil, 0, 0));
		_weaponBarrels[1].transform.position = (_dummyRight.transform.position + _dummyRight.transform.forward / 2 + (transform.forward * (_recoil / 20.0f))) + transform.TransformDirection(_headBobPos);
		_weaponBarrels[1].transform.rotation = _dummyRight.transform.rotation * Quaternion.Euler(new Vector3(_recoil, 0, 0));

	}
	/// <summary>
	/// Sets recoil to certain amount
	/// </summary>
	public void Recoil()
	{
		_recoil = -10;
	}
}
