using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class HomingBullet : MonoBehaviour
{
  
    
    GameObject[] target; //�@�W�I�̍��W���擾
    GameObject closeEnemy;

    [SerializeField, Min(0)]
    float time = 1;
    [SerializeField]
    float lifeTime = 2;// �e�����ԂŊǗ�
    [SerializeField]
    bool limitAcceleration = false; //�����x�̐���
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
            target = GameObject.FindGameObjectsWithTag("HormingEnemy"); // �^�O���� transform ���擾
        }
        if (tagFlag == 2)
        {
            target = GameObject.FindGameObjectsWithTag("UpEnemy"); // �^�O���� transform ���擾
        }


        foreach (GameObject enemy in target)
        {

            // �u�����l�v�̐ݒ�
            float closeDist = 500;
            
            // ���̃I�u�W�F�N�g�i�C�e�j�ƓG�܂ł̋������v��
            float tDist = Vector3.Distance(transform.position, enemy.transform.position);


            // �������u�����l�v�����u�v�������G�܂ł̋����v�̕����߂��Ȃ�΁A
            if (closeDist > tDist)
            {
                // �ucloseDist�v���utDist�i���̓G�܂ł̋����j�v�ɒu��������B
                // ������J��Ԃ����ƂŁA��ԋ߂��G�������o�����Ƃ��ł���B
                closeDist = tDist;

                // ��ԋ߂��G�̏���closeEnemy�Ƃ����ϐ��Ɋi�[����i���j
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


        //���Ԍo�߂ŏ���
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

        Destroy(gameObject); //�����b�o���������
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
