using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HormingEnemy : MonoBehaviour
{

    float timer = 0;

    public Transform player;
    GameObject[] target; //　標的の座標を取得
    //GameObject closePlayer;
    public GameObject SetAitemObj; // アイテム

    public float moveSpeed = 60.0f;
    float enemyHP_ = 10.0f;

    [SerializeField, Min(0)]
    float time = 10;
    [SerializeField]
    float lifeTime = 10;// 弾を時間で管理

    public GameObject particleObject;

   
    bool attackFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        

        

       


    }


    // Update is called once per frame
    void Update()
    {


        target = GameObject.FindGameObjectsWithTag("Player"); // タグから transform を取得


        foreach (GameObject player in target)
        {

            if (player.transform.position.z + 1500 > transform.position.z)
            {
                attackFlag = true;
            }
            // プレイヤーを通り過ぎたら生成中止
            if (player.transform.position.z + 300 > transform.position.z)
            {
                attackFlag = false;
            }


            if (player.transform.position.z < gameObject.transform.position.z)
            {
                if (attackFlag)
                {
                    transform.LookAt(player.transform);
                    GetComponent<Rigidbody>().velocity = transform.forward.normalized * moveSpeed;
                }
            }
            else
            {
                
                Destroy(gameObject);
            }

        }


        //時間経過で消滅
        time -= Time.deltaTime;

        if (time < 0f)
        {
           
            Destroy(gameObject);
            return;
        }

       

        //消滅処理ーーーーーーーーー

        //体力が0になったら消える
        if (enemyHP_ <= 0)
        {
            // 弾を生成 座標を合わせる
            Instantiate(SetAitemObj, new Vector3(transform.position.x,
                transform.position.y, transform.position.z), Quaternion.identity);
            Instantiate(particleObject, this.transform.position, Quaternion.identity); //パーティクル用ゲームオブジェクト生成
            Destroy(gameObject);
        }

        target = GameObject.FindGameObjectsWithTag("Player"); // タグから transform を取得


        foreach (GameObject player in target)
        {

            if (player.transform.position.z >gameObject.transform.position.z)
            {
               
                Destroy(gameObject);
            }                       
        }
    }


    public void Enemydamage_Normal()//ダメージを受けた時
    {
        enemyHP_ = enemyHP_ - 2.5f;
    }

    public void Enemydamage_Horming()//ダメージを受けた時
    {
        enemyHP_ = enemyHP_ - 1.0f;
    }

   

}
