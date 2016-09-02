using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class ContainerController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	protected GameController _gameController;
	protected static float _hideTimerCounter;
	protected static bool _showUI;
	protected bool _childrenActive;
	public ContainerController.Role role;
	private Animation _animation;
	private bool _canPlayAnim;
	private static GameObject[] _container;
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

					PlayAnimation(-1);

				}
				if (_childrenActive == true)
				{
					//SetChildrenActive(false);

				}
			} else
			{
				_hideTimerCounter += Time.deltaTime / _container.Length;
			}
		}
	}


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

	public virtual void OnPointerEnter(PointerEventData data)
	{
		_showUI = true;
		_hideTimerCounter = 0;
		if (_animation != null)
		{
			_animation[_animation.clip.name].time =  0;
		}
		if (_canPlayAnim)
			PlayAnimation(1);
	}


	private void PlayAnimation(int direction)
	{

		_animation[_animation.clip.name].speed = direction;
		_animation.Play(_animation.clip.name);
	}

	public virtual void OnPointerExit(PointerEventData data)
	{
		if (_animation != null)
		{
			_animation[_animation.clip.name].time =  _animation[_animation.clip.name].length;
		}
		_showUI = false;
		_canPlayAnim = true;
	}


	public enum Role
	{
		MASTER,
		SERVANT,
	}
}
