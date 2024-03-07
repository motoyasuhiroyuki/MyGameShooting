using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SetBullet : MonoBehaviour
{


   
    public Transform setPlayer; // 自機のワールド座標
    public GameObject myBullet;

    public Vector3 setPos;

    public float interval = 0.1f; // 何秒間隔で撃つか
    private float timer = 0.0f; // 時間カウント用のタイマー

    
    void Start()
    {
       
    }
    // Update is called once per frame
    void Update()
    {



        transform.position = new Vector3 (setPlayer.position.x + setPos.x, 
            setPlayer.position.y + setPos.y, setPlayer.position.z + setPos.z);
        

        if (Input.GetKey(KeyCode.Z) && timer <= 0.0f && Time.timeScale == 1.0f)// Z を押したら
        {
           
            //弾を生成し　座標をプレイヤーに合わせる
            Instantiate(myBullet, new Vector3(transform.position.x,
                transform.position.y, transform.position.z+2), Quaternion.identity);

            timer = interval; // 間隔をセット
        }
        //　Rボタン＝５　Lボタン＝４
        if ( timer <= 0.0f && Input.GetKey("joystick button 5"))// Z を押したら
        {

            //弾を生成し　座標をプレイヤーに合わせる
            Instantiate(myBullet, new Vector3(transform.position.x,
                transform.position.y, transform.position.z + 2), Quaternion.identity);

            timer = interval; // 間隔をセット
        }


        // タイマーの値を減らす
        if (timer > 0.0f)
        {
            timer -= Time.deltaTime;
        }

    }
   
}
