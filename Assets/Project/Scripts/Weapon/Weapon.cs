using UnityEngine;

public class Weapon : MonoBehaviour
{
	[SerializeField] private BulletSpawner _bulletSpawner;
	[SerializeField] Transform _firePoint;
	[SerializeField] private float _bulletSpeed = 3f;

	public void Init(BulletSpawner bulletSpawner)
	{
		_bulletSpawner = bulletSpawner;
	}

	public void Shoot()
	{
		_bulletSpawner.Spawn(_firePoint.position, _firePoint.rotation, _bulletSpeed);
	}
}
