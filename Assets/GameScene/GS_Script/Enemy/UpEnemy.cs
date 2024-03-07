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
    float lifeTime = 20f;// 弾を時間で管理

    [SerializeField]
    float enemyHP_ = 5f;// 敵の体力

    [SerializeField]
    GameObject particleObject;// 消滅パーティクル

    [SerializeField]
    GameObject SetAitemObj; // 発射する敵のオブジェクト

    [SerializeField]
    Vector3 enemySpeed = new Vector3(0f, 0f, -2.0f); //弾の速さ



    // Start is called before the first frame update
    void Start()
    {

        enemySpeed.x = Random.Range(-0.03f, 0.03f);
        enemySpeed.y = Random.Range(-0.03f, 0.03f);
       
    }

    // Update is called once per frame
    void Update()
    {
       


        transform.position += new Vector3(enemySpeed.x, enemySpeed.y, enemySpeed.z*60)*Time.deltaTime;
        
        //体力が0になったら消える
        if (enemyHP_ <= 0f)
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


    public void UpEnemydamage_Normal()// ダメージを受けた時
    {
        enemyHP_ -= 2.5f;
    }

    public void UpEnemydamage_Horming()// ダメージを受けた時
    {
        enemyHP_ -= 1f;
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
