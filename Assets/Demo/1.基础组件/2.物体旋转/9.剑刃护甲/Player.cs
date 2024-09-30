using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ObjectRotation.BladeArmor
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private GameObject bladePrefab;

        private List<Blade> blades = new List<Blade>();
        private float bladeInterval = 3;//��������ҵļ��

        private float slowBladeRotationSpeed = 15;//������ת�ٶ�
        private float bladeRotationSpeed = 30;//������ת�ٶ�
        private float fastBladeRotationSpeed = 45;//У׼λ��ʱ������ת�ٶ�

        private Blade axisBlade;
        private bool needAdjustBladePosition = false;

        //�������ڱ��
        private Color[] colors = new Color[] { Color.red, Color.yellow, Color.blue, Color.green, Color.grey, Color.black, Color.cyan };

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
                AddBlade();

            Movement();
            AutoBladeRotateAroundSelf();
        }

        /// <summary>
        /// �ƶ�
        /// </summary>
        private void Movement()
        {
            var x = Input.GetAxis("Horizontal");
            var y = Input.GetAxis("Vertical");
            this.transform.Translate(new Vector2(x, y) * Time.deltaTime);
        }

        #region �������&�Ƴ�
        /// <summary>
        /// ��ӽ���
        /// </summary>
        private void AddBlade()
        {
            var blade = Instantiate(bladePrefab, transform.up * bladeInterval, Quaternion.identity, this.transform);

            blade.GetComponentInChildren<SpriteRenderer>().color = colors[blades.Count];//����

            var b = blade.GetComponent<Blade>();
            b.OnBladeDestory += Blade_OnBladeDestory;
            blades.Add(b);

            //�����ӵĽ��г�����������Ҫ����λ��
            if (blades.Count <= 1)
            {
                axisBlade = null;
                needAdjustBladePosition = false;
            }
            else
            {
                axisBlade = b;
                needAdjustBladePosition = true;
            }

            //��һ�ֿ���ֱ��ˢ�µ��ķ�ʽ
            //RefreshBladePosition();
        }


        /// <summary>
        /// �Ƴ�����
        /// </summary>
        /// <param name="blade"></param>
        private void RemoveBlade(Blade blade)
        {
            blade.OnBladeDestory -= Blade_OnBladeDestory;
            blades.Remove(blade);
            //RefreshBladeRotation();//Ч������
            if (blades.Count <= 1)
            {
                axisBlade = null;
                needAdjustBladePosition = false;
            }
            else
            {
                axisBlade = blades[blades.Count - 1];
                needAdjustBladePosition = true;
            }
        }

        /// <summary>
        /// ���������¼�����
        /// </summary>
        /// <param name="obj"></param>
        private void Blade_OnBladeDestory(Blade blade)
        {
            RemoveBlade(blade);
        }
        #endregion

        /// <summary>
        /// �ý������Լ���ת
        /// </summary>
        private void AutoBladeRotateAroundSelf()
        {
            //CommonAutoBladeRotateAroundSelf();

            if (!needAdjustBladePosition)
            {
                CommonAutoBladeRotateAroundSelf();
            }
            else
            {
                AdjustAutoBladeRotateAroundSelf();
            }
        }

        /// <summary>
        /// ��׼������ת���Ѿ�ȷ����λ��
        /// </summary>
        private void CommonAutoBladeRotateAroundSelf()
        {
            for (int i = 0; i < blades.Count; i++)
            {
                var blade = blades[i];
                blade.AutoRotateAroundTarget(this.transform, bladeRotationSpeed);
            }
        }

        /// <summary>
        /// ���ٽ�����ת����Ҫȷ��λ��
        /// ��Ŀǰ�����⡿
        /// </summary>
        private void AdjustAutoBladeRotateAroundSelf()
        {
            var angle = 360 / blades.Count;

            var axis = new Vector3(0, 0, 1);//���ڼ���ǶȵĻ�׼��
            var newBladeIndex = blades.IndexOf(axisBlade);

            ////����
            //StringBuilder sb = new StringBuilder();
            //for (int i = 0; i < blades.Count; i++)
            //{
            //    var blade = blades[i];
            //    float bladeAngle = Vector3.SignedAngle(blade.transform.up, axisBlade.transform.up, axis);
            //    sb.Append(bladeAngle + ",");
            //}
            //Debug.Log($"���:{angle},�����Ƕ�{sb.ToString()}");

            for (int i = 0; i < blades.Count; i++)
            {
                var blade = blades[i];

                if (newBladeIndex == i)
                {
                    //��׼���У������Լ����ٶ���ת
                    blade.AutoRotateAroundTarget(this.transform, bladeRotationSpeed);
                    continue;
                }

                //�������������ļн�
                float bladeAngle = Vector3.SignedAngle(blade.transform.up, axisBlade.transform.up, axis);
                bladeAngle = (bladeAngle - 360f) % 360f;//��������ĽǶ�
                bladeAngle = Mathf.Abs(bladeAngle);

                var intervalCount = Mathf.Abs(newBladeIndex - i);
                var mathAngle = intervalCount * angle;

                if (!Mathf.Approximately(mathAngle, bladeAngle))
                {
                    if (mathAngle <= bladeAngle)
                    {
                        blade.AutoRotateAroundTarget(this.transform, slowBladeRotationSpeed);
                    }
                    else
                    {
                        blade.AutoRotateAroundTarget(this.transform, fastBladeRotationSpeed);
                    }
                }
                else
                {
                    //�Ѿ�������ȷλ��
                    blade.AutoRotateAroundTarget(this.transform, bladeRotationSpeed);
                }
            }
        }

        /// <summary>
        /// ˢ�½��нǶȡ���ӽ��к�������ȷֲ���
        /// </summary>
        private void RefreshBladePosition()
        {
            if (blades.Count <= 1) return;

            var angle = 360 / blades.Count;

            Vector3 originalVector = this.transform.up;//��ת��׼��������ʼ��
            Vector3 axis = new Vector3(0, 0, 1);//��ת��,��������Z��ת

            for (int i = 0; i < blades.Count; i++)
            {
                var blade = blades[i];
                //���ڽǶȺ���ת��������ת��Ԫ��
                Quaternion rotation = Quaternion.AngleAxis(angle * i, axis);
                //������ת��ķ�������
                Vector3 rotatedVector = rotation * originalVector;
                //�����µĽ���λ��
                blade.transform.position = this.transform.position + rotatedVector * bladeInterval;

                //��������ʱ�ᵼ�²��ῴ��Ŀ��,����ˢ��һ��
                blade.LookAtTarget(this.transform);
            }
        }

    }
}