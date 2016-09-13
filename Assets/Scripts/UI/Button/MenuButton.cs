//Devan Patel
//Applications and Scripting
//Sep.12.2016
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class MenuButton : ButtonEventHandler {

	private float _speed = 50.0f;
	private Animation _animation;

	void Start ()
	{
		Init();
		_animation = GameObject.FindWithTag("UI/MenuCanvas").transform.GetChild(0).GetComponent<Animation>();
	}

	void Update ()
	{
		if (HoverSprite != null && hovering)
		{
			transform.Rotate(new Vector3(0, 0, -Time.deltaTime * _speed));
			transform.GetChild(0).transform.Rotate(new Vector3(0, 0, Time.deltaTime * _speed));
		}

		if (_animation[_animation.clip.name].speed == -1)
		{
			if (_animation.IsPlaying(_animation.clip.name) == false)
			{
				if (_gameController.menuActive == MenuActive.MENU)
				{
					_gameController.EnableMenu(MenuActive.GAME);
				}
			}
		}
	}
	/// <summary>
	/// Overrides the on pointer enter from base class.
	/// </summary>
	/// <param name="data"></param>
	public override void OnPointerEnter(PointerEventData data)
	{

		base.OnPointerEnter(data);

		if (_animation.IsPlaying(_animation.clip.name) == false)
			if (buttonID == ButtonID.CREDIT)
			{
				if (buttonID == ButtonID.CREDIT)
				{
					_animation["MenuBackGround"].time = 0;
					_animation["MenuBackGround"].speed = 1;
					_animation.Play("MenuBackGround");
				}
			}
	}
	/// <summary>
	/// Overrides the on pointer exit from base class
	/// </summary>
	/// <param name="data"></param>
	public override void OnPointerExit(PointerEventData data)
	{
		base.OnPointerExit(data);
		if (buttonID == ButtonID.CREDIT)
		{
			_animation["MenuBackGround"].time = _animation["MenuBackGround"].length;
			_animation["MenuBackGround"].speed = -1;
			_animation.Play("MenuBackGround");
		}
	}

	/// <summary>
	/// Overrides the on pointer click from base class
	/// </summary>
	/// <param name="data"></param>
	public override void OnPointerClick(PointerEventData data)
	{
		if (buttonID == ButtonID.PLAY)
		{
			_animation[_animation.clip.name].time = _animation[_animation.clip.name].length;
			_animation[_animation.clip.name].speed = -1;
			_animation.Play(_animation.clip.name);
		} else if (buttonID == ButtonID.CREDIT)
		{

		} else if (buttonID == ButtonID.QUIT)
		{
			Application.Quit();
		}
		base.OnPointerClick(data);

	}
}
