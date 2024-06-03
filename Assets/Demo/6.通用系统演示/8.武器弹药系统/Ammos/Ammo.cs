using UnityEngine;

namespace Demo.Common.Weapons
{
    public class Ammo : MonoBehaviour, IFireable
    {
        [SerializeField] private float useAimAngleDistance = 1;//最小瞄准距离
        [SerializeField] private TrailRenderer trailRenderer;

        private float ammoRange = 0f;
        private float ammoSpeed = 0;
        private Vector2 fireDirectionVector;//发射方向向量
        private float fireDirectionAngle;//发射角度
        private SpriteRenderer spriteRenderer;
        private AmmoDetailSO ammoDetail;

        private float ammoChargeTimer;//弹药充能时间
        private bool isAmmoMaterialSet = false;
        private bool overrideAmmoMovement;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if (ammoChargeTimer > 0)
            {
                ammoChargeTimer -= Time.deltaTime;
                return;
            }
            else if (!isAmmoMaterialSet)
            {
                isAmmoMaterialSet = true;
                //SetAmmoMaterial();
            }

            //弹药在一帧内移动的增量
            Vector3 distanceVector = fireDirectionVector * ammoSpeed * Time.deltaTime;
            transform.position += distanceVector;

            //记录弹药是否已经超过射程
            ammoRange -= distanceVector.magnitude;
            if (ammoRange < 0f)
            {
                DisableAmmo();
            }
        }

        #region IFireable
        public GameObject GetGameObject()
        {
            return transform.gameObject;
        }

        public void InitializeAmmo(AmmoDetailSO ammoDetail, float aimAngle, float weaponAimAngle, float ammoSpeed, Vector2 weaponAimDirectionVector, bool overrideAmmoMovement = false)
        {
            #region Ammo

            this.ammoDetail = ammoDetail;
            SetFireDirection(ammoDetail, aimAngle, weaponAimAngle, weaponAimDirectionVector);
            spriteRenderer.sprite = ammoDetail.ammoSprite;

            if (ammoDetail.ammoChargeTime > 0)
            {
                //存在充能时间
                ammoChargeTimer = ammoDetail.ammoChargeTime;
                SetAmmoMaterial(ammoDetail.ammoChargeMaterial);
                isAmmoMaterialSet = false;
            }
            else
            {
                //无充能时间
                ammoChargeTimer = 0f;
                SetAmmoMaterial(ammoDetail.ammoMaterial);
                isAmmoMaterialSet = true;
            }

            this.ammoRange = ammoDetail.ammoRange;
            this.ammoSpeed = ammoDetail.ammoSpeed;
            this.overrideAmmoMovement = overrideAmmoMovement;

            gameObject.SetActive(true);

            #endregion

            #region Trail
            if (ammoDetail.isAmmoTrail)
            {
                trailRenderer.gameObject.SetActive(true);
                trailRenderer.emitting = true;
                trailRenderer.material = ammoDetail.ammoTrailMaterial;
                trailRenderer.startWidth = ammoDetail.ammoTrailStartWidth;
                trailRenderer.endWidth = ammoDetail.ammoTrailEndWidth;
                trailRenderer.time = ammoDetail.ammoTrailTime;
            }
            else
            {
                trailRenderer.gameObject.SetActive(false);
                trailRenderer.emitting = false;
            }

            #endregion
        }
        #endregion

        private void OnTriggerEnter2D(Collider2D collision)
        {
            DisableAmmo();
        }

        private void SetFireDirection(AmmoDetailSO ammoDetail, float aimAngle, float weaponAimAngle, Vector2 weaponAimDirectionVector)
        {
            float randomSpread = Random.Range(ammoDetail.ammoSpreadMin, ammoDetail.ammoSpreadMax);
            int spreadToggle = Random.Range(0, 2) * 2 - 1;//生成-1和1两个数量用于判断是向左偏移还是向右偏移

            if (weaponAimDirectionVector.magnitude < useAimAngleDistance)
            {
                //小于最小瞄准距离使用自身锚点
                fireDirectionAngle = aimAngle;
            }
            else
            {
                //正常使用武器锚点
                fireDirectionAngle = weaponAimAngle;
            }

            //基于散步计算真实的角度
            fireDirectionAngle += spreadToggle * randomSpread;

            //设置弹药旋转
            transform.eulerAngles = new Vector3(0, 0, fireDirectionAngle);

            //基于旋转角度计算方向
            fireDirectionVector = GetDirectionVectorFromAngle(fireDirectionAngle);
        }

        private Vector2 GetDirectionVectorFromAngle(float angle)
        {
            // 将角度转换为弧度
            float angleRadians = angle * Mathf.Deg2Rad;

            // 使用三角函数计算方向向量
            float x = Mathf.Cos(angleRadians);
            float y = Mathf.Sin(angleRadians);

            // 创建方向向量
            Vector3 direction = new Vector3(x, y, 0f);

            return direction;
        }

        private void SetAmmoMaterial(Material material)
        {
            spriteRenderer.material = material;
        }

        private void DisableAmmo()
        {
            gameObject.SetActive(false);
        }
    }
}
