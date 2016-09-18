using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class Container : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {


	private GameController _gameController;

	void Start ()
	{
		_gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
	}



	public virtual void OnPointerEnter(PointerEventData data)
	{
		_gameController.onContainer = true;

	}

	public virtual void OnPointerExit(PointerEventData data)
	{
		_gameController.onContainer = false;
	}

}
