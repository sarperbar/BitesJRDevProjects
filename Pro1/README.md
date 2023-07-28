
# Prototype 1
Bu oyunda objelerin hareketini anlamamız isteniyor. Araba bir yol üstünde giderken üstüne doğru ters yönde başka arabaların spawn olmasını sağlıyoruz.
Bu prototype'in sorunu araba ileri doğru hareket ederken yolun sabit kalması ve yol bitince arabanın yoldan düşmesiydi. Buna çözüm olarak 'W' tuşuna basıldığında
yolu oynattım ve hareketi böyle sağladım.
```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadMove : MonoBehaviour
{
    private Vector3 startPos;
    private float repeatz;
    public float verticalInput;
    private float speed = 20f;
    void Start()
    {
        startPos= transform.position;
    }
    void Update()
    {              
        repeatz= GetComponent<BoxCollider>().size.z /2 ;
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.right * Time.deltaTime * speed * verticalInput);
        if(transform.position.z <  startPos.z - repeatz ){
            transform.position=startPos;
        }
    }
    }

```

Sonrasında first person kamera ekledim.

```csharpusing System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamCont : MonoBehaviour
{
    public GameObject Cam1;
    public GameObject Cam2;
        
    void Update()
    {

        if(Input.GetKeyDown("1")){
            setCam1();
        }
        if(Input.GetKeyDown("2")){
            setCam2();
        }       
    }
    
    void setCam1(){
        Cam1.SetActive(true);
        Cam2.SetActive(false);
    }
    void setCam2(){
        Cam1.SetActive(false);
        Cam2.SetActive(true);
    }
}
```
Son olarak da UI kısmını bitirdim ve ilk proje bitmiş oldu.
