using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//using static UnityEditor.PlayerSettings;

public class UpEnemy : MonoBehaviour
{
    [SerializeField]
    float moveSpeedZ = -0.5f;// 動く速さ

    [SerializeField]
    float lifeTime = 20;// 弾を時間で管理

    [SerializeField]
    int enemyHP_ = 5;// 敵の体力

    [SerializeField]
    GameObject particleObject;// 消滅パーティクル

    [SerializeField]
    GameObject SetAitemObj; // 発射する敵のオブジェクト


    float move_randX ,move_randY;



    // Start is called before the first frame update
    void Start()
    {

        move_randX = Random.Range(-0.03f, 0.03f);
        move_randY = Random.Range(-0.03f, 0.03f);

    }

    // Update is called once per frame
    void Update()
    {

        Vector3 pos = transform.position;
        pos.x += move_randX;
        pos.y += move_randY;
        pos.z += moveSpeedZ;

        transform.position = pos;

        //体力が0になったら消える
        if (enemyHP_ <= 0)
        {
            // 弾を生成 座標を合わせる
            Instantiate(SetAitemObj, new Vector3(transform.position.x,
                transform.position.y, transform.position.z), Quaternion.identity);
            Instantiate(particleObject, this.transform.position, Quaternion.identity); //パーティクル用ゲームオブジェクト生成
            Destroy(gameObject);
        }

        


        //時間経過で消滅
        lifeTime -= Time.deltaTime;

        if (lifeTime < 0f)
        {
            Destroy(gameObject);
            
            return;
        }


    }


    public void UpEnemydamage()// ダメージを受けた時
    {
        enemyHP_ -= 5;
    }




    private void OnTriggerEnter(Collider other)
    {

        ////障害物（Obstacle）にぶつかったら消える
        //if (other.gameObject.tag == "Obstacle")
        //{
        //    Destroy(gameObject);
        //}

    }

}
