using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Demo.Common.Weapons
{
    public interface IFireable
    {
        void InitializeAmmo(AmmoDetailSO ammoDetail, float aimAngle, float weaponAimAngle,
            float ammoSpeed, Vector2 weaponAimDirectionVector, bool overrideAmmoMovement = false);

        GameObject GetGameObject();
    }


    /**
     * 
     * 如果距离正常，则使用武器->目标作为瞄准线
     * 如果距离小于一个值，则使用角色的锚点->目标作为瞄准线
     * 
     * **/
}
