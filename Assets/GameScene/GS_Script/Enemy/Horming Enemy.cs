using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HormingEnemy : MonoBehaviour
{

    float timer = 0;

    public Transform player;
    GameObject[] target; //�@�W�I�̍��W���擾
    //GameObject closePlayer;
    public GameObject SetAitemObj; // �A�C�e��

    public float moveSpeed = 60.0f;
    float enemyHP_ = 10.0f;

    [SerializeField, Min(0)]
    float time = 10;
    [SerializeField]
    float lifeTime = 10;// �e�����ԂŊǗ�

    public GameObject particleObject;

   
    bool attackFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        

        

       


    }


    // Update is called once per frame
    void Update()
    {


        target = GameObject.FindGameObjectsWithTag("Player"); // �^�O���� transform ���擾


        foreach (GameObject player in target)
        {

            if (player.transform.position.z + 1500 > transform.position.z)
            {
                attackFlag = true;
            }
            // �v���C���[��ʂ�߂����琶�����~
            if (player.transform.position.z + 300 > transform.position.z)
            {
                attackFlag = false;
            }


            if (player.transform.position.z < gameObject.transform.position.z)
            {
                if (attackFlag)
                {
                    transform.LookAt(player.transform);
                    GetComponent<Rigidbody>().velocity = transform.forward.normalized * moveSpeed;
                }
            }
            else
            {
                
                Destroy(gameObject);
            }

        }


        //���Ԍo�߂ŏ���
        time -= Time.deltaTime;

        if (time < 0f)
        {
           
            Destroy(gameObject);
            return;
        }

       

        //���ŏ����[�[�[�[�[�[�[�[�[

        //�̗͂�0�ɂȂ����������
        if (enemyHP_ <= 0)
        {
            // �e�𐶐� ���W�����킹��
            Instantiate(SetAitemObj, new Vector3(transform.position.x,
                transform.position.y, transform.position.z), Quaternion.identity);
            Instantiate(particleObject, this.transform.position, Quaternion.identity); //�p�[�e�B�N���p�Q�[���I�u�W�F�N�g����
            Destroy(gameObject);
        }

        target = GameObject.FindGameObjectsWithTag("Player"); // �^�O���� transform ���擾


        foreach (GameObject player in target)
        {

            if (player.transform.position.z >gameObject.transform.position.z)
            {
               
                Destroy(gameObject);
            }                       
        }
    }


    public void Enemydamage_Normal()//�_���[�W���󂯂���
    {
        enemyHP_ = enemyHP_ - 2.5f;
    }

    public void Enemydamage_Horming()//�_���[�W���󂯂���
    {
        enemyHP_ = enemyHP_ - 1.0f;
    }

   

}
