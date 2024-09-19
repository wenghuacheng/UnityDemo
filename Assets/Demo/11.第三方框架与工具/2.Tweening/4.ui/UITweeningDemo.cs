using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITweeningDemo : MonoBehaviour
{
    [SerializeField] private Text text;

    private void Start()
    {
        //新的textmesh好像没有相关属性
        text.DOText("abcdefghijklmnopqrstuvwxyz", 5f).SetEase(Ease.Linear);
    }
}