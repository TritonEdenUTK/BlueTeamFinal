
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeSlowAbility : MonoBehaviour
{
    public float abilityDuration = 6f;
    public float timeScale = 0.15f;
    public float cooldownDuration = 10f;
    public List<Knight> enemies;
    public EchoEffect echoEffect;

    private float abilityTimer = 0f;
    private float cooldownTimer = 0f;
    private bool slowDownReady = false;
    private bool hasFlashed = false;

    // for cooldown indicator
    public Renderer playerRenderer;

    // PlayerController and Animator components used so player moves normally
    public PlayerController playerController;
    private Rigidbody2D playerRigidbody;

    private float originalJumpImpulse;

  
   
    
   // public Animator playerAnimator;

   private void Awake()
   {
        playerController = GetComponent<PlayerController>();
        playerRigidbody = GetComponent<Rigidbody2D>();

        originalJumpImpulse = playerController.jumpImpulse;
   }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && cooldownTimer <= 0)
        {
            StartCoroutine(SlowTime());
            slowDownReady = false;
            hasFlashed = false;
        }

        if (abilityTimer > 0)
        {
            abilityTimer -= Time.unscaledDeltaTime;
            if (abilityTimer % 0.005f <= Time.deltaTime) 
            { // generate echo trail every .1 seconds
                echoEffect.CreateEcho();
                 // Create an echo trail while ability is active
            }
            //Invoke("echoEffect.CreateEcho()", .09f);
        }
        else if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
        else if (cooldownTimer <= 0 && slowDownReady == true && hasFlashed == false)
        {
           StartCoroutine(FlashPlayerGreen()); 
           hasFlashed = true;
        }
        if (cooldownTimer <= 0)
        {
            slowDownReady = true;
        }
    }

// Working SlowTime() function just works on enemies
    /*IEnumerator SlowTime()
    {
        abilityTimer = abilityDuration;
        cooldownTimer = cooldownDuration;

        foreach (Knight enemy in enemies)
        {
            enemy.walkSpeed *= timeScale;
            enemy.GetComponent<Animator>().speed *= timeScale;
        }

        yield return new WaitForSeconds(abilityDuration);

        foreach (Knight enemy in enemies)
        {
            enemy.walkSpeed /= timeScale;
            enemy.GetComponent<Animator>().speed /= timeScale;
        }
    }*/

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ResetTimeScale();
    }

    private void ResetTimeScale()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
    }
    IEnumerator SlowTime()
    {
        //float originalFollowSpeed = oribitalTransposer.m_FollowOffset.z;
        
        //oribitalTransposer.m_FollowOffset.z *= 0.25f;
        
        abilityTimer = abilityDuration;
        cooldownTimer = cooldownDuration;

        Time.timeScale = timeScale;
        // fixedDelta altered
        Time.fixedDeltaTime = 0.02f * timeScale; 

        //playerAnimator.speed /= timeScale;
        
        //Vector3 originalDamping = framingTransposer.m_Damping;
        //framingTransposer.m_Damping = originalDamping * (1 / timeScale);
        float elapsedTime = 0f;

        playerController.walkSpeed /= timeScale;
        playerController.airWalkSpeed /= timeScale;
        playerController.GetComponent<Animator>().speed /= timeScale;
        //virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_TrackingSpeed *= 2;
        //playerController.GetComponent<Rigidbody2D>().gravityScale /= timeScale;

        //playerController.jumpImpulse /= timeScale;
        //playerController.jumpImpulse /= Mathf.Sqrt(timeScale);
        float originalGravityScale = playerRigidbody.gravityScale;

        playerRigidbody.gravityScale *= 20;
        //playerRigidbody.gravityScale /= timeScale;
        playerController.jumpImpulse = originalJumpImpulse * 4.5f;
        
        while (elapsedTime < abilityDuration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }


        //Time.timeScale = 1f;
        playerController.walkSpeed *= timeScale;
        playerController.airWalkSpeed *= timeScale;

        playerController.jumpImpulse = originalJumpImpulse;
        //playerController.jumpImpulse *= timeScale;
        playerController.GetComponent<Animator>().speed *= timeScale;

        while (playerRigidbody.velocity.y > 0) 
        {
            yield return null;
        }
        //playerController.jumpImpulse *= Mathf.Sqrt(timeScale);
        //playerController.GetComponent<Rigidbody2D>().gravityScale *= timeScale;
        playerRigidbody.gravityScale = originalGravityScale;
        //oribitalTransposer.m_FollowOffset.z = originalFollowSpeed;
        //virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_TrackingSpeed = originalFollowSpeed;

        //framingTransposer.m_Damping = originalDamping;
        ResetTimeScale();
        //Time.timeScale = 1f;
        //Time.fixedDeltaTime = 0.02f;
        //playerAnimator.speed *= timeScale;
    }

   /* public float slowTimeScale = 0.25f;
    public float slowTimeDuration = 6f;
    public float slowTimeCooldown = 10f;
    public KeyCode slowTimeKey = KeyCode.E;
    public Renderer playerRenderer;

    private float nextAvailableTime = 0f;
    private bool isFlashing = false;

    private void Update()
    {
        if (Input.GetKeyDown(slowTimeKey) && Time.time >= nextAvailableTime)
        {
            StartCoroutine(SlowTime());
            nextAvailableTime = Time.time + slowTimeCooldown;
            isFlashing = false;
        }
        else if (Time.time >= nextAvailableTime && !isFlashing)
        {
            StartCoroutine(FlashPlayerGreen());
            isFlashing = true;
        }
    }*/

    /*IEnumerator SlowTime()
    {
        abilityTimer = abilityDuration;
        cooldownTimer = cooldownDuration;

        foreach (Knight enemy in enemies)
        {
            enemy.walkSpeed *= timeScale;
            enemy.GetComponent<Animator>().speed *= timeScale;
        }

        yield return new WaitForSeconds(abilityDuration);

        foreach (Knight enemy in enemies)
        {
            enemy.walkSpeed /= timeScale;
            enemy.GetComponent<Animator>().speed /= timeScale;
        }
    }

    /*private IEnumerator SlowTime()
    {
        Time.timeScale = slowTimeScale;

        float elapsedTime = 0f;
        while (elapsedTime < slowTimeDuration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }

        Time.timeScale = 1f;
    }*/


    private IEnumerator FlashPlayerGreen()
    {
        // Store original player color
        Color originalColor = playerRenderer.material.color;

        // set player color to green
        playerRenderer.material.color = Color.green;

        // wait 1 second
        yield return new WaitForSeconds(.35f);

        // restore orignal player color
        playerRenderer.material.color = originalColor;
    }

    public bool IsTimeSlowActive()
    {
        return abilityTimer > 0;
    }


}
