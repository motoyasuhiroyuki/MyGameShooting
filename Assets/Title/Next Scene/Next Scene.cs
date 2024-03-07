using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextScene : MonoBehaviour
{

    public GameObject Main;
    public GameObject Main2;
    public GameObject Sub;
    public GameObject Sub2;

    // アニメーター
    public Animator m_AnimatorParticle = null;
    public Animator m_AnimatorCannon = null;

    public Image titleUI;
    public Image frameUI;
    public Image startUI;
    public Image ruleUI;
    public Image operatorUI;
    public Image operator_;

    int textFlag_ = 0;
    bool animeFlag_ = false;

    public float AnimeCount = 130.0f;
    float AnimeCountTimer = 0.0f;
    bool AnimeCountFlag = false;

    int menuFlag_ = 1;
    float StickFlag_ = 0f;
    float StickTimer_ = 0f;

    // Start is called before the first frame update
    void Start()
    {

        AnimeCountTimer = 0.0f;
        AnimeCount = 130.0f;
        AnimeCountFlag = false;

        Main = GameObject.Find("Main");
        Main.SetActive(false);

        Main2 = GameObject.Find("Main2");
        Main2.SetActive(false);

        Sub = GameObject.Find("Sub");
        Sub.SetActive(false);

        Sub2 = GameObject.Find("Sub2");
        Sub2.SetActive(false);

        m_AnimatorParticle = GetComponent<Animator>();
        m_AnimatorCannon = GetComponent<Animator>();

        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(textFlag_);
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
        

        if (lsv == 0)
        {
            StickTimer_ = 0f;
            StickFlag_ = 0;
        }

        if (lsv > 0)
        {
            StickFlag_ = 1;
        }

        if (StickFlag_ == 1)
        {
            StickTimer_++;
        }

        if (StickTimer_ >= 38)
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
            StickTimer_--;
        }

        if (StickTimer_ <= -38)
        {
            textFlag_ += 1;
            StickTimer_ = 0f;
        }

        //ゲームを始める
        if (textFlag_ == 0)
        {
            frameUI.transform.position = new Vector3(startUI.transform.position.x, startUI.transform.position.y, startUI.transform.position.z);
            if (Input.GetKey(KeyCode.Space) || Input.GetKey("joystick button 2") && menuFlag_!=2)
            {
                titleUI.enabled = false;
                startUI.enabled = false;
                ruleUI.enabled = false;
                operatorUI.enabled = false;
                frameUI.enabled = false;
                Main.SetActive(true);
                Main2.SetActive(true);
                Sub.SetActive(true);
                Sub2.SetActive(true);

                m_AnimatorParticle.SetTrigger("StartFlag");
                m_AnimatorCannon.SetTrigger("CannonFlag");
                menuFlag_ = 0;   
            }
        }
        //ルール説明
        if (textFlag_ == 1)
        {
            frameUI.transform.position = new Vector3(ruleUI.transform.position.x, ruleUI.transform.position.y, ruleUI.transform.position.z);
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKey("joystick button 2"))
            {

            }
        }
        //操作説明
        if (textFlag_ == 2)
        {
            frameUI.transform.position = new Vector3(operatorUI.transform.position.x, operatorUI.transform.position.y, operatorUI.transform.position.z);
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKey("joystick button 2"))
            {
                menuFlag_ = 2;
            }
        }

        if (Input.GetKeyDown("joystick button 7") || Input.GetKeyDown(KeyCode.M))
        { 
            menuFlag_ = 1;
        }

        if(menuFlag_ == 1)
        {
            titleUI.enabled = true;
            startUI.enabled = true;
            ruleUI.enabled = true;
            operatorUI.enabled = true;
            frameUI.enabled = true;
        }
        else
        {
            titleUI.enabled = false;
            startUI.enabled = false;
            ruleUI.enabled = false;
            operatorUI.enabled = false;
            frameUI.enabled = false;
        }

        if (menuFlag_==2)
        {
            operator_.enabled = true;
        }
        else
        {
            operator_.enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
                Application.Quit();//ゲームプレイ終了
#endif
        }


        if (Input.GetKey(KeyCode.Space) || Input.GetKey("joystick button 2") && textFlag_==0)
        {
            AnimeCountFlag = true;
        }


        if (AnimeCountFlag)
        {
            AnimeCountTimer += 0.1f;
        }

        Vector3 Pos = transform.position;

        if (Pos.z>=300)
        {
            //SceneBをロード。現在のシーンは自動的に削除されて、シーンBだけになる
            SceneManager.LoadScene("GameScene");
        }

    }    
}
