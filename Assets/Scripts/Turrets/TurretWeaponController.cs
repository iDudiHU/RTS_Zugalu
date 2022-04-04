using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretWeaponController : MonoBehaviour
{
	[SerializeField]
	private float _hp;
	[SerializeField]
	private float _speed;
	[SerializeField]
	private float _fireRate = 0.15f;
	[SerializeField]
	private List<Transform> _WeaponTips = new List<Transform>();
	private BulletPool _pool;
	private float _nextFireTime;
	private int alternator = 0;

	private void Start()
	{
		_pool = GameObject.FindGameObjectWithTag("EnemyBulletPool").GetComponent<BulletPool>();
	}
	public void Fire()
	{
		if (_nextFireTime > Time.time) return;
		Bullet b = _pool.GetBulletFromPool();
		if (b == null) return;
		if (alternator == 0)
		{
			b.Shoot(_WeaponTips[alternator]);
			_nextFireTime = Time.time + _fireRate;
			alternator++;
		}
		else
		{
			b.Shoot(_WeaponTips[alternator]);
			_nextFireTime = Time.time + _fireRate;
			alternator = 0;
		}
		
	}
}
