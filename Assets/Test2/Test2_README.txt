Scripts
TankMover.cs: 
Used for move the tank forward.
MoveSpeed - The moving speed of the tank, higher the value, faster the movement.


Bullet.cs:
Used for moving the bullet forward.
SetBulletSpeed(float speed) - Used for setting speed from other scripts.
OnTriggerEnter(Collider other) - Used for destroy bullet after collide the object with TankMover Component.

Shooter.cs:
bulletSpeed - Used for setting speed of bullet
fireRate - Used for spawn bullet by seconds
bullet - Used for assign the bullet to spawn
        
target - target to aim
partToTurn - Part of object to rotate after calculated intercept position
shootPos - Position of spawning projectile

Fire() - Used for spawn bullet
IsCanFire() - Used for check is the tank able to fire based on fire rate.
CalculateInterceptPosition - Used for calculate intercept position and check available of shooting