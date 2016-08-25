using UnityEngine;
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
	private bool _isMoving;
	private Vector3 _headBobPos = Vector3.zero;
	private Vector3 _targetHeadBob = Vector3.zero;
	#endregion---/PRIVATE MEMBERS---

	void Start () {
		_characterController = GetComponent<CharacterController>();
		_child = transform.FindChild("Main Camera").transform.gameObject;
	}

	// Update is called once per frame
	void Update () {
		Move();
		Look();
		HeadBob();
	}

	void Move()
	{
		float strafe = Input.GetAxis(Constants.STRAFE) * CameraMoveSpeed;
		float forward = Input.GetAxis(Constants.FORWARD) * CameraMoveSpeed;
		_isMoving = strafe != 0 || forward != 0;
		Vector3 movement = new Vector3(strafe, 0, forward);
		movement = transform.rotation * movement;
		_characterController.Move(movement * Time.deltaTime);
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
