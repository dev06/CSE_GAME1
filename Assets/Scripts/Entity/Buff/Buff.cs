using UnityEngine;
using System.Collections;

public class Buff : EntityItem {

	protected float _duration;
	protected float _currentBuffTime;
	void Start ()
	{
		Init();
	}


	public virtual void Tick() {}

	public virtual void UseBuff() {}

	public virtual void UseBuff(GameController _gameController) {}


}
