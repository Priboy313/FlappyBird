using UnityEngine;


namespace Systems.Gameplay.Bird {

	[RequireComponent(typeof(Camera))]
	public class BirdTracker : MonoBehaviour
	{
		[SerializeField] private Bird _bird;
		[SerializeField] private float _offset;

		private Vector3 _position;

		private void Update()
		{
			_position = transform.position;
			_position.x = _bird.transform.position.x + _offset;
			transform.position = _position;
		}
	}
}
