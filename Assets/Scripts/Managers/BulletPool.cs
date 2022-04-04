using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
	[SerializeField] private GameObject _bulletPrefab;
	[SerializeField] private int _startCount = 20, _maxCount = 30;
	private Queue<Bullet> _pool = new Queue<Bullet>();
	private int _createdBulletCount;

	private void Start()
	{
		for (int i = 0; i < _startCount; i++) CreateBullet();
	}

	private void CreateBullet()
	{
		if (_createdBulletCount == _maxCount) return;
		_createdBulletCount++;
		GameObject newBullet = Instantiate(_bulletPrefab, transform);
		Bullet b = newBullet.GetComponent<Bullet>();
		b.SetBulletPool(this);
		b.ResetBullet();
	}

	public void AddbulletToPool(Bullet bullet)
	{
		_pool.Enqueue(bullet);
	}

	public Bullet GetBulletFromPool()
	{
		if (_pool.Count == 0) CreateBullet();

		if (_pool.Count == 0) return null;

		return _pool.Dequeue();
	}
}
