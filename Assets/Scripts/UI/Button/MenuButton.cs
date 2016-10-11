//Devan Patel
//Applications and Scripting
//Sep.12.2016
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class MenuButton : ButtonEventHandler {

	private float _speed = 50.0f;
	private Animation _animation;
	private Animation _rootAnimation;
	private GameObject _hoverContainer;
	private Text _hoverContainerText;
	void Start ()
	{
		Init();
		_rootAnimation = GameObject.FindWithTag("UI/MenuCanvas").transform.GetChild(0).GetComponent<Animation>();

		_animation = transform.parent.FindChild("HoverContainer").GetComponent<Animation>();
		_hoverContainer = transform.parent.FindChild("HoverContainer").gameObject;
		_hoverContainerText = _hoverContainer.transform.FindChild("Hover").transform.FindChild("Text").GetComponent<Text>();
	}

	void Update ()
	{
		if (HoverSprite != null && hovering)
		{
			transform.Rotate(new Vector3(0, 0, -Time.deltaTime * _speed));
			transform.GetChild(0).transform.Rotate(new Vector3(0, 0, Time.deltaTime * _speed));
		}

		if (_rootAnimation != null)
		{
			if (_rootAnimation[_rootAnimation.clip.name].speed == -1)
			{
				if (_rootAnimation.IsPlaying(_rootAnimation.clip.name) == false)
				{
					if (_gameController.menuActive == MenuActive.MENU)
					{
						_gameController.EnableMenu(MenuActive.GAME);
					}
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
		if (_animation != null)
		{
			//	if (_animation.IsPlaying(_animation.clip.name) == false)
			//	{

			if (buttonID == ButtonID.CREDIT)
			{
				_hoverContainerText.text = "Font - Code Bold\n   \nUnity Standard Assets\n  \nGoogle Images\n  \nDevan Patel" ;
				_hoverContainerText.fontSize = 125;
				_animation["MenuBackGround"].time = 0;
				_animation["MenuBackGround"].speed = 1;
				_animation.Play("MenuBackGround");
			}


			if (buttonID == ButtonID.CONTROL)
			{
				_hoverContainerText.text = "Controls\n\nE to open inventory \n\nRight click to interact";
				_hoverContainerText.fontSize = 130;
				_animation["MenuBackGround"].time = 0;
				_animation["MenuBackGround"].speed = 1;
				_animation.Play("MenuBackGround");
			}
			//}
		}
	}
	/// <summary>
	/// Overrides the on pointer exit from base class
	/// </summary>
	/// <param name="data"></param>
	public override void OnPointerExit(PointerEventData data)
	{
		base.OnPointerExit(data);
		if (_animation != null)
		{
			if (buttonID == ButtonID.CREDIT)
			{
				_animation["MenuBackGround"].time = _animation["MenuBackGround"].length;
				_animation["MenuBackGround"].speed = -1;
				_animation.Play("MenuBackGround");
			}

			if (buttonID == ButtonID.CONTROL)
			{
				_animation["MenuBackGround"].time = _animation["MenuBackGround"].length;
				_animation["MenuBackGround"].speed = -1;
				_animation.Play("MenuBackGround");
			}
		}
	}

	/// <summary>
	/// Overrides the on pointer click from base class
	/// </summary>
	/// <param name="data"></param>
	public override void OnPointerClick(PointerEventData data)
	{
		if (_rootAnimation != null)
		{
			if (buttonID == ButtonID.PLAY)
			{
				_rootAnimation[_rootAnimation.clip.name].time = _rootAnimation[_rootAnimation.clip.name].length;
				_rootAnimation[_rootAnimation.clip.name].speed = -1;
				_rootAnimation.Play(_rootAnimation.clip.name);

			} else if (buttonID == ButtonID.CREDIT)
			{

			} else if (buttonID == ButtonID.QUIT)
			{
				Application.Quit();
			}
		}

		base.OnPointerClick(data);

	}
}
