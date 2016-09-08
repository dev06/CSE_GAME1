using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class RetryButton : ButtonEventHandler {

	// Use this for initialization
	private float _speed = 50.0f;
	void Start () {
		Init();
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
	}
}
