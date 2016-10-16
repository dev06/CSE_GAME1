using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class BuffIndicator : BuffContainer {

	public float targetValue;
	public float targetMaxValue;
	public bool alive;
	private Image _maskImage;
	private RectTransform _rectTransform;
	void Start ()
	{
		_maskImage = transform.GetChild(0).GetComponent<Image>();
		_rectTransform = transform.GetComponent<RectTransform>();
		_rectTransform.localScale = new Vector3(1, 1, 1);
		alive = true;
	}

	void Update ()
	{

		_maskImage.fillAmount = (targetValue / targetMaxValue);

		if (!alive)
		{
			Destroy(gameObject);
		}
	}
}
