using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager_4 : MonoBehaviour
{

    private float score=1;
    public TextMeshProUGUI scoreText;
    private Vector3 lastAcceleration;
    private float lastTimestamp;
    public float velocityZ;
    void Start()
    {
        lastAcceleration = Input.acceleration;
        lastTimestamp = Time.timeSinceLevelLoad;
    }
    
    void Update()
    {
        Vector3 currentAcceleration = Input.acceleration;
        float currentTimestamp = Time.timeSinceLevelLoad;

        // acceleration on z axis
        float accelerationZ = (currentAcceleration.z - lastAcceleration.z) / (currentTimestamp - lastTimestamp);

        // instantenius velocity on z axis
        velocityZ = accelerationZ * (currentTimestamp - lastTimestamp);
        velocityZ = (Mathf.Abs (velocityZ));
        

        // update variables
        lastAcceleration = currentAcceleration;
        lastTimestamp = currentTimestamp;
    }
    public void RestartGame(){
       SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void  UpdateScore(){ 
        scoreText.text = "Score: " + score;
        score++;
    }

    public void Text()
    {
        Debug.Log("gamemanager4");
    }
}

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
     public Button restartButton;
    private float score;
    private Vector3 lastAcceleration;
    private float lastTimestamp;
    public float velocityZ;
    // Start is called before the first frame update
    public void  UpdateScore(){ 
    score+=10;    
    scoreText.text = "Score: " + score;
   }
   public void RestartGame(){
   SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}
   

private void Start()
{
    lastAcceleration = Input.acceleration;
    lastTimestamp = Time.timeSinceLevelLoad;
}

private void Update()
{
    Vector3 currentAcceleration = Input.acceleration;
    float currentTimestamp = Time.timeSinceLevelLoad;

    // Z eksenindeki ivmeyi hesapla
    float accelerationZ = (currentAcceleration.z - lastAcceleration.z) / (currentTimestamp - lastTimestamp);

    // Z eksenindeki anlık hızı hesapla
     velocityZ = accelerationZ * (currentTimestamp - lastTimestamp);
     velocityZ = (Mathf.Abs (velocityZ));

    // Veriyi kullanabilirsiniz

    // Değerleri güncelle
    lastAcceleration = currentAcceleration;
    lastTimestamp = currentTimestamp;
   
}
}
*/