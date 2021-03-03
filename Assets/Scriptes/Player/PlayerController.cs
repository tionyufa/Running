using System.Collections;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;


public class PlayerController : MonoBehaviourPunCallbacks
{
    private static PlayerController Singletion { get; set; }
    
    [Header("Move")]
    [SerializeField] float speedRotate;
    [SerializeField] Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private Animator _animator; 
    [SerializeField] private float timeSlow;
   
    public bool isTuchJoy;
    private VariableJoystick variableJoystick;
    
    protected float timeRun;
    private float moveVertical;
    private float speedPlus = 0.2f;
    private float speedMinus = 0.2f; 
    private float timePlusSpeed;
     
    [Header("Photon")]
    public PhotonView _photonView;
    
    
    
    [Header("Skill")]
    [SerializeField] private int _forceRb;
    [SerializeField] private GameObject _panelSkills;
    [SerializeField] private GameObject _arrowImage;
    [SerializeField] private GameObject _imageCube;
    [SerializeField] private Vector3 posit;
    [SerializeField] SkillPlayer skillPlayer;
    [SerializeField] private GameObject shieldButton;
    [SerializeField] private Image _imageShield;
    
        private float shieldtime;
        private int numSkill = 0;
        public bool isShield;
        public Ray ray;
        private Quaternion _quaternion;
        private Vector3 position;
     
        
    
    private void Awake()
    {
        Singletion = this;
        variableJoystick = FindObjectOfType<VariableJoystick>();
        

    }

