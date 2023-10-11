using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Monster : MonoBehaviour
{
    public GameObject enemyPrefab;

    public class EnemyParams
    {
        public int hp = 50;
        public int att = 10;
        public bool IsDead = false;
    }

    public class PlayerParams
    {
        public int hp = 100;
        public int att = 25;
        public bool IsDead = false;
    }

    EnemyParams enemyParams;
    PlayerParams playerParams;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(EnemyDie());
    }

    IEnumerator EnemyDie()
    {
        if (enemyParams.hp == 0)
        {
            enemyParams.IsDead = true;
            yield return new WaitForSeconds(2.0f);
            enemyPrefab.SetActive(false);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Player")
        {
            playerParams.hp -= enemyParams.att;
            if (playerParams.hp == 0)
            {
                Destroy(collision.gameObject, 2f);
            }
        }
    }

    //1)� ���Ͱ� �ִٰ� ��������.Ư�� ĳ���͸� ������ �� ������ ������ �����Ѵ�
    //2)�� ��ü�� �浹�Ͽ� �Ͼ �� �ִ� �ó������� ������ �� �����̳� �� ���� �糪������ �ۼ��Ѵ�,
    //3) �� ��� �ڷ�ƾ�� ���Ե� �˰����� ���� �Ͻÿ�.(�� �������� �����̳� ������ �ϳ��� �����ص� �����մϴ�.)
}
