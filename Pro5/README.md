# Prototype5
Bu oyunda FruitNinja tarzı, aşağıdan yukarıya gelen objeleri patlatmamız bekleniyor. Bu projede genel olarak UI öğretmek amaçlanmış.
Spawn olma sıklığını ayarlayan bir zorluk ayarı ekledim.
```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class DifficultyButton : MonoBehaviour
{
    private Button button;
    private GameManager _gameManager;
    public int difficulty;    
    
    void Start()
    {
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        button = GetComponent<Button>();
        button.onClick.AddListener(SetDifficulty);
    }

    void SetDifficulty()
    {
        _gameManager.StartGame(difficulty);
               
    }
}

```
```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> targets;
    private float spawnRate = 1.0f;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI liveText;
    private int score = 0;
    public bool isGame = true;
    public Button restartButton;
    public GameObject titleScreen;
    private int isLive = 3;
    [SerializeField] private GameObject pauseMenu;
    void Start()
    {
        pauseMenu.SetActive(false);
    }
    // Empty Unity callbacks also will be called from Mono, so if you will not use, do not write them. 
    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnTarget()
    {
        while (isGame)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
            

        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
        
        
    }

    public void GameOver()
    {
        if (isLive==0 &&isGame)
        {
            restartButton.gameObject.SetActive(true);
            gameOverText.gameObject.SetActive(true);
            isGame = false;

        }

        liveText.text = "Live:" + isLive;
        isLive--;
    }

    public void RestartGame()
    {
       
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {
        isGame = true;
        StartCoroutine(SpawnTarget());
        score = 0;
        titleScreen.gameObject.SetActive(false);
        spawnRate /= difficulty;
        liveText.gameObject.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void Pause()
    {
// investigate timescale and time freeze alternatives. Dangerous!!!!!
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        
    }
    public void MainMenu()
    {
//        SceneManager.LoadScene("MainMenu");

    }
}

```
Bu proje ile Unity JR Pathway bitmiş oldu. Pathwaydaki challengeları da yaptım ama karışık oldukları için yükleyebilecek hale gelmediler.
