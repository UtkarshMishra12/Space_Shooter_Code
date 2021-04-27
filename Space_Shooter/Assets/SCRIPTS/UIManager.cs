using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text ScoreText;
    [SerializeField]
    private Image _LiveImage;
    [SerializeField]
    private Sprite[]  _liveSprite;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text restarText;
    private GameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    {
        
        ScoreText.text = "Score: " + 0;
        _gameOverText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("GameManger").GetComponent<GameManager>();
        if (_gameManager == null)
        {
            Debug.Log("NUll");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateScore(int playerscore)
    {
        ScoreText.text = "Score: " + playerscore.ToString();
    }

    public void UpdateLives( int currentLives)
    {
        _LiveImage.sprite = _liveSprite[currentLives];
        if (currentLives < 1)
        {
            GameOverSequence();
        }
    }

    void GameOverSequence()
    {
        _gameManager.GameOver();
        _gameOverText.gameObject.SetActive(true);
        restarText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine());
    }

    IEnumerator GameOverFlickerRoutine()
    {
        while (true)
        {
            _gameOverText.text = "  GAME OVER  ";
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = " ";
            yield return new WaitForSeconds(0.5f);
        }
    }
}
