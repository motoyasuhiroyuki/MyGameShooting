using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UI : MonoBehaviour
{

    public GameObject score_object = null;//スコアテキスト
    public static int score_num = 0; // スコア変数

    public GameObject playerhp_object = null;//プレイヤーのテキスト
    public int playerhp_num = 10;//プレイヤーの体力

    [SerializeField]
    float damageTimer = 0f;
    bool damageFlag = false;

    public Slider player_hpbar;
    float maxHp ;

    public Image menu;
    //public Text a;
    //public Text b;
    //public Text c;

    public Image frameUI;
    public Image returnUI;
    public Image endUI;
    public Image operatorUI;

    float menuFlag_ = -1;
    float textFlag_ = 0;
    int operatorFlag_ = 1;
    float StickFlag_ = 0f;
    float StickTimer_ = 0f;
    

    // Start is called before the first frame update
    void Start()
    {
        maxHp = playerhp_num;
        score_num = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (menuFlag_ == 1)
        { 
            if (textFlag_ > 2)
            {
                textFlag_ = 2;
            }
            if (textFlag_ < 0)
            {
                textFlag_ = 0;
            }

            //L Stick
            float lsh = Input.GetAxis("L_Stick_H");
            float lsv = Input.GetAxis("L_Stick_V");

            

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                textFlag_ -= 1;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                textFlag_ += 1;
            }
            if (textFlag_ <= 0)
            {
                textFlag_ = 0;
            }

            if (lsv == 0)
            {
                StickTimer_ = 0f;
                StickFlag_ = 0;
            }

            if(lsv > 0)
            {
                StickFlag_ = 1;
            }

            if(StickFlag_ == 1)
            {
                StickTimer_++;
            }

            if (StickTimer_ >= 8)
            {
                textFlag_ -= 1;
                StickTimer_ = 0f;
            }

            if (lsv < 0)
            {
                StickFlag_ = -1;
            }

            if (StickFlag_ == -1)
            {
                StickTimer_++;
            }

            if (StickTimer_ >= 8)
            {
                textFlag_ += 1;
                StickTimer_ = 0f;
            }

            if (textFlag_ == 0)
            {
                frameUI.transform.position = new Vector3(returnUI.transform.position.x, returnUI.transform.position.y, returnUI.transform.position.z);
                if (Input.GetKeyDown("joystick button 2" )|| Input.GetKeyDown(KeyCode.Space))
                {
                    menuFlag_ = -1;
                }
            }

            if (textFlag_ == 1)
            {
                frameUI.transform.position = new Vector3(endUI.transform.position.x, endUI.transform.position.y, endUI.transform.position.z);
                if (Input.GetKeyDown("joystick button 2") || Input.GetKeyDown(KeyCode.Space))
                {
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
                                Application.Quit();//ゲームプレイ終了
#endif

                    //SceneManager.LoadScene("Title");
                }
            }

            if (textFlag_ == 2)
            {
                frameUI.transform.position = new Vector3(operatorUI.transform.position.x, operatorUI.transform.position.y, operatorUI.transform.position.z);
                if (Input.GetKeyDown("joystick button 2") || Input.GetKeyDown(KeyCode.Space) && menuFlag_ == 1)
                {
                    menuFlag_ = 3;
                }
            }
        }

       

        if (Input.GetKeyDown("joystick button 7") || Input.GetKeyDown(KeyCode.M))
            if (menuFlag_ == -1)
            {
                {
                    menuFlag_ = 0;
                }
            }

        if (Input.GetKeyDown("joystick button 7") || Input.GetKeyDown(KeyCode.M))
            if (menuFlag_ ==  1)
            {
                {
                    menuFlag_ = -1;
                }
            }

        if (menuFlag_ == 0)
        {
            menuFlag_ = 1;
        }

        if (menuFlag_ == 1)
        {
            Time.timeScale = 0.0f;
            menu.enabled = false;
            frameUI.enabled = true;
            endUI.enabled = true;
            returnUI.enabled = true;
            operatorUI.enabled = true;

        }
        else
        {
            frameUI.enabled = false;
            endUI.enabled = false;
            returnUI.enabled = false;
            operatorUI.enabled = false;
        }


        if (menuFlag_ == -1)
        {
            menu.enabled = false;
            Time.timeScale = 1.0f;
        }

        if(menuFlag_ == 3)
        {
            Time.timeScale = 0.0f;
            menu.enabled = true;
            if (Input.GetKeyDown("joystick button 7") || Input.GetKeyDown(KeyCode.M))
            menuFlag_ = 0;
        }

//        if (Input.GetKey(KeyCode.Escape) && menuFlag_ == 1)
//        {
//#if UNITY_EDITOR
//            UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
//#else
//            Application.Quit();//ゲームプレイ終了
//#endif


            //            }

            if (damageFlag)
        {
            damageTimer += Time.deltaTime;
        }
    
        if (damageTimer > 1f)
        {
            damageTimer = 0f;
            damageFlag = false;
        }

        Text Score_text = score_object.GetComponent<Text>();
        Score_text.text = "SCORE   "+score_num;

        Text PlayerHp_text = playerhp_object.GetComponent<Text>();
        PlayerHp_text.text = "HP  "+playerhp_num;

        player_hpbar.value = (float)playerhp_num / (float)maxHp; ;

        if (playerhp_num <= 0)
        {
            SceneManager.LoadScene("Result");
        }

    }

    public void ScoreUp()
    {
        score_num += 100;
    }

    public static int GetScore()
    {
        return score_num;
    }

    public void SubUnittdamage()
    {
        playerhp_num -= 1;
    }

    private void OnTriggerEnter(Collider other)
    {
        

            if (other.gameObject.tag == "HormingEnemy" && !damageFlag)
            {
                damageFlag = true;
                playerhp_num -= 1;
            }
            if (other.gameObject.tag == "UpEnemy" && !damageFlag)
            {
                damageFlag = true;
                playerhp_num -= 1;
            }

            if (other.gameObject.tag == "Obstacle" && !damageFlag)
            {
                damageFlag = true;
                playerhp_num -= 1;
            }
       

            

        


    }




    


}
