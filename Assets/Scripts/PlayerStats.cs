using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;


    // Health
    public int maxHealth = 100;

    private int _curHealth;
    public int curHealth
    {
        get { return _curHealth; }
        set { _curHealth = Mathf.Clamp(value, 0, maxHealth); }
    }

    public float healthRegenRate = 2f;
    

    //Hydration
    public int maxHydro = 100;

    private int _curHydro;
    public int curHydro
    {
        get { return _curHydro; }
        set { _curHydro = Mathf.Clamp(value, 0, maxHydro); }
    }

    public float hydroLoss = 0.5f;
    public float thirstDamage = 1f;


    //Misc
    public float movementSpeed = 10f;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
}
