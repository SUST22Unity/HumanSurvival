using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class EquipmentManagementSystem : MonoBehaviour
{
    public List<Weapon> Weapons;
    public List<Accessory> Accessories;
    public List<int> MasteredWeapons;
    public List<int> MasteredAccessories;
    public SkillFiringSystem skillFiringSystem;
    public int[] TransWeaponIndex; // �ش� index�� weapon�� ���� �������� Weapons�� �� ��° index�� �ִ��� ��ȯ�ϴ� �迭, ���ٸ� -1 ��ȯ
    public int[] TransAccessoryIndex; // ���� ������ Accessory�� �ش�

    void Awake(){ skillFiringSystem = GameObject.Find("SkillFiringSystem").GetComponent<SkillFiringSystem>(); }

    public void ApplyItem(Tuple<int, int, int> pickUp)
    {
        switch ((Enums.PickUpType)pickUp.Item1)
        {
            case Enums.PickUpType.Weapon:
                applyWeapon(pickUp.Item2, pickUp.Item3);
                break;
            case Enums.PickUpType.Accessory:
                applyAccessory(pickUp.Item2, pickUp.Item3);
                break;
            default:
                applyEtc(pickUp.Item2);
                break;
        }
    }
    private void applyWeapon(int weaponIndex, int hasWeapon)
    {
        if (hasWeapon == 0)
            GetWeapon(weaponIndex);
        else
            UpgradeWeapon(weaponIndex);
    }
    private void applyAccessory(int accessoryIndex, int hasAccessory)
    {
        if (hasAccessory == 0)
            GetAccessory(accessoryIndex);
        else
            UpgradeAccessory(accessoryIndex);
    }
    private void applyEtc(int etcIndex)
    {
        switch ((Enums.Etc)etcIndex)
        {
            case Enums.Etc.Food:
                // TODO: ü�� ȸ�� �Լ��� ����
                break;
            case Enums.Etc.Money:
                // TODO: ��ȭ ȹ�� �Լ��� ����
                break;
            default:
                break;
        }
    }
    //ToDo: SkillFiringSystem�̶� ���� �� �Լ�
    public void GetWeapon(int weaponIndex)
    {
        TransWeaponIndex[weaponIndex] = Weapons.Count;
        Weapon newWeapon = (skillFiringSystem.weaponPrefabs[weaponIndex]).GetComponent<Weapon>();
        newWeapon.WeaponSetting(weaponIndex);
        Weapons.Add(newWeapon);
    }
    public void UpgradeWeapon(int weaponIndex)
    {
        Weapons[TransWeaponIndex[weaponIndex]].Upgrade();
        if (Weapons[TransWeaponIndex[weaponIndex]].IsMaster())
        {
            MasteredWeapons.Add(weaponIndex);
        }
    }
    public void GetAccessory(int accessoryIndex)
    {
        TransAccessoryIndex[accessoryIndex] = Accessories.Count;
        Accessories.Add(new Accessory(accessoryIndex));
        UpgradeAccessory(accessoryIndex);
    }
    public void UpgradeAccessory(int accessoryIndex)
    {
        Accessories[TransAccessoryIndex[accessoryIndex]].Upgrade();
        if (Accessories[TransAccessoryIndex[accessoryIndex]].IsMaster())
        {
            MasteredAccessories.Add(accessoryIndex);
        }
    }
}
