using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Optimizes
{
    public class OptimizeUI : MonoBehaviour
    {
        /**
         * 1:Canvas����
         * ��Canvas�е�Ԫ�ط����仯ʱ��������һ������(�ؽ�)���ؽ�����Canvas UI����
         * �任�����������л����ƶ��������С������۵Ĵ�仯����һ�ۿ���������С�仯���ؽ����̵ĳɱ��ܸ�
         * ���Էֲ���Ƕ�׿��Լ����ؽ���������ɱ�
         * ����ӻ����а�����Ԫ�ط����仯����ֻ�������ӻ������ؽ������������и�����
         * **/

        /**
         * 2.�������ٵ�����Layout ���������VerticalLayoutGroup���ڴ�ֱ���룬GridLayoutGroup�����������
         * ����Ŀ������༭ĳЩ����ʱ���ᷢ�������ؽ��������ؽ����������ؽ�һ������һ������Ĺ���
         * ���Ի���ê���Լ���д�ű����ƣ������ҿ�Դ�ģ�
         * **/

        /**
         * 3.���ֻ��ʾImage��RawImage����������Raycast Target          
         * **/

        /** 
         * 4.��������ʹ��Mask�����RectMask2d�����Ϊ����Ч��
         * ��ʹʹ���ˣ��ڲ���Ҫʱ��enabled����Ϊfalse�����������ε�Ŀ�걣���ڱ�Ҫ������޶�
         * **/

        /**
         * 5.TextMeshProʹ��ʱ����ʹ��SetText������ʹ�ø�ʽ���ַ���
         * label.SetText("{0}", number);  //������string����
         * label.text = number.ToString(); //���������һ��string
         * **/

        /**
         * 6.UGUI��ͨ��SetActive��ʽ��ʾ/���سɱ��ܸߣ���ΪOnEnableΪ�����ؽ�����Dirty��־��ִ����������صĳ�ʼ��
         * ʹ��CanvasGroup������Aplhaֵ��Ϊ0���Խ������أ�Ч����ߣ�
         * ����һ��������enabled����
         * **/
    }
}