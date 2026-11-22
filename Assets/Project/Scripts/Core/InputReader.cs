using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
	[Header("Inputs")]
	[SerializeField] private KeyCode _jump = KeyCode.Z;
	[SerializeField] private KeyCode _shoot = KeyCode.X;

	public event Action JumpPressed;
	public event Action ShootPressed;

	private void Update()
	{
		if (Input.GetKeyDown(_jump))
		{
			JumpPressed?.Invoke();
		}

		if (Input.GetKeyDown(_shoot))
		{
			ShootPressed?.Invoke();
		}
	}
}
