using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillFiringSystem : MonoBehaviour
{
    public GameObject[] weaponPrefabs; //???? ??????
    public GameObject[] evolutionWeaponPrefabs; //??ȭ ???? ??????
    void Update()
    {
        foreach (var weapon in GameManager.instance.player.GetComponent<Character>().Weapons)
        {
            Attack(weapon);
        }
    }
    private void Attack(Weapon weapon)
    {
        switch (weapon.WeaponIndex)
        {
            case 0:     // Whip
                break;
            case 1:     // MagicWand
                break;
            case 2:     // Knife
                if (!weapon.IsMaster()) {
                    GameManager.instance.player.GetComponent<Character>().Weapons[GameManager.instance.player.GetComponent<Character>().TransWeaponIndex[2]].GetComponent<Knife>().FireKnife(weaponPrefabs[2]);
                }
                else {
                    GameManager.instance.player.GetComponent<Character>().Weapons[GameManager.instance.player.GetComponent<Character>().TransWeaponIndex[2]].GetComponent<Knife>().FireKnife(evolutionWeaponPrefabs[2]);
                }
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
                GameManager.instance.player.GetComponent<Character>().Weapons[GameManager.instance.player.GetComponent<Character>().TransWeaponIndex[7]].GetComponent<Gralic>().SpawnGralic(weaponPrefabs[7]);
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
    
}