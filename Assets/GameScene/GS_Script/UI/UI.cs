using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UI : MonoBehaviour
{

    public GameObject score_object = null;//スコアテキスト
    public int score_num = 0; // スコア変数

    public GameObject playerhp_object = null;//プレイヤーのテキスト
    public int playerhp_num = 10;//プレイヤーの体力

    [SerializeField]
    float damageTimer = 0f;
    bool damageFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (damageFlag)
        {
            damageTimer += Time.deltaTime;
        }
    
        if (damageTimer > 2f)
        {
            damageTimer = 0f;
            damageFlag = false;
        }

        Text Score_text = score_object.GetComponent<Text>();
        Score_text.text = "SCORE   "+score_num;

        Text PlayerHp_text = playerhp_object.GetComponent<Text>();
        PlayerHp_text.text = "HP  "+playerhp_num;

        if (playerhp_num <= 0)
        {
            SceneManager.LoadScene("Result");
        }

    }

    public void ScoreUp()
    {
        score_num += 100;
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
