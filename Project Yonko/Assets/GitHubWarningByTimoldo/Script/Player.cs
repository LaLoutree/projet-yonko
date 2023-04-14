using UnityEngine;
using System.Collections;
using System;
using TMPro;

public class Player : MonoBehaviour
{
    // mettre serialized les attributs du player si ils sont actualisés à chaque instant du jeux
    [SerializeField] public string Username { get; set; }
    [SerializeField] public bool IsAlive;
    [SerializeField] public int Shield;
    [SerializeField] public int Health;
    [SerializeField] public int Speed;
    [SerializeField] public int Strength;
    [SerializeField] public int Discretion;
    [SerializeField] public int Stamina;
    [SerializeField] public float BonusHealth;
    [SerializeField] public float BonusSpeed;
    [SerializeField] public float Bonusstrength;
    [SerializeField] public float Bonusdiscretion;
    [SerializeField] public float BonusStamina;
    [SerializeField] public int MaxHealth;
    [SerializeField] public int MaxShield;
    [SerializeField] public int Money;
    public HealthBar HealthBar;
    public ShieldBar ShieldBar;
    public TMP_Text TextMoney;


    Player(string type, string username) //Creer un personnage en prenant son type (nos 4 persos)
    {
        Money = 0;
        Username = username;
        MaxShield = 100;
        MaxHealth = 100;
        Health = 100;
        Speed = 100;
        Strength = 100;
        Discretion = 100;
        Stamina = 100;
        Shield = 50;
        IsAlive = true;
        HealthBar.SetMaxHealth(MaxHealth);
        ShieldBar.SetMaxShield(MaxShield);
        switch (type) 
        {
            //Caracteristiques du personnage
            case "Ngannou":
                MaxHealth = 150;
                BonusHealth = 0.8f;
                BonusSpeed = 1;
                Bonusstrength = 1.2f;
                Bonusdiscretion = 0.8f;
                BonusStamina = 1;
                break;
            case "Tyson":
                MaxHealth = 100;
                BonusHealth = 0.8f;
                BonusSpeed = 1;
                Bonusstrength = 1.2f;
                Bonusdiscretion = 0.8f;
                BonusStamina = 1;
                break;
            case "Arsenik":
                MaxHealth = 120;
                BonusHealth = 0.8f;
                BonusSpeed = 1;
                Bonusstrength = 1.2f;
                Bonusdiscretion = 0.8f;
                BonusStamina = 1;
                break;
            default: //"Tavares"
                MaxHealth = 110;
                BonusHealth = 0.8f;
                BonusSpeed = 1;
                Bonusstrength = 1.2f;
                Bonusdiscretion = 0.8f;
                BonusStamina = 1;
                break;
        }
    }

