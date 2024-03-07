using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//using static UnityEditor.PlayerSettings;

public class UpEnemy : MonoBehaviour
{
    [SerializeField]
    float moveSpeedZ = -0.5f;// ��������

    [SerializeField]
    float lifeTime = 20f;// �e�����ԂŊǗ�

    [SerializeField]
    float enemyHP_ = 5f;// �G�̗̑�

    [SerializeField]
    GameObject particleObject;// ���Ńp�[�e�B�N��

    [SerializeField]
    GameObject SetAitemObj; // ���˂���G�̃I�u�W�F�N�g

    [SerializeField]
    Vector3 enemySpeed = new Vector3(0f, 0f, -2.0f); //�e�̑���



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
        
        //�̗͂�0�ɂȂ����������
        if (enemyHP_ <= 0f)
        {
            // �e�𐶐� ���W�����킹��
            Instantiate(SetAitemObj, new Vector3(transform.position.x,
                transform.position.y, transform.position.z), Quaternion.identity);
            Instantiate(particleObject, this.transform.position, Quaternion.identity); //�p�[�e�B�N���p�Q�[���I�u�W�F�N�g����
            Destroy(gameObject);
        }

        


        //���Ԍo�߂ŏ���
        lifeTime -= Time.deltaTime;

        if (lifeTime < 0f)
        {
            Destroy(gameObject);
            
            return;
        }


    }


    public void UpEnemydamage_Normal()// �_���[�W���󂯂���
    {
        enemyHP_ -= 2.5f;
    }

    public void UpEnemydamage_Horming()// �_���[�W���󂯂���
    {
        enemyHP_ -= 1f;
    }


    private void OnTriggerEnter(Collider other)
    {

        ////��Q���iObstacle�j�ɂԂ������������
        //if (other.gameObject.tag == "Obstacle")
        //{
        //    Destroy(gameObject);
        //}

    }

}
