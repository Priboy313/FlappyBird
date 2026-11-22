using UnityEngine;
using UnityEngine.SceneManagement;


public class Game : MonoBehaviour
{
	[SerializeField] private Player _player;
	[SerializeField] private InputReader _inputReader;

	[SerializeField] private GameObject _startScreen;
	[SerializeField] private GameObject _gameOverScreen;

	private void OnEnable()
	{
		_player.GameOver += OnGameOver;
	}

	private void OnDisable()
	{
		_player.GameOver -= OnGameOver;
	}

	private void Start()
	{
		Time.timeScale = 0;
		_inputReader.enabled = false;
		_startScreen.SetActive(true);
		_gameOverScreen.SetActive(false);
	}

	public void PlayGame()
	{
		_inputReader.enabled = true;
		_startScreen.SetActive(false);
		Time.timeScale = 1;
	}

	private void OnGameOver()
	{
		_inputReader.enabled = false;
		Time.timeScale = 0;
		_gameOverScreen.SetActive(true);
	}

	public void RestartGame()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