    private void Start()
    {
        if (_photonView.IsMine)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    private void Update()
    {
      
        TimeRun();
        isTuchJoy = variableJoystick.isTuch;
        skillPlayer.isTuch(isTuchJoy);
        skillPlayer.RunTime(timeRun);
    
       
       
        
    }

    public void FixedUpdate()
    {
       
        if (_photonView.IsMine && PhotonNetwork.IsConnected)
        {
             Move();  
        }
        else return;


    }

    private void TimeRun()
    {
        shieldtime -= Time.deltaTime;
        timeRun -= Time.deltaTime;
        timeSlow -= Time.deltaTime;
        timePlusSpeed -= Time.deltaTime;
        
        if (timeRun < 0 )
        timeRun = 0;
        
        if (timeSlow < 0)
        timeSlow = 0;
        
        if (timePlusSpeed < 0)
        timePlusSpeed = 0;


        if (shieldtime < 0)
        {
            shieldtime = 0;
            _imageShield.fillAmount = 1f;
        }
        else if (shieldtime > 0)
        {
            _imageShield.fillAmount = shieldtime / 5;
        }
        
    }

    public void Move()
    {
        
        if (timeRun <= 0)
        {
            if (timeSlow > 0)
            {
                speed = speedMinus;
            }
            else if (timeSlow < 0.0001f)
            {
                speed = 0.2f;
            }
            
            moveVertical = Input.GetAxis("Vertical") * speed + variableJoystick.Vertical * speed;
            float rotateHorizonatl = Input.GetAxis("Horizontal") * speedRotate + variableJoystick.Horizontal * speedRotate;

            
            transform.Translate(0, 0, moveVertical, Space.Self);
            transform.Rotate(0, rotateHorizonatl, 0);

            _animator.SetFloat("Run1", Mathf.Abs(moveVertical));
            
            _animator.SetBool("Fire",false);
            _animator.SetBool("Shock",false);
            _animator.SetBool("Stun",false);
            _animator.SetBool("Slow",false);

        }
        else
        {
            _animator.SetBool("Stun",true);
            
            
        }
    }

    public void SpeedPlus (float value, float plusTime)
    {
        
        speedPlus = speed + value;
        
        if (speedPlus > 0.3f)
        {
            speedPlus = 0.3f;
        }
        
    }

    public void SpeedMin(float value, float _slowTime)
    {
        timeSlow = _slowTime;
        speedMinus = speed - value;
        if (speedMinus < 0.1f)
        {
            speedMinus = 0.1f;
        }
    }

    public void Stun()
    {
        if (_photonView.IsMine) 
             
        {
                PhotonNetwork.Instantiate("PreFab/Partical/Star", transform.position + new Vector3(0,2,0), Quaternion.identity);
        }
    }

    
    public void Stun_Time(float value)
    {
       timeRun = value;
    }

   

    public void ActiveSkill()
    {
        int skillID = Random.Range(0, _panelSkills.transform.childCount);
        var skill = _panelSkills.transform;
        
       
            if (skill.GetChild(skillID).gameObject.activeSelf == false && numSkill < 4)
            {
                skill.GetChild(skillID).gameObject.SetActive(true);
                numSkill++;
                return;
            }
            
            
            if (skill.GetChild(skillID).gameObject.activeSelf)
            {
                for (int i = 0; i < _panelSkills.transform.childCount; i++)
                {
                    
                    skillID++;
                    if (skillID >= _panelSkills.transform.childCount)
                    {
                        skillID = 0;
                    }
                    
                    
                    else if (skill.GetChild(skillID).gameObject.activeSelf == false && numSkill < 4)
                    {
                        skill.GetChild(skillID).gameObject.SetActive(true);
                        numSkill++;
                        return;
                    }
                                        
                }
               
            }
            else return;
            
          
    }

    public void Skill(string nameskill)
    {
        if (nameskill == "Shield")
        {
            _imageShield.fillAmount = 1f;
               
        }
        for (int i = 0; i < _panelSkills.transform.childCount; i++)
            {
                if (_panelSkills.transform.GetChild(i).name == nameskill)
                {
                    _panelSkills.transform.GetChild(i).gameObject.SetActive(false);
                    numSkill--; 
                    if (numSkill < 0) 
                        numSkill = 0;
                }
            }
       
                      
    }

    public void ForcePlayer()
    {
        if (_photonView.IsMine && timeRun <= 0)
        {
            rb.AddRelativeForce(Vector3.forward * _forceRb, ForceMode.Force);
            Skill("Force");
        }
    }

    public void AnimatorIce( Quaternion quaternion)
    {
        if (_photonView.IsMine && timeRun <= 0)
        {
            _quaternion = quaternion;
            _animator.SetBool("Slow", true);
        }
    }
    
    [PunRPC]
    public void IceStart()
    {
          if (_photonView.IsMine)
          {
             _arrowImage.SetActive(false);  
             var _clone = PhotonNetwork.Instantiate("PreFab/Partical/Slow",transform.position,Quaternion.identity);
             _clone.GetComponent<SlowSkill>().setVector(transform,_quaternion);
             _clone.GetComponent<SlowSkill>().ID = _photonView.ViewID;
            Skill("Ice");
            _arrowImage.SetActive(false);
          }
        
    }

   
    [PunRPC]
    public void Shield()
    {
         if (_photonView.IsMine && !isShield)
         {
             isShield = true;
             shieldButton.GetComponent<Image>().color = Color.gray;
            
            var shiled = PhotonNetwork.Instantiate("PreFab/Partical/Shield", transform.position, Quaternion.identity);
            shiled.GetComponent<Shield>().tranf(gameObject.transform);
            StartCoroutine("ShieldImage");
         }
    }

    IEnumerator ShieldImage()
    {
        shieldtime = 5f;
        yield return new WaitForSeconds(4.9f);
        Skill("Shield");
        shieldButton.GetComponent<Image>().color = Color.green;
        _imageShield.fillAmount = 1f;
        isShield = false;
    }
    
     public void Shock()
        {
             if (_photonView.IsMine && timeRun <= 0)
             {
                _imageCube.SetActive(true);
               _animator.SetBool("Shock", true);
             }
           
        }
     
    [PunRPC]
    public void ShockStart()
    {
         if (_photonView.IsMine)
         {
            _imageCube.SetActive(false);
            var shock = PhotonNetwork.Instantiate("PreFab/Partical/Shock", transform.position, Quaternion.identity);
            shock.GetComponent<StunSkill>().setID(_photonView.ViewID);
            Skill("Shock");
            _imageCube.SetActive(false);
         }
    }
   
    public void AnimatorFire(Ray _ray)
    {
        ray = _ray;
        _animator.SetBool("Fire",true);
    }
  
    [PunRPC]
     public void FireStart()
    {
         if (_photonView.IsMine)
         { 
          Vector3 FXposition = transform.position + posit;
        var Clone = PhotonNetwork.Instantiate("PreFab/Partical/Fire",FXposition,Quaternion.identity);
        
          Clone.GetComponent<TranslateFire>().setID(ray);
          Clone.GetComponent<TranslateFire>().ID = _photonView.ViewID;
          Skill("Fire");
        }
        
    }

   
}