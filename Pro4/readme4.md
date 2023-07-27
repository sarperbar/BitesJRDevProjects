# Prototype4
![Prototype4](https://connect-prd-cdn.unity.com/20190521/learn/images/7dd7bf7d-eb01-4753-9b05-69a4104a6cdb_4_1_full.png.400x0x1.webp)

Bu oyun karakteri düşürmeye çalışan ve sayıları yok oldukça dalgalar halinde artan düşmanlar yapmayı amaçlıyor. Öncelikle kamerayı karaktere atadık bu sayede sağa veya sola giderken kamera da dönüyordu.
Sonrasında rastgele spawn olan düşman toplarının oyuncu topunu sürekli takip etmesini sağladım. Bu toplar karakter topunun üzerinden geçip planeden aşağı düşerse yok olmalılardı.Bu yüzden karakterin kaçması gerekiyordu.
```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy : MonoBehaviour
{
    private Rigidbody enemyRb;
    private GameObject player;
    private float speed = 3.0f;
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player= GameObject.Find("Player");
    }
    void Update()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        enemyRb.AddForce( lookDirection * speed);
        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }
}
```
Karakterin kaçmadan da düşmanları düşürebilmesi için belirli zamanlarda beliren _powerup_ nesnesi oluşturdum(belirli bir süre sonra yok olan).
Bu nesneyi karakter aldığı zaman düşman topları ile temas ettiği anda düşman toplarını alanın dışına fırlatacaktı.
Bu mekaniği _PlayerController_ scriptinin içine yazdım.
```csharp
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private float forwardIn;
    private float speed = 5;
    private GameObject focalPoint;
    public bool hasPowerUp = false;
    private float powerUpSt = 15.0f;
    public GameObject PowerUpInd;
    private GameManager _GM;
    public bool isGame = true;
 
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint=GameObject.Find("FocalPoint");
        _GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    void Update()
    {
        forwardIn = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardIn);
        PowerUpInd.transform.position = transform.position;
        if (transform.position.y < -10)
        {
            _GM.GameOver();
            isGame = false;
        }    
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            hasPowerUp = true;
            Destroy(other.gameObject);
            StartCoroutine(CountdownPowerUp());
            PowerUpInd.gameObject.SetActive(true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
        if (collision.gameObject.CompareTag("Enemy") && hasPowerUp)
        {
            Vector3 awayfromPlayer = collision.gameObject.transform.position - transform.position;
            
            enemyRb.AddForce(awayfromPlayer * powerUpSt, ForceMode.Impulse);
            
            
            Debug.Log("Collided with"+ collision.gameObject.name + " with powerup set to"+ hasPowerUp);
        }

        if (collision.gameObject.CompareTag("HardEnemy") && hasPowerUp)
        {
            hasPowerUp = false;
            PowerUpInd.gameObject.SetActive(false);
        } 
    IEnumerator CountdownPowerUp()
    {
        yield return new WaitForSeconds(7);
        hasPowerUp = false;
        PowerUpInd.gameObject.SetActive(false);
    }
}

```
