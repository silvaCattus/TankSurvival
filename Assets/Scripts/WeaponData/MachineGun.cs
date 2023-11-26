public class MachineGun : Weapon
{
    public MachineGun()
    {
        bulletPrototype = new MachineGunBullet();
        id = 0;
        rechargingTime = 0.1f;
    }
}

