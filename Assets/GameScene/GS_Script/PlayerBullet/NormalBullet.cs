using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : MonoBehaviour
{

    

    Vector3 Pos;
    [SerializeField]
    float lifeTime = 2;//íeÇéûä‘Ç≈ä«óùÇ∑ÇÈ



    Vector3 pos = new Vector3(0f, 0f, 6.0f);
    float directionFlag = 1;


    // Start is called before the first frame update
    void Start()
    {

        
        StartCoroutine(nameof(Timer));
    }



    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(pos.x, pos.y, pos.z*80) *Time.deltaTime;   
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
            other.GetComponent<HormingEnemy>().Enemydamage_Normal();
            Destroy(gameObject);
        }


        if (other.gameObject.tag == "UpEnemy")
        {
            other.GetComponent<UpEnemy>().UpEnemydamage_Normal();
            Destroy(gameObject);
        }



    }



}
