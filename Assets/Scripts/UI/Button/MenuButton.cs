using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class MenuButton : ButtonEventHandler {

	// Use this for initialization
	private float _speed = 50.0f;
	private Animation _animation;
	void Start ()
	{
		Init();
		_animation = GameObject.FindWithTag("UI/MenuCanvas").transform.GetChild(0).GetComponent<Animation>();

	}

	// Update is called once per frame
	void Update ()
	{
		if (HoverSprite != null && hovering)
		{
			transform.Rotate(new Vector3(0, 0, -Time.deltaTime * _speed));
			transform.GetChild(0).transform.Rotate(new Vector3(0, 0, Time.deltaTime * _speed));
			//transform.GetChild(1).transform.Rotate(new Vector3(0, 0, Time.deltaTime * _speed));

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

	public override void OnPointerEnter(PointerEventData data)
	{

		base.OnPointerEnter(data);

		if (_animation.IsPlaying(_animation.clip.name) == false)
		{
			if (buttonID == ButtonID.CREDIT)
			{
				_animation["MenuBackGround"].time = 0;
				_animation["MenuBackGround"].speed = 1;
				_animation.Play("MenuBackGround");
			}
		}



	}

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
