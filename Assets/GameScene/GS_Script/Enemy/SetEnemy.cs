using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetEnemy : MonoBehaviour
{
    //public Transform SetEnemyTransform; 

    GameObject[] target; // �W�I�̍��W���擾

    public GameObject SetEnemyObj; // ���˂���G�̃I�u�W�F�N�g

    public Vector3 setPos;// ���ˈʒu

    public float interval = 3.0f; // ���b�Ԋu�Ō���
    private float timer = 0.0f; // ���ԃJ�E���g�p�̃^�C�}�[

    bool attackFlag = false; // �I�u�W�F�N�g�����t���O

    public int shootmode = 1; //�P�� 1, ���� 2

    void Start()
    {

        

    }
    // Update is called once per frame
    void Update()
    {
        // �^�O���� transform ���擾
        target = GameObject.FindGameObjectsWithTag("Player"); 
                                                              

        foreach (GameObject player in target)
        {
            // �v���C���[�Ƃ̋�����1000���߂��ɂ����琶���J�n
            if (player.transform.position.z+1500 > transform.position.z )
            {
                attackFlag = true;
            }
            // �v���C���[��ʂ�߂����琶�����~
            if(player.transform.position.z+300>transform.position.z)
            {
                attackFlag = false;
            }
           
        }

        if (timer <= 0.0f && attackFlag)// 
        {
            if (shootmode == 1)
            {
                // �e�𐶐� ���W�����킹��
                Instantiate(SetEnemyObj, new Vector3(transform.position.x,
                    transform.position.y, transform.position.z), Quaternion.identity);
                timer = 100; // �Ԋu���Z�b�g
            }



            if (shootmode == 2)
            {
                // �e�𐶐� ���W�����킹��
                Instantiate(SetEnemyObj, new Vector3(transform.position.x,
                    transform.position.y, transform.position.z), Quaternion.identity);
                timer = interval; // �Ԋu���Z�b�g
            }
        }


       


        // �^�C�}�[�̒l�����炷
        if (timer > 0.0f)
        {
            timer -= Time.deltaTime;
        }

    }

}
