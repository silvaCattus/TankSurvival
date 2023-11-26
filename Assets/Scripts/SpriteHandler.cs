using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpriteHandler : MonoBehaviour
{
    public static SpriteHandler instance;

    [SerializeField] private List<Sprite> weaponSprites = null;
    [SerializeField] private List<Sprite> weaponIcons = null;



    public List<Sprite> WeaponSprites => weaponSprites;
    public List<Sprite> WeaponIcons => weaponIcons;
    public List<Sprite> BulletSprites => weaponIcons;

    private void Awake()
    {
        instance = this;
    }

}
