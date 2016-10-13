using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuffContainer : MonoBehaviour {

	public List<GameObject> currentBuffs = new List<GameObject>();
	private GameController _gameController;
	private float _containerHeight;
	void Start () {
		_gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
		_containerHeight = transform.GetComponent<RectTransform>().sizeDelta.y;

	}

	void Update ()
	{
		PositionBuffs();
	}

	private void PositionBuffs()
	{
		if (currentBuffs.Count > 0)
		{
			for (int i = 0; i < currentBuffs.Count; i++)
			{
				if (currentBuffs[i] != null)
				{
					currentBuffs[i].transform.localPosition = new Vector3(0, i * 50, 0);
				} else
				{
					currentBuffs.Remove(currentBuffs[i]);
				}
			}
		}
	}
}
