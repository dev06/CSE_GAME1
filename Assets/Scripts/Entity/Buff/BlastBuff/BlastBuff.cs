using UnityEngine;
using System.Collections;

public class BlastBuff : Buff {

	// Use this for initialization
	void Start ()
	{
		Init();
	}

	// Update is called once per frame
	void Update ()
	{

	}

	public override void UseBuff(GameController _gameController)
	{
		_active = true;
		GameObject _blast = Instantiate(Constants.Blast_Buff_Particle, _gameController.Player.transform.position, Quaternion.identity) as GameObject;
		_blast.transform.SetParent(_gameController.activeEntities.transform);
		_blast.transform.rotation = Quaternion.Euler(new Vector3(270, 0, 0));

		GameObject[] _enemies = GameObject.FindGameObjectsWithTag("Entity/Enemy");
		for (int i = 0; i < _enemies.Length; i++)
		{
			if (_enemies[i] != null)
			{
				GameObject _currentEnemy = _enemies[i];
				float _distance = Vector3.Distance(_gameController.Player.transform.position, _currentEnemy.transform.position);
				if (_distance < Constants.BlastRadius)
				{
					_currentEnemy.SendMessage("DoDamage", Constants.BlastDamage);
				}
			}
		}
	}
}
