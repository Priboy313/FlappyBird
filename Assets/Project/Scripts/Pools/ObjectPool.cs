using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T: PooledObject
{
	private Transform _container;
	private T _prefab;
	private Queue<T> _pool;

	public ObjectPool(T prefab, Transform container)
	{
		_prefab = prefab;
		_container = container;
		_pool = new Queue<T>();
	}

	public T Get(Vector3 position, Quaternion rotation)
	{
		T item;

		if (_pool.Count == 0)
		{
			item = Object.Instantiate(_prefab, position, rotation, _container);
		}
		else
		{
			item = _pool.Dequeue();
			item.transform.position = position;
			item.transform.rotation = rotation;
		}

		item.ReadyToRelease += Release;
		item.gameObject.SetActive(true);

		return item;
	}

	private void Release(PooledObject item)
	{
		item.ReadyToRelease -= Release;

		if (item is T typedItem)
		{
			_pool.Enqueue(typedItem);
			typedItem.gameObject.SetActive(false);
		}

	}
}