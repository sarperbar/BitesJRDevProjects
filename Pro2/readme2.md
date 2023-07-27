# Prototype 2
Bu oyunda üzerimize doğru gelen hayvanları ileriye doğru yemek fırlatarak beslememiz (vurmamız) bekleniyor. Karakter her doğrultuda hareket edebilmeli ve 'space' ile yiyecek fırlatmalı.
Prototype1'e çok benzer dinamikleri var. Karakterin oyun sahnesinden çıkmaması için hareket scriptine boundries ekledim.
```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerController : MonoBehaviour
{
    private float HorizontalInput;
    private float VerticalInput;
    private float speed= 10.0f;
    private float xRange =22.0f;
    public GameObject projectilePrefab;
    public int playerH=3;
    public TextMeshProUGUI scoreText;
    private GameManager _GM;    
    
    void Start()
    {
        _GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
        
    void Update()
    {
    
        if(transform.position.x < -xRange ){
                transform.position = new Vector3( -xRange,transform.position.y, transform.position.z );

        }

         if(transform.position.x > xRange ){
                transform.position = new Vector3( xRange,transform.position.y, transform.position.z );

        }
        if(transform.position.z < -4 ){
                transform.position = new Vector3( transform.position.x,transform.position.y, -4 );

        }

         if(transform.position.z > 12 ){
                transform.position = new Vector3( transform.position.x,transform.position.y, 12 );

        }
        
        HorizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * HorizontalInput * Time.deltaTime * speed) ;  
        VerticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * VerticalInput * Time.deltaTime * speed );  

            if(Input.GetKeyDown(KeyCode.Space))     {

                Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
            }       
          
    }
      
        public void TakeDamage(int amount){
            if (playerH > 0)
            {
                playerH -= amount;
                scoreText.text = "Health: " + playerH;
            }else{

            Destroy(gameObject);
            _GM.GameOver();
            }
    }
         public void RestoreH(){
        
        playerH++;
        scoreText.text = "Health: " + playerH;
    }
                   
}

```
Ek olarak UI ekledim.