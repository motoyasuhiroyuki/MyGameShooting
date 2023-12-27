using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : MonoBehaviour
{

    

    Vector3 Pos;
    [SerializeField]
    float lifeTime = 2;//íeÇéûä‘Ç≈ä«óùÇ∑ÇÈ

   
    public float Speed = 2f;

    int directionFlag = 1;


    // Start is called before the first frame update
    void Start()
    {
        

        StartCoroutine(nameof(Timer));
    }



    // Update is called once per frame
    void Update()
    {

        Pos = transform.position;


        Pos.z += Speed;
       
     

        transform.position = Pos;
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(lifeTime);

        Destroy(gameObject);
        
    }

    private void OnTriggerEnter(Collider other)
    {



        if (other.gameObject.tag == "HormingEnemy")
        {
            other.GetComponent<HormingEnemy>().Enemydamage();
            Destroy(gameObject);
        }


        if (other.gameObject.tag == "UpEnemy")
        {
            other.GetComponent<UpEnemy>().UpEnemydamage();
            Destroy(gameObject);
        }



    }



}
