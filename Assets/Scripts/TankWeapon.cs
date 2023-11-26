using UnityEngine;

public class TankWeapon : MonoBehaviour
{
    public static TankWeapon instance;
    [SerializeField] private Transform bulletStartPoint = null;
    [SerializeField] private SpriteRenderer spriteRenderer = null;
    [SerializeField] private Bullet bulletPrefab = null;

    private InputControls inputControls;
    private Weapon usingWeapon;

    private bool isRecharging;
    private float rechargingTimer;

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
        inputControls.Tank.Attack.performed += context => Attack();
    }

    public void SetWeapon(Weapon weapon)
    {
        usingWeapon = weapon;

        spriteRenderer.sprite = SpriteHandler.instance.WeaponSprites[weapon.id];
    }

    public void Attack()
    {
        if (isRecharging)
        {
            return;
        }

        var newBullet = Instantiate(bulletPrefab);

        newBullet.transform.position = bulletStartPoint.position;

        newBullet.SetBulletData(usingWeapon.bulletPrototype, SpriteHandler.instance.BulletSprites[usingWeapon.id]);
        newBullet.StartMove(transform.TransformDirection(Vector2.up));

        isRecharging = true;
    }

    protected void Update()
    {
        if (isRecharging)
        {
            rechargingTimer -= Time.deltaTime;
            if (rechargingTimer <= 0)
            {
                rechargingTimer = usingWeapon.rechargingTime;
                isRecharging = false;
            }
        }
    }
}
