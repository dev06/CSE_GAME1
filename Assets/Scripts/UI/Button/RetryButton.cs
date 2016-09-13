//Devan Patel
//Applications and Scripting
//Sep.12.2016
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class RetryButton : ButtonEventHandler {

	// Use this for initialization
	private float _speed = 50.0f;
	void Start ()
	{
		Init();
	}

	// Update is called once per frame
	void Update ()
	{
		if (HoverSprite != null && hovering)
		{
			transform.Rotate(new Vector3(0, 0, -Time.deltaTime * _speed));
			transform.GetChild(0).transform.Rotate(new Vector3(0, 0, Time.deltaTime * _speed));
		}
	}

	/// <summary>
	/// Overrides the on pointer click from base class.
	/// </summary>
	/// <param name="data"></param>
	public override void OnPointerClick(PointerEventData data)
	{
		base.OnPointerClick(data);

		if (buttonID == ButtonID.RETRY)
		{
			_gameController.Reset();
		}


		if (buttonID == ButtonID.QUIT)
		{
			Application.Quit();
		}
	}
}
