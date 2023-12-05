using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform _spawnerBullets;
    [SerializeField] private Bullet _bullet;

    private Bullet _newBullet;
    public void ChangeDirection()
    {
        _spawnerBullets.localPosition = -_spawnerBullets.localPosition;
        _spawnerBullets.localRotation = -_spawnerBullets.localRotation;
    }

    public void Attack()
    {
        _newBullet = Instantiate(_bullet, _spawnerBullets.position, Quaternion.identity);
        _newBullet.transform.localEulerAngles = new Vector3(_spawnerBullets.localRotation.x, 0, -_spawnerBullets.localRotation.y);
    }
}
