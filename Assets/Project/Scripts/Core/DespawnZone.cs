using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class DespawnZone : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.attachedRigidbody != null && collision.attachedRigidbody.TryGetComponent(out PooledObject obj))
		{
			obj.ReturnToPool();
		}
	}
}
