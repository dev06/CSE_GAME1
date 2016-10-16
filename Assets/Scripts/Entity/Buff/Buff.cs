using UnityEngine;
using System.Collections;

public class Buff : EntityItem {

	public bool _active;
	protected float _duration;
	protected float _currentBuffTime;
	protected GameObject _buffIcon;
	protected BuffIndicator _buffIndicator;
	protected GameObject _container;
	void Start ()
	{
		Init();

	}


	public virtual void Tick() {}

	public virtual void UseBuff() {}

	public virtual void UseBuff(GameController _gameController) {}


}
