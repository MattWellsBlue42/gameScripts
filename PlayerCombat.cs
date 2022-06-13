using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    public Animator animator;
    public Transform attackPointRight;
    public Transform attackPointLeft;
    public Transform attackPointUp;
    public Transform attackPointDown;
    public LayerMask enemyLayers;

    public float attackRange = 0.5f;
    public int attackDamage = 40;

    public float attackRate = 2f;
    float nextAttackTime = 0f;

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && animator.GetBool("rightFace") == true || Input.GetKeyDown(KeyCode.Space) && animator.GetBool("rightFace") == true)
            {
                attackRight();
                nextAttackTime = Time.time + 1f / attackRate;
            }
            if (Input.GetKeyDown(KeyCode.Mouse0) && animator.GetBool("leftFace") == true || Input.GetKeyDown(KeyCode.Space) && animator.GetBool("leftFace") == true)
            {
                attackLeft();
                nextAttackTime = Time.time + 1f / attackRate;
            }
            if (Input.GetKeyDown(KeyCode.Mouse0) && animator.GetBool("upFace") == true || Input.GetKeyDown(KeyCode.Space) && animator.GetBool("upFace") == true)
            {
                attackUp();
                nextAttackTime = Time.time + 1f / attackRate;
            }
            if (Input.GetKeyDown(KeyCode.Mouse0) && animator.GetBool("downFace") == true || Input.GetKeyDown(KeyCode.Space) && animator.GetBool("downFace") == true)
            {
                attackDown();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }

    }

    void attackRight()
    {
        //Play Attack Animation
        animator.SetTrigger("attackRight");

        //Detect Enemies in range
        Collider2D [] hitEnemies = Physics2D.OverlapCircleAll(attackPointRight.position, attackRange, enemyLayers);

        //Damage them
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    void attackLeft()
    {
        animator.SetTrigger("attackLeft");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointLeft.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    void attackUp()
    {
        animator.SetTrigger("attackUp");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointUp.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    void attackDown()
    {
        animator.SetTrigger("attackDown");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointDown.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPointRight == null)
            return;
        if (attackPointLeft == null)
            return;
        if (attackPointUp == null)
            return;
        if (attackPointDown == null)
            return;

        Gizmos.DrawWireSphere(attackPointRight.position, attackRange);
        Gizmos.DrawWireSphere(attackPointLeft.position, attackRange);
        Gizmos.DrawWireSphere(attackPointUp.position, attackRange);
        Gizmos.DrawWireSphere(attackPointDown.position, attackRange);
    }
}
