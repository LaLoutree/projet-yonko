using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public string Name;
    [SerializeField] public int Damage;
    [SerializeField] public int Range;
    public bool NeedAmo;
    [SerializeField] public int Amo;
    [SerializeField] public int Id;

    Weapon(string name, int id)
    {
        Name = name;
        Id = id;
        NeedAmo = true;
        switch(name)
        {
            case "Knife":
                Damage = 20;
                Range = 1;
                Amo = 1;
                NeedAmo = false;
                break;
            case "Glock":
                Damage = 10;
                Range = 100;
                Amo = 100;     
                break;
            case "Magnum":
                Damage = 10;
                Range = 100;
                Amo = 100;
                break;
            case "Pompe":
                Damage = 10;
                Range = 100;
                Amo = 100;
                break;
            case "AK47":
                Damage = 10;
                Range = 100;
                Amo = 100;
                break;
            case "Battesuse":
                Damage = 10;
                Range = 100;
                Amo = 100;
                break;
        }
    }
    void useamo()
    {
        if (NeedAmo) Amo -= 1;
    }
    void shoot()
    {
        //Crée un projectile qui disparait aprés Range distance
    }
}
