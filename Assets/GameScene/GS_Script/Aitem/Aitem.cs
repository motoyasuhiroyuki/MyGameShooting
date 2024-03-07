using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class Aitem : MonoBehaviour
{

    GameObject[] target; //　標的の座標を取得
    float moveSpeed = 40.0f;
    bool hormingFlag = false;
    public float hormingRanzeX = 10.0f;
    public float hormingRangeZ = 50.0f;
    [SerializeField, Min(0)]
    float time = 10.0f;

    
   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

       
      


        Vector3 pos = transform.position;
        //pos.z -= 0.1f;
        transform.position = pos;
        target = GameObject.FindGameObjectsWithTag("Player"); // タグから transform を取得

        

        foreach (GameObject player in target)
        {

            if (player.transform.position.z + hormingRangeZ >= transform.position.z && player.transform.position.z - hormingRangeZ <= transform.position.z)
                if(player.transform.position.x + hormingRanzeX >= transform.position.x && player.transform.position.x - hormingRanzeX <= transform.position.x)
            {
                    hormingFlag = true;
            }

            if (hormingFlag)
            {
                transform.LookAt(player.transform);
                GetComponent<Rigidbody>().velocity = transform.forward.normalized * moveSpeed *2;
            }
        }


        //時間経過で消滅
        time -= Time.deltaTime;

        if (time < 0f)
        {
            Destroy(gameObject);
            return;
        }

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<UI>().ScoreUp();
            Destroy(gameObject);
        }

    }

   


}
