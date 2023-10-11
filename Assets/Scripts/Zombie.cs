using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Zombie : LivingEntity
{
    public LayerMask TargetLayer;

   public ZombieData zombieData;
    private NavMeshAgent navmeshagent;
    private LivingEntity targetEntity;

    public ParticleSystem hitSystem;
    public AudioClip deathSound;
    public AudioClip hitSound;

    private Animator zombieAni;
    private AudioSource zombieSound;
    private Renderer zombieRenderer;

    public float zombieDamage = 20f;
    public float timeBetAttack = 0.5f;
    private float lastAttackTime;

    private bool hasTarget
    {
        get
        {
            if(targetEntity != null && !targetEntity.dead)
            {
                return true;
            }
            return false;
        }
    }
    private void Awake()
    {
     
        navmeshagent = GetComponent<NavMeshAgent>();
        zombieAni = GetComponent<Animator>();
        zombieSound = GetComponent<AudioSource>();

        zombieRenderer = GetComponentInChildren<Renderer>();
    }

    // ���� AI�ʱ⽺���� �����ϴ� �¾� �޼ҵ�
    public void Setup(ZombieData zombieData)
    {
        startingHealth = zombieData.health;
        Debug.Log(zombieData.health);
        // ü�¼���
        health  = zombieData.health;
        // ���ݷ¼���
        zombieDamage = zombieData.damage;
        //����Ž� �̵��ӵ� ����
        navmeshagent.speed = zombieData.speed;
        //���׸����� ���� �ǵ���� ������ ���ϰ�
        zombieRenderer.material.color = zombieData.skinColor;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(UpdatePath());
    }

    // Update is called once per frame
    void Update()
    {
        zombieAni.SetBool("HasTarget", hasTarget);
    }

    private IEnumerator UpdatePath()
    {
        while(!dead)
        {
            if(hasTarget)
            {
                navmeshagent.isStopped = false;
                navmeshagent.SetDestination(targetEntity.transform.position);
            }
            else
            {
                navmeshagent.isStopped = true;
                // 20������ �������� ���� ������ ���� �׷��� �� ���� ��ġ�� ��� �ݶ��̴��� ������
                // ��, Target ���̾ ���� �ݶ��̴��� ���������� ����
                Collider[] colliders = Physics.OverlapSphere(transform.position, 20f, TargetLayer);
                for(int i = 0; i<colliders.Length; i++)
                {
                    LivingEntity livingEntity = colliders[i].GetComponent<LivingEntity>();
                    if(livingEntity != null && !livingEntity.dead)
                    {
                        targetEntity = livingEntity;
                        break;
                    }
                }
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        if(!dead)
        {
            hitSystem.transform.position = hitPoint;
            hitSystem.transform.rotation = Quaternion.LookRotation(hitNormal);
            hitSystem.Play();

            zombieSound.PlayOneShot(hitSound);
         }
        base.OnDamage(damage, hitPoint, hitNormal);
    }

    public override void Die()
    {
        base.Die();

        Collider[] zombieColliders = GetComponents<Collider>();

        for(int i = 0; i<zombieColliders.Length; i++)
        {
            zombieColliders[i].enabled = false;
        }

        navmeshagent.isStopped = true;
        navmeshagent.enabled = false;

        zombieAni.SetTrigger("Die");
        zombieSound.PlayOneShot(deathSound);
       //  UIManager.instance.UpdateScoreText(zombieData.score);
    }

    private void OnTriggerStay(Collider other)
    {
        if(!dead && Time.time >= lastAttackTime + timeBetAttack)
        {
            LivingEntity attackTarget = other.GetComponent<LivingEntity>();
            if(attackTarget != null && attackTarget == targetEntity)
            {
                lastAttackTime = Time.time;

                Vector3 hitPoint = other.ClosestPoint(transform.position);
                Vector3 hitNormal = transform.position - other.transform.position;

                attackTarget.OnDamage(zombieDamage, hitPoint, hitNormal);
            }
        }
    }
}
