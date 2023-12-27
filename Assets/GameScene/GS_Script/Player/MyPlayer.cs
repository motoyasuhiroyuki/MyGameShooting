using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class MyPlayer : MonoBehaviour
{
    //カメラオブジェクト
    public GameObject mainCamera;

    // 軸を調整 (Adjust) 正の数ならプレイヤーの前に (一人称視点)、
    // 負の数ならプレイヤーの後ろに配置する         (三人称視点)、
    public float xAdjust =  0.0f;
    public float yAdjust =  1.0f;
    public float zAdjust = -8.0f;

    /*public*/ float maxAngleX =  0.0f; // 最大回転角度X
    /*public*/ float minAngleX =  0.0f; // 最小回転角度X
    /*public*/ float speedAngleX = 0.0f; // 回転スピードX

    /*public*/ float maxAngleZ =  10.0f;   // 最大回転角度Z
    /*public*/ float minAngleZ = -10.0f;   // 最小回転角度Z
    /*public*/ float speedAngleZ =   0.6f; // 回転スピードZ





    int   rotaKeep = 0;       // フラグを経たせるための変数
    bool  rotaFlag = false;   // 回転フラグ
    float rotaTimer = 0.0f;   // 回転タイマー
    float rotaSpeed = 360.0f; // 回転速度
    float moveSpeed = 0.3f;   // 移動速度

    public float moveSpeedZ = 0.3f;
   

    private NormalBullet NB;




    void Start()
    {
        



    }

    


    // Update is called once per frame
    void Update()
    {

        Rota(); // ローテーション 関数

        Move(); // ポジション 関数

        
        SetCamera(); // プレイヤー座標にカメラ座標を合わせる 関数  

    }



    void Move()// 移動処理
    {
        Vector3 pos = transform.position; // 座標取得



        //L Stick
        float lsh = Input.GetAxis("L_Stick_H");
        float lsv = Input.GetAxis("L_Stick_V");

        //R Stick
        float rsh = Input.GetAxis("R_Stick_H");
        float rsv = Input.GetAxis("R_Stick_V");


        if (Input.GetKey(KeyCode.UpArrow))// 上
        {
            pos.y += moveSpeed;
        }
        if (Input.GetKey(KeyCode.DownArrow))// 下
        {
            pos.y += -moveSpeed;
        }

        if (lsv < 0)// Lスティックを 上 に倒したとき
        {
            pos.y += -moveSpeed;// 上 に移動
        }
        if (lsv > 0)// Lスティックを 下 に倒したとき
        {
            pos.y += moveSpeed;// 下 に移動
        }


      
           
            if (Input.GetKey(KeyCode.LeftArrow))// 左
            {
                pos.x += -moveSpeed;
            }
            if (Input.GetKey(KeyCode.RightArrow))// 右
            {
                pos.x += moveSpeed;
            }
         

            if(lsh < 0)// Lスティックを右に倒したとき
            {
                pos.x += moveSpeed;// 右に移動
            }
            if(lsh > 0)// Lスティックを 左 に倒したとき
            {
                pos.x += -moveSpeed;// 左 に移動
            }


        

       

       

        //プレイヤーの範囲指定

        if(pos.x > 60)//X軸　右
        {
            pos.x = 60;
        }
        if(pos.x <-60)//X軸　左
        {
            pos.x = -60;
        }

        if(pos.y > 130)//Y軸　上
        {
            pos.y = 130;
        }
        if(pos.y < 10)//Y軸　下
        {
            pos.y = 10;
        }



        pos.z += moveSpeedZ;

        if (pos.z <= 1800)
        {
            //カメラはプレイヤーと同じ位置にする
            mainCamera.transform.position = new Vector3(transform.position.x + xAdjust,
                transform.position.y + yAdjust, transform.position.z + zAdjust);
        }

        if (pos.z >= 2300)
        {
            SceneManager.LoadScene("Result");
        }



        transform.position = new Vector3(pos.x, pos.y, pos.z);

        
    }

    void Rota() // 回転処理
    {

        if (Input.GetKeyDown(KeyCode.Space)||Input.GetKeyDown("joystick button 2"))// スペースキーを押したとき
        {
            rotaKeep += 1;// 値が増え
        }

        if (rotaKeep > 0)// 一定の値以上になったら
        {
            rotaFlag = true;// フラグが立つ
        }

        if (rotaFlag)// フラグが立ったら
        {
            rotaTimer += Time.deltaTime;// 秒数を数えて


            if (rotaTimer < 0.5)// ○○秒まで回転させる
            {
                transform.Rotate(new Vector3(0, 0, -rotaSpeed*2) * Time.deltaTime);
                moveSpeed = 0.5f;
            }
            else
            {
                // ○○秒を超えたら初期化する
                moveSpeed = 0.3f;
                rotaKeep = 0;
                rotaTimer = 0;
                rotaFlag = false;
            }
        }

       
        if (!rotaFlag)// フラグがたっていなければ
        {
        //ーーーーーーー上下傾きーーーーーーーーーーー
        // 入力情報
        float turnX = Input.GetAxis("Vertical");
        // 現在の回転角度を0～360から-180～180に変換
        float rotateX = (transform.eulerAngles.x > 180) ? transform.eulerAngles.x - 360 : transform.eulerAngles.x;
        // 現在の回転角度に入力(turn)を加味した回転角度をMathf.Clamp()を使いminAngleからMaxAngle内に収まるようにする
        float angleX = Mathf.Clamp(rotateX + -turnX * speedAngleX, minAngleX, maxAngleX);

        // 回転角度を-180～180から0～360に変換
        angleX = (angleX < 0) ? angleX + 360 : angleX;


        //ーーーーーーー左右傾きーーーーーーーーーーー
        // 入力情報
        float turnZ = Input.GetAxis("Horizontal");
        // 現在の回転角度を0～360から-180～180に変換
        float rotateZ = (transform.eulerAngles.z > 180) ? transform.eulerAngles.z - 360 : transform.eulerAngles.z;
        // 現在の回転角度に入力(turn)を加味した回転角度をMathf.Clamp()を使いminAngleからMaxAngle内に収まるようにする
        float angleZ = Mathf.Clamp(rotateZ + -turnZ * speedAngleZ, minAngleZ, maxAngleZ);

        // 回転角度を-180～180から0～360に変換
        angleZ = (angleZ < 0) ? angleZ + 360 : angleZ;
        // 回転角度をオブジェクトに適用
        transform.rotation = Quaternion.Euler(angleX, 0, angleZ);


        // 角度を初期値に戻す
        
            // 0.2は遊び値
            if (rotateX <= -0.2f)
            {
            transform.eulerAngles += new Vector3(80.0f, 0.0f, 0.0f) * Time.deltaTime;
            }
            else if (rotateX >= 0.2f)
            {
            transform.eulerAngles -= new Vector3(80.0f, 0.0f, 0.0f) * Time.deltaTime;
            }

            // 0.2は遊び値
            if (rotateZ <= -0.2f)
            {
                transform.eulerAngles += new Vector3(0.0f, 0.0f, 40.0f) * Time.deltaTime;
            }
            else if (rotateZ >= 0.2f)
            {
                transform.eulerAngles -= new Vector3(0.0f, 0.0f, 40.0f) * Time.deltaTime;
            }
        }


       
    }

    void SetCamera()// カメラ座標Set
    {

        ////カメラ視点移動（　座標　）角度はカメラの　C#スクリプト　で
        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    zAdjust = -30;
        //    xAdjust = 0;
        //    directionFlag = 1;
        //}

        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    zAdjust = 30;
        //    xAdjust = 0;
        //    directionFlag = 2;

        //}

        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    zAdjust = 0;
        //    xAdjust = 30;
        //    directionFlag = 3;
        //}

        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    zAdjust = 0;
        //    xAdjust = -30;
        //    directionFlag = 4;
        //}

        
        
    }

    



}


