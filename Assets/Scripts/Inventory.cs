using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;

public class WeaponUnityEvent : UnityEvent<Weapon> { }

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    [SerializeField] private InventoryView inventoryView = null;

    private List<Weapon> weapons = new List<Weapon>();
    private InputControls inputControls;

    public int activeWeaponId { get; protected set; } = 0;

    public IEnumerable<Weapon> Weapons => weapons;


    [HideInInspector] public UnityEvent weaponsKitChanged = new UnityEvent();
    [HideInInspector] public IntUnityEvent activeWeaponIdChanged = new IntUnityEvent();
    [HideInInspector] public WeaponUnityEvent activeWeaponChanged = new WeaponUnityEvent();


    private void Awake()
    {
        instance = this;
        inputControls = new InputControls();
    }

    private void OnEnable()
    {
        inputControls.Enable();
    }

    private void OnDisable()
    {
        inputControls.Disable();
    }

    public void Start()
    {
        inputControls.UI.Q.performed += context => ChangeActiveWeapon(-1);
        inputControls.UI.E.performed += context => ChangeActiveWeapon(1);

        activeWeaponChanged.AddListener(TankWeapon.instance.SetWeapon);
        weaponsKitChanged.AddListener(inventoryView.CreateInventorySlots);
        activeWeaponIdChanged.AddListener(inventoryView.ChangeActiveWeapon);

        SetInventory();
    }

    private void SetInventory()
    {
        Type weaponType = typeof(Weapon); 
        IEnumerable<Type> childTypes = Assembly.GetAssembly(weaponType).GetTypes().Where(type => type.IsSubclassOf(weaponType));
        foreach (Type t in childTypes)
        {
            Weapon instance = (Weapon)Activator.CreateInstance(t);
            weapons.Add(instance);
        }

        activeWeaponId = 0;

        weaponsKitChanged?.Invoke();

        activeWeaponIdChanged?.Invoke(activeWeaponId);

        activeWeaponChanged?.Invoke(
            weapons.Find(weapon => weapon.id == activeWeaponId));
    }

    private void ChangeActiveWeapon(int step)
    {
        activeWeaponId += step;

        var activeWeapon = weapons.Find(weapon => weapon.id == activeWeaponId);

        if (activeWeapon == null && step < 0)
        {
            activeWeaponId = weapons[weapons.Count - 1].id;
        }
        if (activeWeapon == null && step > 0)
        {
            activeWeaponId = weapons[0].id;
        }

        activeWeaponIdChanged?.Invoke(activeWeaponId);

        activeWeaponChanged?.Invoke(
            weapons.Find(weapon => weapon.id == activeWeaponId));
    }

}
