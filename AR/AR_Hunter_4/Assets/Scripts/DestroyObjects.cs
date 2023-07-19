using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjects : MonoBehaviour
{
    private float destroyDelay = 30; 
    private GameManager_4 gameManager;
    public AudioClip explodeSound;
    public  ParticleSystem explosionParticle;
    private AudioSource playerAudio;
    private string soundPath;
    private float destroyDelay_touched = 2.0f; 
   
    void Start()
    {
        
        explosionParticle = GetComponent<ParticleSystem>();
        explosionParticle.Stop();
        playerAudio = GetComponent<AudioSource>();
        Destroy(gameObject, destroyDelay);
        gameManager = GameObject.Find("GameManager_4").GetComponent<GameManager_4>();

        gameManager.Text();
       // emitSetting = new ParticleSystem.EmitParams();
       

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
                   
                    gameManager.UpdateScore();
                    explosionParticle.Play();
                    playerAudio.PlayOneShot(explodeSound, 1.0f);
                 
                    Invoke("DestroyObject", destroyDelay_touched);
                }

              
                   

                
            }
        }
        
    }
    void DestroyObject()
    {
        Destroy(gameObject);
    }
}
