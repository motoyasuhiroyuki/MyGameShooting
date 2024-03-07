using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetEnemy : MonoBehaviour
{
    //public Transform SetEnemyTransform; 

    GameObject[] target; // 標的の座標を取得

    public GameObject SetEnemyObj; // 発射する敵のオブジェクト

    public Vector3 setPos;// 発射位置

    public float interval = 3.0f; // 何秒間隔で撃つか
    private float timer = 0.0f; // 時間カウント用のタイマー

    bool attackFlag = false; // オブジェクト生成フラグ

    public int shootmode = 1; //単発 1, 複数 2

    void Start()
    {

        

    }
    // Update is called once per frame
    void Update()
    {
        // タグから transform を取得
        target = GameObject.FindGameObjectsWithTag("Player"); 
                                                              

        foreach (GameObject player in target)
        {
            // プレイヤーとの距離が1000より近くにいたら生成開始
            if (player.transform.position.z+1500 > transform.position.z )
            {
                attackFlag = true;
            }
            // プレイヤーを通り過ぎたら生成中止
            if(player.transform.position.z+300>transform.position.z)
            {
                attackFlag = false;
            }
           
        }

        if (timer <= 0.0f && attackFlag)// 
        {
            if (shootmode == 1)
            {
                // 弾を生成 座標を合わせる
                Instantiate(SetEnemyObj, new Vector3(transform.position.x,
                    transform.position.y, transform.position.z), Quaternion.identity);
                timer = 100; // 間隔をセット
            }



            if (shootmode == 2)
            {
                // 弾を生成 座標を合わせる
                Instantiate(SetEnemyObj, new Vector3(transform.position.x,
                    transform.position.y, transform.position.z), Quaternion.identity);
                timer = interval; // 間隔をセット
            }
        }


       


        // タイマーの値を減らす
        if (timer > 0.0f)
        {
            timer -= Time.deltaTime;
        }

    }

}
