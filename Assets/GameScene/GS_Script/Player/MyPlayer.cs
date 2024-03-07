using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Threading;

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
    float moveSpeed = 40.0f;   // 移動速度
   
    //x軸方向の入力を保存
    private float _input_x;
    //z軸方向の入力を保存
    private float _input_z;


    private NormalBullet NB;

    int  accelerationFlag = 0;
    float accelerationTimer = 0.0f;

    public static bool isPause = false;
    public float moveSpeedZ = 100f;


    //点滅関連
    [SerializeField] 
    private Renderer playerhide1; //点滅用
    [SerializeField]
    private Renderer playerhide2; //点滅用
    [SerializeField]
    private Renderer playerhide3; //点滅用
    [SerializeField]
    private Renderer playerhide4; //点滅用
    [SerializeField]
    private Renderer playerhide5; //点滅用
    [SerializeField]
    private Renderer playerhide6; //点滅用
    [SerializeField]
    private Renderer playerhide7; //点滅用

    float intervalhideTime = 0.0f;
    float hideTime = 0.2f;
    float hideCount = 0;
    bool hideFlag = false;

    private void Start()
    {
        Application.targetFrameRate = 60;
    }

  


    // Update is called once per frame
    void Update()
    {

        

        if (hideFlag)
        {
            intervalhideTime += Time.deltaTime;
            if(intervalhideTime > 0.1f) { //点滅開始
                hideTime += Time.deltaTime;
                if(hideTime >= 0.1)
                {
                    hideTime = 0.0f;
                    hideCount+=1;
                    playerhide1.enabled = !playerhide1.enabled;
                    playerhide2.enabled = !playerhide2.enabled;
                    playerhide3.enabled = !playerhide3.enabled;
                    playerhide4.enabled = !playerhide4.enabled;
                    playerhide5.enabled = !playerhide5.enabled;
                    playerhide6.enabled = !playerhide6.enabled;
                    playerhide7.enabled = !playerhide7.enabled;
                }
                if (hideCount == 8f)
                {
                    hideFlag = false;
                    intervalhideTime = 0;
                    hideTime = 0.0f;
                    hideCount = 0f;
                }
                
            }


        }


        Rota(); // ローテーション 関数

        Move(); // ポジション 関数

    }
   
    void Move()// 移動処理
    {

        //L Stick
        float lsh = Input.GetAxis("L_Stick_H");
        float lsv = Input.GetAxis("L_Stick_V");

        //R Stick
        float rsh = Input.GetAxis("R_Stick_H");
        float rsv = Input.GetAxis("R_Stick_V");





        if (Input.GetKey(KeyCode.UpArrow) || lsv > 0)
        {
            transform.position += new Vector3(0, moveSpeed, 0) * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.DownArrow) || lsv < 0)
        {
            transform.position += new Vector3(0,-moveSpeed, 0) * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.LeftArrow) || lsh > 0 )// 左
        {
            transform.position += new Vector3(-moveSpeed , 0,0) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow) || lsh < 0 )// 右
        {
            transform.position += new Vector3(moveSpeed , 0,0) * Time.deltaTime;
        }

        if (accelerationFlag == 0)
        {
            moveSpeedZ = 100f;
            transform.position += new Vector3(0, 0, moveSpeedZ) * Time.deltaTime;
        }
        //加速フラグ

        if (accelerationFlag == 1)
        {

            if (moveSpeedZ <= 160.0f)
            {
                moveSpeedZ += 0.5f;//加速
                transform.position += new Vector3(0, 0, moveSpeedZ) * Time.deltaTime;
            }
            else
            {
                accelerationFlag = 2;
            }
        }

        if (accelerationFlag == 2)
        {
           
            if (moveSpeedZ >= 80.0f)
            {
                moveSpeedZ -= 0.5f;
                transform.position += new Vector3(0, 0, moveSpeedZ) * Time.deltaTime;
            }
            if (moveSpeedZ <= 80f)
            {
                accelerationFlag = 0;
            }

        }


        //移動範囲制限

        if (transform.position.y >= 250)//上
        {
            transform.position += new Vector3(0, -moveSpeed, 0) * Time.deltaTime;
        }
        if (transform.position.y <= -100)//下
        {
            transform.position += new Vector3(0, moveSpeed, 0) * Time.deltaTime;
        }
        

        //座標超えたらゴール

        if (transform.position.z >= 1800)
        {
            SceneManager.LoadScene("Result");
        }



        mainCamera.transform.position = new Vector3(transform.position.x+xAdjust,transform.position.y+yAdjust,transform.position.z+zAdjust);




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
                transform.Rotate(new Vector3(0, 0, rotaSpeed*2) * Time.deltaTime);
                moveSpeed = 120f;
            }
            else
            {
                // ○○秒を超えたら初期化する
                moveSpeed = 40f;
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
            //// 回転角度をオブジェクトに適用
            //transform.rotation = Quaternion.Euler(0, 0, angleZ);


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
                transform.eulerAngles += new Vector3(0.0f, 0.0f, 80.0f) * Time.deltaTime;
            }
            else if (rotateZ >= 0.2f)
            {
                transform.eulerAngles -= new Vector3(0.0f, 0.0f, 80.0f) * Time.deltaTime;
            }
        }


       
    }

   

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "acceleItem")
        {
            accelerationFlag = 1;
        }


        if (other.gameObject.tag == "acceleItem" || other.gameObject.tag == "Item"|| 
            other.gameObject.tag == "Bullet")
        {
        }
        else if(!hideFlag)
        {
            hideFlag = true;
        }

    }

    

}


