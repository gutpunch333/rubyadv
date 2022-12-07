using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


﻿public class RubyController : MonoBehaviour
{
    public float speed = 3.0f;
    
    public int maxHealth = 5;
    public int ammoCount = 4;
    
    public GameObject projectilePrefab;
    
    public AudioClip throwSound;
    public AudioClip hitSound;
    //public AudioClip winSound;
    //public AudioClip loseSound;
    public AudioClip backgroundMusic;
    public AudioSource musicSource;
    public AudioClip speedSound;
    public AudioClip frogSound;
    
    public int health { get { return currentHealth; }}
    public int currentHealth;
    
    public float timeInvincible = 2.0f;
    bool isInvincible;
    float invincibleTimer;

    public bool canGoFast;
    public float timeFast = 0.2f;
    float fastTimer;
    
    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;
    
    Animator animator;
    Vector2 lookDirection = new Vector2(1,0);
    
    AudioSource audioSource;

    public ParticleSystem healEffect;
    public ParticleSystem hitEffect;
    public ParticleSystem speedEffect;

    public GameObject winObj;
    public GameObject loseObj;
    public GameObject loseObj2;
    public GameObject speedPlats;
    public Text score;
    public Text ammo;
    //public GameObject camera1;
    //public GameObject camera2;

    //public Transform pos2;

    public bool ifIsStage2 = false;
    public int numFixed;
    private int livesValue = 5;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
        currentHealth = maxHealth;
        score.text = "Number Fixed: " + numFixed.ToString();
        ammo.text = "Remaining Cogs: " + ammoCount.ToString();
        audioSource = GetComponent<AudioSource>();
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
        //musicSource.clip = backgroundMusic;
        musicSource.Play();
        numFixed = 0;
        if (ifIsStage2 == true){
            numFixed = numFixed + 4;
            score.text = "Number Fixed: " + numFixed.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        
        Vector2 move = new Vector2(horizontal, vertical);
        
        if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }
        
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);
        
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }
        
        if(Input.GetKeyDown(KeyCode.C))
        {
            Launch();
            ammo.text = "Remaining Cogs: " + ammoCount.ToString();
        }
        //if lose and reset on level 1 or win on 2 and reset
        if((livesValue < 1 && Input.GetKeyDown(KeyCode.R) && ifIsStage2 == false) || (numFixed > 7 && Input.GetKeyDown(KeyCode.R)))
        {
            
            
            
            
            UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            //teleport player to start 
            winObj.SetActive(false);
            loseObj.SetActive(false);
            loseObj2.SetActive(false);
            // respawn bots
            musicSource.Stop();
            musicSource.clip = backgroundMusic;
            musicSource.Play();
            score.text = "Number Fixed: " + numFixed.ToString();
            ammo.text = "Remaining Cogs: " + ammoCount.ToString();
            if (livesValue < 1){
                musicSource.Stop();
                musicSource.clip = backgroundMusic;
                musicSource.Play();
            }
            if (numFixed > 7){
                musicSource.Stop();
                musicSource.clip = backgroundMusic;
                musicSource.Play();
            }
        }

        // if lose on 2 and reset
        if((livesValue < 1 && Input.GetKeyDown(KeyCode.R) && ifIsStage2 == true))
        {
            
            UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            //respawn bots
            //teleport player to level 2
            winObj.SetActive(false);
            loseObj.SetActive(false);
            loseObj2.SetActive(false);
            musicSource.Stop();
            musicSource.clip = backgroundMusic;
            musicSource.Play();
            score.text = "Number Fixed: " + numFixed.ToString();
            ammo.text = "Remaining Cogs: " + ammoCount.ToString();
        }
        //if lose on 1
        if((livesValue < 1 || livesValue == -1) && ifIsStage2 == false)
        {
                loseObj.SetActive(true);
                musicSource.Stop();
                
                Destroy(GameObject.FindGameObjectWithTag("Music"));
                NonPlayerCharacter levelTwoText = GameObject.FindGameObjectWithTag("NPC").gameObject.GetComponent<NonPlayerCharacter>();
                levelTwoText.DisplayDialog2();
                UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
            }
        // if lose on 2
        if((livesValue < 1 || livesValue == -1) && ifIsStage2 == true)
        {
                loseObj2.SetActive(true);
                musicSource.Stop();
                
                Destroy(GameObject.FindGameObjectWithTag("Music"));
                UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
                NonPlayerCharacter levelTwoText = GameObject.FindGameObjectWithTag("NPC").gameObject.GetComponent<NonPlayerCharacter>();
                levelTwoText.DisplayDialog2();
            }
            // if win on 2
            if((numFixed > 7) && ifIsStage2 == true)
        {
                winObj.SetActive(true);
                musicSource.Stop();
                
                Destroy(GameObject.FindGameObjectWithTag("Music"));
                UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
                
            }
        
        if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null && hit.collider.tag != "speedFrog")
            {
                if (ifIsStage2 == true){
                    SceneManager.LoadScene(1);
                    
                //transform.position=pos2.position;
                //camera1.SetActive(false);.buildIndex
                //camera2.SetActive(true);
                //musicSource.Stop();
                }

                NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
                if (character != null)
                {
                    character.DisplayDialog();
                }

                if (numFixed < 4)
                {
            
                NonPlayerCharacter levelTwoText = GameObject.FindGameObjectWithTag("NPC").gameObject.GetComponent<NonPlayerCharacter>();
                levelTwoText.DisplayDialog2();
            
                }
                PlaySound(frogSound);


            }
            if (hit.collider != null && hit.collider.tag == "speedFrog")
            {
                

                /*NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
                if (character != null)
                {
                    character.DisplayDialog();
                }*/

            
                NPC2 hideSpeedText = GameObject.FindGameObjectWithTag("speedFrog").gameObject.GetComponent<NPC2>();
                hideSpeedText.DisplayDialog4();
            
                
                PlaySound(frogSound);
                speedPlats.SetActive(true);

            }
        }
        
        if (canGoFast)
        {
            fastTimer -= Time.deltaTime;
            speed = 6;
            
            if (fastTimer < 0)
            {
                canGoFast = false;
                speed = 3;
            }
        }
         
        


         
    }
    
    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }

    public void GoingFast()
    {
        if (canGoFast)
                return;
            
            canGoFast = true;
            fastTimer = timeFast;
        //if (canGoFast==true)
        //{
            //speed = 6;
        
        //canGoFast = false;
        //}
        ParticleSystem playSpeedEffect = Instantiate(speedEffect, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
        
    }

    public void GottaGoFast()
    {
        PlaySound(speedSound);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
                return;
            
            isInvincible = true;
            invincibleTimer = timeInvincible;
            
            
            livesValue = livesValue + amount;
            animator.SetTrigger("Hit");
            ParticleSystem playHitEffect = Instantiate(hitEffect, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
            UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
            PlaySound(hitSound);

            
            
        }
        
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        if (amount > 0)
        {
            ParticleSystem playHealEffect = Instantiate(healEffect, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
        }
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
    }

    public void ChangeAmmo(int number)
    {
        ammoCount = ammoCount + 1;
        ammo.text = "Remaining Cogs: " + ammoCount.ToString();
    }
    
    void Launch()
    {
        if (ammoCount > 0){
            GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

            Projectile projectile = projectileObject.GetComponent<Projectile>();
            projectile.Launch(lookDirection, 300);

            animator.SetTrigger("Launch");
        
            PlaySound(throwSound);

            ammoCount = ammoCount -1;
        }
    } 
    
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    public void callNumberFixed()
    {
        numFixed = numFixed + 1;
        score.text = "Number Fixed: " + numFixed.ToString();
        
       if (numFixed == 4)
        {
            ifIsStage2 = true;
            UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
            NonPlayerCharacter levelTwoText = GameObject.FindGameObjectWithTag("NPC").gameObject.GetComponent<NonPlayerCharacter>();
            levelTwoText.DisplayDialog();
            
        }
        if (numFixed > 4)
        {
            
            
            NonPlayerCharacter levelTwoText = GameObject.FindGameObjectWithTag("NPC").gameObject.GetComponent<NonPlayerCharacter>();
            levelTwoText.DisplayDialog2();
            
        }
        
        if (numFixed > 7)
        {
            winObj.SetActive(true);
            musicSource.Stop();
            
            
            NonPlayerCharacter levelTwoText = GameObject.FindGameObjectWithTag("NPC").gameObject.GetComponent<NonPlayerCharacter>();
            levelTwoText.DisplayDialog2();
        }

    }
}