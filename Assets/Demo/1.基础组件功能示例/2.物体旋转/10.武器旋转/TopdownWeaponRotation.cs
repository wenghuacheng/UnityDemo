using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopdownWeaponRotation : MonoBehaviour
{
    [SerializeField] private Transform weaponRotationPointTransform;
    [SerializeField] private Transform weaponShootPosition;

    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        var mouseWorldPostion = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPostion.z = 0;

        var weaponDirection = (mouseWorldPostion - weaponShootPosition.position);
        float aimAngle = GetAngleFromVector(weaponDirection);

        AimDirection aimDirection = GetAimDirection(aimAngle);

        //旋转武器
        weaponRotationPointTransform.eulerAngles = new Vector3(0, 0, aimAngle);

        //基于旋转的方向翻转武器
        switch (aimDirection)
        {
            case AimDirection.Left:
            case AimDirection.UpLeft:
                weaponRotationPointTransform.localScale = new Vector3(1, -1, 0f);
                break;
            case AimDirection.Up:
            case AimDirection.UpRight:
            case AimDirection.Right:
            case AimDirection.Down:
                weaponRotationPointTransform.localScale = new Vector3(1, 1, 0f);
                break;
        }

    }

    #region Private Method
    private float GetAngleFromVector(Vector2 vector)
    {
        float radian = Mathf.Atan2(vector.y, vector.x);
        return radian * Mathf.Rad2Deg;
    }

    private AimDirection GetAimDirection(float angle)
    {
        if (angle >= 22f && angle <= 67f)
            return AimDirection.UpRight;
        else if (angle > 67 && angle <= 112f)
            return AimDirection.Up;
        else if (angle > 112f && angle <= 158f)
            return AimDirection.UpLeft;
        else if ((angle > 158f && angle <= 180f) || (angle > -180f && angle <= -135))
            return AimDirection.Left;
        else if (angle > -135f && angle <= -45f)
            return AimDirection.Down;
        else if ((angle > -45f && angle <= 0f) || (angle > 0f && angle <= 22f))
            return AimDirection.Right;

        return AimDirection.Right;
    }
    #endregion

    #region Constants

    public enum AimDirection
    {
        Up, UpRight, UpLeft, Right, Left, Down
    }

    #endregion
}
