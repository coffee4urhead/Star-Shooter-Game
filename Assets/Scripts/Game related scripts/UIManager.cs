using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [SerializeField] private Sprite[] _challengeSprites;
    [SerializeField] private Image _challengesInCurrent;
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private GameObject _playerOfTheGame;
    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField]
    private Image _livesImage;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _continueText;
    private GameManager _gameManager;
    void Start()
    {
        _scoreText.text = "Score: " + "0";
        _gameOverText.gameObject.SetActive(false);
        _continueText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        if (_gameManager == null)
        {
            Debug.Log("The game manager is null!");
        }
    }

    // Update is called once per frame
    public void UpdateScore(int newScore)
    {
        _scoreText.text = "Score: " + newScore.ToString();
    }

    public void UpdateChallengesDisplay(int challengesLeft)
    {
        _challengesInCurrent.sprite = _challengeSprites[challengesLeft];
    }
    public void UpdateLivesCount(int livesOfThePlayer)
    {
        _livesImage.sprite = _liveSprites[livesOfThePlayer];
    }
    public void DisplayGameOver()
    {
        GameOverSequence();
    }
    void GameOverSequence()
    {
        _gameManager.GameOver();
        _continueText.gameObject.SetActive(true);
        _gameOverText.gameObject.SetActive(true);
        StartCoroutine(CreateWigglingEffect());
    }
    IEnumerator CreateWigglingEffect()
    {
        while (true)
        {
            _gameOverText.text = "Game Over";
            yield return new WaitForSeconds(1f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(1f);
        }
    }
}
