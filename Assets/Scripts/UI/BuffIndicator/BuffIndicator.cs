using UnityEngine;
using System.Collections;

public class BuffIndicator : BuffContainer {

	public float targetValue;
	public float targetMaxValue;
	public bool alive;
	private RectTransform _rectTransform;
	private Vector2 _size;
	private float _sizeX;
	private float _sizeY;
	void Start ()
	{
		_rectTransform = transform.GetChild(0).transform.GetComponent<RectTransform>();
		_size = _rectTransform.sizeDelta;
		_sizeX = _size.x;
		_sizeY = _size.y;
		alive = true;
	}

	void Update ()
	{
		_size = new Vector2((targetValue * _sizeX) / targetMaxValue, _sizeY);
		_rectTransform.sizeDelta = _size;

		if (!alive)
		{
			Destroy(gameObject);
		}
	}
}
