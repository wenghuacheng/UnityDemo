using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var achievement = collision.GetComponent<PlayerAchievement>();
        if (achievement != null)
        {
            achievement.SetScore(1);
            Destroy(this.gameObject);
        }
        
    }
}
