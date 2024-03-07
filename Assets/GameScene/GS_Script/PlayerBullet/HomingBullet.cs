using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class HomingBullet : MonoBehaviour
{
  
    
    GameObject[] target; //　標的の座標を取得
    GameObject closeEnemy;

    [SerializeField, Min(0)]
    float time = 1;
    [SerializeField]
    float lifeTime = 2;// 弾を時間で管理
    [SerializeField]
    bool limitAcceleration = false; //加速度の制限
    [SerializeField, Min(0)]
    float maxAcceleration = 100;

    Vector3 position;
    Vector3 velocity;
    Vector3 acceleration;
    Vector3 targetPos;
    Transform thisTransform;

    float bulletRange_ = 1000.0f;
    float bulllet_enemy_Range = 0.0f;

    [SerializeField]
    float tagFlag = 1;

    int delete_time = 600;

    void Start()
    {


        if (tagFlag == 1)
        {
            target = GameObject.FindGameObjectsWithTag("HormingEnemy"); // タグから transform を取得
        }
        if (tagFlag == 2)
        {
            target = GameObject.FindGameObjectsWithTag("UpEnemy"); // タグから transform を取得
        }


        foreach (GameObject enemy in target)
        {

            // 「初期値」の設定
            float closeDist = 500;
            
            // このオブジェクト（砲弾）と敵までの距離を計測
            float tDist = Vector3.Distance(transform.position, enemy.transform.position);


            // もしも「初期値」よりも「計測した敵までの距離」の方が近いならば、
            if (closeDist > tDist)
            {
                // 「closeDist」を「tDist（その敵までの距離）」に置き換える。
                // これを繰り返すことで、一番近い敵を見つけ出すことができる。
                closeDist = tDist;

                // 一番近い敵の情報をcloseEnemyという変数に格納する（★）
                closeEnemy = enemy;


            }
        }
        
        thisTransform = transform; 
        position = thisTransform.position;
    }

    public void Update()
    {
        delete_time--;
        if (delete_time <= 0)
        {
            Destroy(gameObject);
        }

        if (closeEnemy == null)
        {
            return;
        }

        acceleration = 2f / (time * time) * (closeEnemy.transform.position - position - time * velocity);

        if (limitAcceleration && acceleration.sqrMagnitude > maxAcceleration * maxAcceleration)
        {
            acceleration = acceleration.normalized * maxAcceleration;
        }
        
        velocity += acceleration * Time.deltaTime;
        position += velocity * Time.deltaTime;
        thisTransform.position = position;
        thisTransform.rotation = Quaternion.LookRotation(velocity);


        //時間経過で消滅
        time -= Time.deltaTime;

        if (time < 0f)
        {
            Destroy(gameObject);
            return;
        }

    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(lifeTime);

        Destroy(gameObject); //○○秒経ったら消す
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "HormingEnemy")
        {
            other.GetComponent<HormingEnemy>().Enemydamage_Horming();
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "UpEnemy")
        {
            other.GetComponent<UpEnemy>().UpEnemydamage_Horming();
            Destroy(gameObject);
        }
    }
}
