using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Class that handles the weapon of the turret
public class TurretWeaponController : MonoBehaviour
{
	[Tooltip("Fire rate")]
	[SerializeField]
	private float _fireRate = 0.15f;
	[Tooltip("List of transforms of barrel tips")]
	[SerializeField]
	private List<Transform> _WeaponTips = new List<Transform>();
	private BulletPool _pool;
	private float _nextFireTime;
	private int alternator = 0;

	private void Start()
	{
		_pool = GameObject.FindGameObjectWithTag("EnemyBulletPool").GetComponent<BulletPool>();
	}
	//Handles fireing the bullet if fire rate ammount of time has passed
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
