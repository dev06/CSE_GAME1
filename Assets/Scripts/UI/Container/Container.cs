using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class Container : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {


	private GameController _gameController;
	private Animation _animation;

	void OnEnable()
	{

	}

	void Start ()
	{
		_gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
		_animation = GetComponent<Animation>();
	}



	public void PlayAnimation(int direction)
	{
		if (direction > 0)
		{
			_animation[_animation.clip.name].time = 0;
		} else {
			_animation[_animation.clip.name].time = _animation[_animation.clip.name].length;
		}
		_animation[_animation.clip.name].speed = direction;
		_animation.Play(_animation.clip.name);
	}



	public virtual void OnPointerEnter(PointerEventData data)
	{
		_gameController.onContainer = true;
	}

	public virtual void OnPointerExit(PointerEventData data)
	{
		_gameController.onContainer = false;
	}


	void OnDisable()
	{

	}
}
