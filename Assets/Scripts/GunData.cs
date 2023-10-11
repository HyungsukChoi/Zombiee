using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/GunData", fileName = "Gun Data")]
public class GunData : ScriptableObject
{
    public AudioClip shotClip;
    public AudioClip reloadClip;

    public float damage = 25f;
    public int startAmmoRemain = 100;   //ó�� źâ �뷮
    public int magCapacity = 25;    // źâ�뷮

    public float timeBetFire = 0.12f;   //�߻� ����
    public float reloadTime = 1.8f;     //���ε� �ҿ�ð�
}
