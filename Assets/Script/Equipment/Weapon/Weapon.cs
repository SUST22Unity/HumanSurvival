using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using System;
using System.Linq;

public class Weapon : MonoBehaviour
{
    //������ ���� ����
    //���ø� ���� ���� �������� ����
    public int WeaponIndex;
    public int WeaponLevel;
    public int WeaponMaxLevel;
    public bool Mastered = false;

    private float totalDamage;
    private float totalProjectileSpeed;
    private float totalDuration;
    private float totalAttackRange;
    private float totalCooldown;
    private int totalNumberOfProjectiles;
    private float totalspeed;

    private Vector3 direction;

    public float[] WeaponStats;
    public float[] WeaponTotalStats;

    public EquipmentData EquipmentData;
    public PoolManager pool;
    private void Update()
    {
        transform.position = transform.position + direction * totalspeed * Time.deltaTime;
    }
    public void Shoot(float speed, Vector3 direct)
    {
        totalspeed = speed;
        direction = direct;
    }
    public void WeaponSetting(int weaponIndex=0)
    {
        this.WeaponIndex = weaponIndex;
        this.WeaponStats = Enumerable.Range(0, EquipmentData.defaultWeaponStats.GetLength(1)).Select(x => EquipmentData.defaultWeaponStats[weaponIndex, x]).ToArray();

        WeaponLevel = 1;
        WeaponMaxLevel = (int)WeaponStats[(int)Enums.WeaponStat.MaxLevel];
        WeaponTotalStats = WeaponStats;
    }

    public void Upgrade()
    {
        WeaponLevel++;
        foreach ((var statIndex, var data) in EquipmentData.WeaponUpgrade[WeaponIndex][WeaponLevel])
        {
            WeaponStats[statIndex] += data;
        }
    }
    public bool IsMaster()
    {
        return WeaponLevel == WeaponMaxLevel;
    }
    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("DestructibleObj"))
        {
            if (col.gameObject.TryGetComponent(out DestructibleObject destructible))
            {
                Debug.Log("�繰 hit");
                destructible.TakeDamage(totalDamage);

            }
        }
    }
    /*���⸶�� �ൿ
    public void WhichWeapon()
    {
        switch (WeaponIndex)
        {
            case 0:     // Whip
                break;
            case 1:     // MagicWand
                break;
            case 2:     // Knife
                break;
            case 3:     // Axe
                break;
            case 4:     // Cross
                break;
            case 5:     //KingBible
                break;
            case 6:     // FireWand
                break;
            case 7:     // Garlic
                break;
            case 8:     // SantaWater
                break;
            case 9:     // Peachone
                break;
            case 10:    // EbonyWings
                break;
            case 11:    // Runetracer
                break;
            case 12:   // LightningRing
                break;
        }
    }
    */
    //�Ʒ� ����� �ѹ��� �ϱ�
    //������ �Ҷ����� �����ϴ� ������ ����
    private void AttackCalculation()
    {
        DamageCalculation();
        ProjectileSpeedCalculation();
        DurationCalculation();
        AttackRangeCalculation();
        CooldownCalculation();
        CalculateNumberOfProjectiles();
    }
    private void DamageCalculation()
    {
        WeaponTotalStats[((int)Enums.WeaponStat.Might)] = WeaponStats[((int)Enums.WeaponStat.Might)] * (1 + GameManager.instance.player.GetComponent<Character>().Damage / 100);
    }
    private void ProjectileSpeedCalculation()
    {
        WeaponTotalStats[((int)Enums.WeaponStat.ProjectileSpeed)] = WeaponStats[((int)Enums.WeaponStat.ProjectileSpeed)] * (1 + GameManager.instance.player.GetComponent<Character>().ProjectileSpeed / 100);
    }
    private void DurationCalculation()
    {
        WeaponTotalStats[((int)Enums.WeaponStat.Duration)] = WeaponStats[((int)Enums.WeaponStat.Duration)] * (1 + GameManager.instance.player.GetComponent<Character>().Duration / 100);
    }
    private void AttackRangeCalculation()
    {
        WeaponTotalStats[((int)Enums.WeaponStat.Area)] = WeaponStats[((int)Enums.WeaponStat.Area)] * (1 + GameManager.instance.player.GetComponent<Character>().AttackRange / 100);
    }
    private void CooldownCalculation()
    {
        WeaponTotalStats[((int)Enums.WeaponStat.Cooldown)] = WeaponStats[((int)Enums.WeaponStat.Cooldown)] * (1 + GameManager.instance.player.GetComponent<Character>().Cooldown / 100);
    }
    private void CalculateNumberOfProjectiles()
    {
        WeaponTotalStats[((int)Enums.WeaponStat.Amount)] = ((int)WeaponStats[((int)Enums.WeaponStat.Amount)]) + GameManager.instance.player.GetComponent<Character>().NumberOfProjectiles;
    }
}
