using System;
using UnityEngine;

public abstract class PooledObject : MonoBehaviour
{
	public event Action<PooledObject> ReadyToRelease;

	public void ReturnToPool()
	{
		ReadyToRelease?.Invoke(this);
	}
}
