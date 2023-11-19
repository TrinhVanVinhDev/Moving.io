using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    public static Character InsChar;

    private void Awake()
    {
        InsChar = this;
    }

    public Animator animationCharater;

    [HideInInspector] public IdleState idleState;
    [HideInInspector] public AttackState attackState;
    [HideInInspector] public RunState runState;
    [HideInInspector] public DeathState deathState;
    [HideInInspector] public DanceState danceState;
    [HideInInspector] public List<GameObject> listTarget;
    [HideInInspector] public GameObject weaponEquip;
    [HideInInspector] public IStateMachine curentState;
    [HideInInspector] public Vector3 originPosition;
    [HideInInspector] public Vector3 direction;
    [HideInInspector] public Vector3 movingDirection;
    [HideInInspector] public Transform targetAttack;
    [HideInInspector] public GameObject weaponSelf;
    [HideInInspector] public Color skinColor;
    [HideInInspector] public Character enemyTarget;
    [HideInInspector] public int point = 1;
    [HideInInspector] public bool godMode = false;
    [HideInInspector] public bool isMoving = false;
    [HideInInspector] public bool isAttack = false;
    [HideInInspector] public bool isDeath = false;
    [HideInInspector] public bool isVictory = false;
    [HideInInspector] public bool isLost = false;
    [HideInInspector] public static bool isNotPlaying = true;

    [SerializeField] private EquipmentDataBase equipmentData;
    [SerializeField] private Transform weaponOnHand;
    [SerializeField] private Transform hatOnHead;
    [SerializeField] private SkinnedMeshRenderer pantSkinMesh;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform bodyChar;
    [SerializeField] private Renderer bodyRender;

    private GameObject weapon;
    private GameObject hat;
    private GameObject weaponAttack;
    private string curentAnim;
    private int wanderRadius;
    private float curentScale = 100;

    public virtual void OnInit()
    {
        OnInitState();
        ChangeSkinColor(skinColor);
    }

    public virtual void Moving(Vector3 movingDirection, bool isPlayer = false)
    {
        ChangeAnimByBool(Constant.IS_RUN);
        if (!isPlayer)
        {
            wanderRadius = Random.Range(9, 20);
            originPosition = RandomNavSphere(transform, wanderRadius, NavMesh.AllAreas);
            direction = GetDirection(transform.position, originPosition);
            agent.SetDestination(originPosition);
        } else
        {
            originPosition = Vector3.zero;
            direction = movingDirection;
            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
            agent.Move(direction * Time.deltaTime * agent.speed);
        }
        isMoving = false;
    }

    public void Attack()
    {
        ChangeAnimByBool(Constant.IS_ATTACK);
        ActiveWeaponAttack();
        ResetPosWeapon();
        ThrowWeapon();
    }

    public void Death()
    {
        isDeath = true;
        ChangeAnimByBool(Constant.IS_DEAD);
    }

    public void StopMoving()
    {
        transform.position = transform.position;
        ChangeAnimByBool(Constant.IS_IDLE);
    }

    public void ChangeState(IStateMachine newState)
    {
        if (curentState != null)
        {
            curentState.OnExit(this);
        }

        if (newState != null)
        {
            curentState = newState;
            curentState.OnEnter(this);
        }
    }

    public void ChangeWeapon(WeaponId idWeapon)
    {
        if (weapon != null) Destroy(weapon);
        if (weaponAttack != null) Destroy(weaponAttack);
        Weapon weaponSelect = equipmentData.GetWeaponById((int)idWeapon);
        weapon = Instantiate(weaponSelect.meshPrefab, weaponOnHand);
        weaponAttack = Instantiate(weaponSelect.prefabs);
        weaponAttack.GetComponent<WeaponAttack>().characterSelf = bodyChar;
        weaponAttack.SetActive(false);
    }

    public void ChangePant(PantId idPant)
    {
        Skin charactorEquipPant = equipmentData.GetPantById((int)idPant);
        pantSkinMesh.material = charactorEquipPant.skinMaterial;
    }

    public void ChangeHat(HeadId Hat)
    {
        if (hat != null) Destroy(hat);
        Skin hatSelect = equipmentData.GetHeadById((int)Hat);
        hat = Instantiate(hatSelect.skinPrefabs, hatOnHead);
        float scaleX = hat.transform.localScale.x;
        if (scaleX >= curentScale)
        {
            hat.transform.localScale = new Vector3(1, 1, 1);
        } else if(scaleX <= 1)
        {
            hat.transform.localScale = new Vector3(100, 100, 100);
        }
    }

    public void ChangeAnimByBool(string animName)
    {
        if (curentAnim != animName && curentAnim != null)
        {
            animationCharater.SetBool(curentAnim, false);
            curentAnim = animName;
            animationCharater.SetBool(animName, true);
        }
        else if (curentAnim == null)
        {
            curentAnim = animName;
            animationCharater.SetBool(animName, true);
        }
    }

    public void DeactiveWeaponAttack(GameObject weaponEquip)
    {
        weaponEquip.SetActive(false);
    }

    public void ChangeSizeCharater(Transform body)
    {
        if (!isDeath)
        {
            body.localScale += new Vector3(0.2f, 0.2f, 0.2f); 
            body.position += new Vector3(0f, 0.2f, 0f);
        }
    }

    public void ChangeSkinColor(Color skin)
    {
        bodyRender.material.color = skin;
    }

    public void GetComponentEnemy()
    {
        if (enemyTarget == null)
        {
            enemyTarget = targetAttack.GetComponent<Character>();
        }
    }
    public virtual void ChangePropertyWithWeapon(GameObject charater, WeaponId idWeapon)
    {   
        Weapon weaponSelect = equipmentData.GetWeaponById((int)idWeapon);
        SphereCollider colliderChar = charater.transform.GetComponent<SphereCollider>();
        ChangeRadiusCollider(colliderChar, weaponSelect);
    }

    public WeaponId RandomCreateWeapon()
    {
        int indexWeapon = Random.Range(0, equipmentData.weaponCount - 1);
        Weapon weapon = equipmentData.GetWeaponByIndex(indexWeapon);
        return (WeaponId)weapon.id;
    }

    public PantId RandomCreatePant()
    {
        int indexPant = Random.Range(0, equipmentData.pantCount - 1);
        Skin pant = equipmentData.GetPantByIndex(indexPant);
        return (PantId)pant.idSkin;
    }

    public HeadId RandomCreateHead()
    {
        int indexHead = Random.Range(0, equipmentData.headCount - 1);
        Skin head = equipmentData.GetHeadByIndex(indexHead);
        return (HeadId)head.idSkin;
    }

    public static void PlayGame()
    {
        isNotPlaying = false;
    }

    public static void OutPlayGame()
    {
        isNotPlaying = true;
    }

    private void OnInitState()
    {
        idleState = new IdleState();
        attackState = new AttackState();
        runState = new RunState();
        deathState = new DeathState();
        danceState = new DanceState();
        ChangeState(idleState);
    }

    private void ChangeRadiusCollider(SphereCollider colliderChar, Weapon weaponData)
    {
        colliderChar.radius = weaponData.range + Constant.RADIUS_DFAULT;
    }

    private Vector3 RandomNavSphere(Transform origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin.position;
        NavMeshHit navHit;
        origin.rotation = Quaternion.LookRotation(randDirection, Vector3.up);
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
        return navHit.position;
    }

    private Vector3 GetDirection(Vector3 fromPosition, Vector3 toPosition)
    {
        return originPosition - transform.position;
    }

    private void ActiveWeaponAttack()
    {
        weaponAttack.SetActive(true);
    }

    private void ResetPosWeapon()
    {
        weaponAttack.gameObject.transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
    }

    private void ThrowWeapon()
    {
        Rigidbody rb = weaponAttack.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        float distance = Vector3.Distance(targetAttack.position, transform.position);
        Vector3 direction = (targetAttack.position - transform.position) / distance;
        rb.AddForce(direction * 500);
    }
}
