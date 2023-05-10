using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement; // Added as part of falling death, used for restarting level

// Contains all of the components for a "living" character, allowing the character to be damaged by others
public class Damageable : MonoBehaviour
{
    public UnityEvent<int, Vector2> damageableHit;
    public UnityEvent<int, int> healthChanged;
    Animator animator;

    // MAX HEALTH: How much health the character is given at the start of the game
    [SerializeField]
    private int _maxHealth = 100;
    public int MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        set
        {
            _maxHealth = value;
        }
    }

    // HEALTH: Current health that the character has during the game
    [SerializeField]
    private int _health = 100;
    public int Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
            healthChanged?.Invoke(_health, MaxHealth);
            // if health drops 0 and below, character is no longer alive
            if (_health <= 0)
            {
                IsAlive = false;
            }
        }
    }

    // Boolean checking if the player is still alive (health above 0) or invincible (can't be hit)
    [SerializeField]
    private bool _isAlive = true;
    [SerializeField]
    private bool isInvincible = false;

    // Boolean checking if the character is hit (calling the hit function)
    public bool IsHit
    {
        get
        {
            return animator.GetBool(AnimationStrings.isHit);
        }
        private set
        {
            animator.SetBool(AnimationStrings.isHit, value);
        }
    }

    // Determines the time after a hit is done and the invincibility time
    private float timeSinceHit = 0;
    public float invinsibilityTime = 0.5f;

    public bool IsAlive
    {
        get
        {
            return _isAlive;
        }
        private set
        {
            _isAlive = value;
            animator.SetBool(AnimationStrings.isAlive, value);
            Debug.Log("IsAlive set " + value);
            // Added to restart level once character dies
            if (_isAlive == false)
            {
                Die();
                //Invoke("ResetLevel", 3);
            }
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update checks for invincibility
    private void Update()
    {
        if (isInvincible)
        {
            if (timeSinceHit > invinsibilityTime)
            {
                // remove invincibility
                isInvincible = false;
                timeSinceHit = 0;
            }

            timeSinceHit += Time.deltaTime;
        }
    }

    // Hit will subtract the current health by the damage given. Also delivers a knockback and uses Unity Events
    public bool Hit(int damage, Vector2 knockback)
    {
        if (IsAlive && !isInvincible)
        {
            Health -= damage;
            isInvincible = true;

            // Notify other subscribed components that damageable was hit to handle knockback and such
            IsHit = true;
            damageableHit?.Invoke(damage, knockback);
            CharacterEvents.characterDamaged.Invoke(gameObject, damage);

            return true;
        }
        return false;
    }

    // Death from Falling out of the Map
    public GameObject flatLined;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("trap"))
        {
            Die();
        }
    }

    // If the player dies, reset the level
    private void Die()
    {
        animator.SetBool(AnimationStrings.isAlive, false);
        flatLined.SetActive(true);
        Invoke("ResetLevel", 2);
    }

    private void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
