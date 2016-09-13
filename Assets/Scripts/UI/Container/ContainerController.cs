//Devan Patel
//Applications and Scripting
//Sep.12.2016
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class ContainerController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	#region-----PROTECTED MEMBERS-----
	protected GameController _gameController;
	protected static float _hideTimerCounter;
	protected static bool _showUI;
	protected bool _childrenActive;
	#endregion-----/PROTECTED MEMBERS-----

	#region-----PRIVATE MEMBERS-----
	private Animation _animation;
	private bool _initAnim;
	private static bool _canPlayAnim;
	private static GameObject[] _container;
	#endregion-----/PRIVATE MEMBERS-----
	void Start ()
	{
		Init();
	}

	public void Init()
	{
		_gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
		_container = GameObject.FindGameObjectsWithTag("ContainerControl/Container");
		_animation = GetComponent<Animation>();
		SetChildrenActive(true);
	}

	// Update is called once per frame
	void Update ()
	{

		if (_showUI == false)
		{
			if (_hideTimerCounter > Constants.HideControlUITimer)
			{
				if (_animation != null)
				{

					if (_canPlayAnim == false)
					{
						PlayAnimation(-1);
						_canPlayAnim = true;

					}

				}

			} else
			{
				if (_gameController.menuActive == MenuActive.GAME)
				{
					_hideTimerCounter += Time.deltaTime / _container.Length;
				}
			}
		}
		ResetIfRequired();
	}

	/// <summary>
	/// Sets the children active to the bool
	/// </summary>
	/// <param name="b"></param>
	private void SetChildrenActive(bool b)
	{

		for (int i = 0; i < _container.Length; i++)
		{
			for (int ii = 0; ii < _container[i].transform.childCount; ii++)
			{
				_container[i].transform.GetChild(ii).gameObject.SetActive(b);
			}
		}
		_childrenActive = b;
	}

	/// <summary>
	/// Overrides the on pointer enter from base class
	/// </summary>
	/// <param name="data"></param>
	public virtual void OnPointerEnter(PointerEventData data)
	{
		_showUI = true;
		_hideTimerCounter = 0;


		if (_canPlayAnim)
		{
			PlayAnimation(1);
			_canPlayAnim = false;
		}
	}

	/// <summary>
	/// Plays the animation in the direction specified.
	/// </summary>
	/// <param name="direction"></param>
	private void PlayAnimation(int direction)
	{
		for (int i = 0; i < _container.Length; i++)
		{
			Animation _anim = _container[i].GetComponent<Animation>();
			_anim[_anim.clip.name].time =  (direction > 0) ? 0 : _anim[_anim.clip.name].length;
			_anim[_anim.clip.name].speed = direction;
			_anim.Play(_anim.clip.name);
		}
	}

	/// <summary>
	/// Overrides the on pointer exit
	/// </summary>
	/// <param name="data"></param>
	public virtual void OnPointerExit(PointerEventData data)
	{
		_showUI = false;

	}

	/// <summary>
	/// Resets the Container Control if it current menu does not equal to game.
	/// </summary>
	private void ResetIfRequired()
	{
		if (_gameController.menuActive != MenuActive.GAME)
		{
			_canPlayAnim = false;
			_showUI = false;
			_hideTimerCounter = 0;
		}
	}
}
