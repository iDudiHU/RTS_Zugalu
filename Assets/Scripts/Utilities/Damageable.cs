using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

//Damagable base class that all class that will be damageable inherit from

public class Damageable : MonoBehaviour, IDamageable
{
	[Tooltip("Hp of the object")]
	[SerializeField] 
	protected float _hp;
	[Tooltip("Particles to spawn on Die")]
	[SerializeField] 
	private GameObject _destroyedParticles;
	[Tooltip("Can the object die")]
	[SerializeField]
	private bool _invincible;

	protected float _maxHp = float.PositiveInfinity;
	public float GetHp => _hp;
	public float GetMaxHp => _maxHp;

	protected virtual void Start()
	{
		SetMaxHp(_maxHp, _hp);
		_maxHp = _hp;
		ChangeHp(_maxHp);
	}

	

	//What to do when the gamobject _hp reaches 0
	public void Die()
	{
		_destroyedParticles.transform.SetParent(null);
		_destroyedParticles.SetActive(true);
		Destroy(gameObject);
	}

	//Public setter function for changing hp derived from IDamageable
	public void ChangeHp(float ammount)
	{
		if (_invincible) return;
		_hp += ammount;
		_hp = Mathf.Clamp(_hp, .0f, _maxHp);

		if (_hp <= .0f)
		{
			_hp = .0f;
			Die();
		}
	}
	private void SetMaxHp(float maxHp, float hp)
	{
		
	}
}
