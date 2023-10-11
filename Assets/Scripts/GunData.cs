using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/GunData", fileName = "Gun Data")]
public class GunData : ScriptableObject
{
    public AudioClip shotClip;
    public AudioClip reloadClip;

    public float damage = 25f;
    public int startAmmoRemain = 100;   //처음 탄창 용량
    public int magCapacity = 25;    // 탄창용량

    public float timeBetFire = 0.12f;   //발사 간격
    public float reloadTime = 1.8f;     //리로드 소요시간
}
