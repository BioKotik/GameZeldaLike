﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float thrust;
    public float knockTime;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Rigidbody2D enemy = collision.GetComponent<Rigidbody2D>();
            if(enemy != null)
            {
                enemy.GetComponent<Enemy>().currentState = EnemyState.stagger;
                enemy.isKinematic = false;
                Vector2 difference = enemy.transform.position - transform.position;
                difference = difference.normalized * thrust;
                enemy.AddForce(difference, ForceMode2D.Impulse);
                enemy.isKinematic = true;
                StartCoroutine(Knock(enemy));
                
            }
        }
    }

    private IEnumerator Knock(Rigidbody2D enemy)
    {
        if (enemy != null)
        {
            yield return new WaitForSeconds(knockTime);
            enemy.velocity = Vector2.zero;
            enemy.GetComponent<Enemy>().currentState = EnemyState.idle;
        }
    }


}
