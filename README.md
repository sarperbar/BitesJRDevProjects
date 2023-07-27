# Sarper ERBAR BİTES Staj Raporu

>Bitirdiğim Pathway ve Tutorials: [Unity Hesabım](https://learn.unity.com/u/6322fd72edbc2a2a9ee73461?tab=profile "Sarper Erbar")
## İlk Hafta
Unity JR Pathway'ine başladım.
Pathway'de toplam 5 adet oyun yapma görevi vardı ve ilk hafta boyunca bu oyunlar üzerine çalıştım.
Eğitimin sonunda bütün oyunları fixlemeye uğraştım.
_OOP_ ile alakalı olan kısımları inceledim ve ilerideki projelerimde nasıl kullanacağım hakkında fikir sahibi oldum.  



## İkinci Hafta
Unity AR eğitimine başladım ve ilk iki gün bununla uğraştım. AR'ın temel mekanikleri anlamaya çalıştım. Pathwaydeki yüz filtreleme
uygulamasını yaptım. Yürümeye teşvik eden Treasure Hunter tarzında bir oyun için araştırmaya başladım.
Öncelikle GPS ve Cesium kullanarak oyuncunun yürüdüğünü algılamayı düşündüm ama bunları çalıştıramadım. Bu yüzden telefonun gyro sensörünü optimum şekilde filtreleyip
yürüdükçe spawn olan objeler şeklinde bir script yazdım.

```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Spawner : MonoBehaviour
{
    public GameObject[] objectPrefabs;
    private GameManager_4 gameManager;
    [SerializeField] private TextMeshProUGUI spawnText;
    

    // spawn range
    private float spawnRangeX =15;
    private float spawnRangZ = 15;
    private float spawnRangey = 2;
    
    private float lastAcceleration;
    private int stepCount;
    private float lastStepTime;
    int time=0;
    float previousVelocityZ;
    
    
    void Start()
    {
        gameManager=GameObject.Find("GameManager_4").GetComponent<GameManager_4>();

    }
    void Update()
    {

        if (!gameManager.isPause)
        {

            float velocityZDifference = Mathf.Abs(gameManager.velocityZ - previousVelocityZ);

            if (gameManager.velocityZ < 0.6f && gameManager.velocityZ > 0.1f && velocityZDifference < 0.05f)
            {
                if (Time.timeSinceLevelLoad - time > Random.Range(5, 15))
                {
                    RandomPrefab();
                    RandomPrefab();
                    RandomPrefab();
                    RandomPrefab();
                    spawnText.gameObject.SetActive(true);
                    StartCoroutine("SpawnTextDelay");
                    time = (int)Time.timeSinceLevelLoad;
                }
            }


            previousVelocityZ = gameManager.velocityZ;

        }




    }
   
    void RandomPrefab()
    {
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX),Random.Range(-spawnRangey, spawnRangey), Random.Range(-spawnRangZ, spawnRangZ));
        int prefabIndex = Random.Range(0, objectPrefabs.Length);
        Instantiate(objectPrefabs[prefabIndex], spawnPos, objectPrefabs[prefabIndex].transform.rotation);
       
    }
    
    
    private IEnumerator SpawnTextDelay()
    {
        yield return new WaitForSeconds(3.0f);
        spawnText.gameObject.SetActive(false);
        
    }
}

```

Bir sonraki adımda kullanıcı objelerin üstüne dokununca objelerin patlaması gerekiyordu. Bunun için aşağıdaki scripti kullandım.
```csharp
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjects : MonoBehaviour
{
    private bool isDestroyed = true;
    [SerializeField] float destroyTime = 20f;
    private float elapsedTime = 0f;
    private GameManager_4 gameManager;
    [SerializeField] AudioClip explodeSound;
    [SerializeField] AudioClip spawnSound;
    public ParticleSystem explosionParticle;
    private AudioSource playerAudio;
    private string soundPath;
    private float hideDelay = 2f;


    private void Awake()
    {
        explosionParticle = GetComponent<ParticleSystem>();
        playerAudio = GetComponent<AudioSource>();
    }

    void Start()
    {
        explosionParticle.Stop();
        playerAudio.PlayOneShot(spawnSound, 1.0f);
        gameManager = GameObject.Find("GameManager_4").GetComponent<GameManager_4>();
        Invoke("DestroyObject", destroyTime);
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    explosionParticle.Play();
                    playerAudio.PlayOneShot(explodeSound, 1.0f);
                    StartCoroutine(DestroyAfterParticleSystem());
                }
            }
        }
    }

    private IEnumerator DestroyAfterParticleSystem()
    {
        if (isDestroyed)
        {
            gameManager.UpdateScore();
            isDestroyed = false;
        }
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
        
    }
    void DestroyObject()
    {
        Destroy(gameObject);
        gameManager.minesScoreText.gameObject.SetActive(true);
        gameManager.objectHBDestroyed++;
        gameManager.DownScore();
    }
}


```