    void Update() //Test la barre de vie
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(75);
            Money += 10;
        }
        
        TextMoney.SetText("Money : " + Money);
    }

    void TakeDamage(int damage)
    {
        Shield -= damage;
        if (Shield < 0)
        {
            Health += Shield;
            Shield = 0;
            HealthBar.SetHealth(Health);
            if (Health < 1)
            {
                Die();
                IsAlive = false;
            }
        }
        ShieldBar.SetShield(Shield);
    }

    void Regen(int PV)
    {
        Health += PV;
        if (Health > MaxHealth) 
        {
            Health = MaxHealth;
        }
        HealthBar.SetHealth(Health);
    }

    void Die()
    {
        HealthBar.SetHealth(0);
        ShieldBar.SetShield(0);
        // Respawn ??
        // New scene ??
    }

    void UseMoney(int price)
    {
        Money -= price;
    }

    void WinMoney(int income)
    {
        Money += income;
    }
}

    /*
    [SyncVar]
    private bool _isDead = false;
    public bool isDead
    {
        get { return _isDead; }
        protected set { _isDead = value; }
    }

    [SerializeField]
    private float maxHealth = 100f;

    [SyncVar]
    private float currentHealth;

    public float GetHealthPct()
    {
        return (float)currentHealth / maxHealth;
    }

    [SyncVar]
    public string username = "Player";

    public int kills;
    public int deaths;

    [SerializeField]
    private Behaviour[] disableOnDeath;

    [SerializeField]
    private GameObject[] disableGameObjectsOnDeath;

    private bool[] wasEnabledOnStart;

    [SerializeField]
    private GameObject deathEffect;

    [SerializeField]
    private GameObject spawnEffect;

    private bool firstSetup = true;

    [SerializeField]
    private AudioClip hitSound;
    [SerializeField]
    private AudioClip destroySound;

    public void Setup()
    {
        if(isLocalPlayer)
        {
            // Changement de caméra
            GameManager.instance.SetSceneCameraActive(false);
            GetComponent<PlayerSetup>().playerUIInstance.SetActive(true);
        }

        CmdBroadcastNewPlayerSetup();
    }

    [Command(ignoreAuthority = true)]
    private void CmdBroadcastNewPlayerSetup()
    {
        RpcSetupPlayerOnAllClients();
    }

    [ClientRpc]
    private void RpcSetupPlayerOnAllClients()
    {
        if(firstSetup)
        {
            wasEnabledOnStart = new bool[disableOnDeath.Length];
            for (int i = 0; i < disableOnDeath.Length; i++)
            {
                wasEnabledOnStart[i] = disableOnDeath[i].enabled;
            }

            firstSetup = false;
        }

        SetDefaults();
    }

    public void SetDefaults()
    {
        isDead = false;
        currentHealth = maxHealth;

        // Ré-active les scripts du joueur
        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = wasEnabledOnStart[i];
        }

        // Ré-active les gameobjects du joueur
        for (int i = 0; i < disableGameObjectsOnDeath.Length; i++)
        {
            disableGameObjectsOnDeath[i].SetActive(true);
        }

        // Ré-active le collider du joueur
        Collider col = GetComponent<Collider>();
        if(col != null)
        {
            col.enabled = true;
        }

        // Apparition du système de particules de mort
        GameObject _gfxIns = Instantiate(spawnEffect, transform.position, Quaternion.identity);
        Destroy(_gfxIns, 3f);
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(GameManager.instance.matchSettings.respawnTimer);
        
        Transform spawnPoint = NetworkManager.singleton.GetStartPosition();
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;

        yield return new WaitForSeconds(0.1f);

        Setup();
    }

    private void Update()
    {
        if(!isLocalPlayer)
        {
            return;
        }

        if(Input.GetKeyDown(KeyCode.K))
        {
            RpcTakeDamage(25, "Joueur");
        }
    }

    [ClientRpc]
    public void RpcTakeDamage(float amount, string sourceID)
    {
        if(isDead)
        {
            return;
        }

        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(hitSound);

        currentHealth -= amount;
        Debug.Log(transform.name + " a maintenant : " + currentHealth + " points de vies.");

        if(currentHealth <= 0)
        {
            audioSource.PlayOneShot(destroySound);
            Die(sourceID);
        }
    }

    private void Die(string sourceID)
    {
        isDead = true;

        Player sourcePlayer = GameManager.GetPlayer(sourceID);
        if(sourcePlayer != null)
        {
            sourcePlayer.kills++;
            GameManager.instance.onPlayerKilledCallback.Invoke(username, sourcePlayer.username);
        }

        deaths++;

        // Désactive les components du joueur lors de la mort
        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = false;
        }

        // Désactive les gameobjects du joueur lors de la mort
        for (int i = 0; i < disableGameObjectsOnDeath.Length; i++)
        {
            disableGameObjectsOnDeath[i].SetActive(false);
        }

        // Désactive le collider du joueur
        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            col.enabled = false;
        }

        // Apparition du système de particules de mort
        GameObject _gfxIns = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(_gfxIns, 3f);

        // Changement de caméra
        if(isLocalPlayer)
        {
            GameManager.instance.SetSceneCameraActive(true);
            GetComponent<PlayerSetup>().playerUIInstance.SetActive(false);
        }
        Debug.Log(transform.name + " a été éliminé.");

        StartCoroutine(Respawn());

    }
    */