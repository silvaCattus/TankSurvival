public class Rocket : Weapon
{
    public Rocket()
    {
        bulletPrototype = new RocketBullet();
        id = 1;
        rechargingTime = 1f;
    }
}

