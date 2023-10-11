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

    //1)어떤 몬스터가 있다고 가정하자.특정 캐릭터를 선정한 후 적절한 변수를 설정한다
    //2)두 물체가 충돌하여 일어날 수 있는 시나리오를 선정한 후 공격이나 방어에 관한 사나리오를 작성한다,
    //3) 이 가운데 코루틴이 포함된 알고리즘을 구현 하시오.(이 과정에서 공격이나 방어과정 하나만 구현해도 무방합니다.)
}
