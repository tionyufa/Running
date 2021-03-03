using Photon.Pun;
using UnityEngine;



public class TranslateFire : MonoBehaviour , IPunObservable
{
    [SerializeField] private int distance;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Transform arrow;
    [SerializeField] private Vector3 transformPosition;
    [SerializeField] private int Speed;
    [SerializeField] private GameObject ShockFire;
    public float timeLife;
    [SerializeField] private float f;
    
     RaycastHit hit;
     private PlayerController _playerController;
     private Ray _ray;
     private Vector3 _vector3;
     private Ray RaymousePos;
     private float y;
     private int IDconntoll;
     private bool acter;
     public int ID = 0;
     
     private void Awake()
     {
         
         _vector3 = RaymousePos.direction + new Vector3(0, -1, 0);
          
     }

     public void setID(Ray Raymouse)
     {
        RaymousePos = Raymouse;
                  
     }

    
     private void Update()
     { 
        timeLife -= Time.deltaTime;
            
           MoveFire();
     }

     public void MoveFire()
     {
         if (timeLife > 0)
         {
             Vector3 rayDir = RaymousePos.direction.normalized;

             _ray = new Ray(transform.position, _vector3 * distance);
             transformPosition = arrow.position;

             Debug.DrawRay(transform.position, _vector3 * distance, Color.blue);

             if (rayDir.y < 0)
             {
                 rayDir.y = +0.1f;
             }

             if (Physics.Raycast(_ray, out hit, _layerMask))
             {
                 if (hit.collider)
                 {
                     var val = hit.point.y;
                     y = transformPosition.y - val;
                     if (y > 2.5f)
                     {
                         rayDir.y -= f;
                     }
                     else if (y < 2.5f)
                     {
                         rayDir.y += f;
                     }
                 }
                 else
                 {
                     rayDir = _ray.direction;
                 }
             }

             transform.Translate(rayDir * Time.deltaTime * Speed, Space.Self);
         }

         else if (timeLife <= 0)
             {
                 var pos = transform.position - new Vector3(0, 2.5f, 0);
                 var stun = PhotonNetwork.Instantiate("PreFab/Partical/Shock", transform.position, Quaternion.identity);
                 stun.GetComponent<StunSkill>().setID(ID);
                 Destroy(stun, 0.4f);
                 Destroy(gameObject);
             }
         
     }
     
     private void OnTriggerEnter(Collider other)
     {

         if (other != null && ID != 0 && other.GetComponent<PlayerController>() != null)
         {

             if (other.GetComponent<PhotonView>().ViewID != ID &&
                 !other.GetComponent<PlayerController>().isShield)
             {
                 timeLife = 0.25f;

             }
             else if (other.GetComponent<PhotonView>().ViewID != ID &&
                      other.GetComponent<PlayerController>().isShield)
             {
                 timeLife = 0.25f;
                 other.GetComponent<PlayerController>().isShield = false;
                 if (other.GetComponent<PlayerController>().transform.GetChild(5) != null)
                     Destroy(other.GetComponent<PlayerController>().transform.GetChild(5).gameObject);
             }
         }
         //bot
            if (other.GetComponent<BotsControl>() && !other.GetComponent<BotsControl>().isShield)
             {
                 timeLife = 0.25f;
             }
             else if (other.GetComponent<BotsControl>() && other.GetComponent<BotsControl>().isShield)
             {
                 timeLife = 0.25f;
                 other.GetComponent<BotsControl>().isShield = false;
                
             }
        
     }


     public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
     {
         if (stream.IsWriting)
         {
             stream.SendNext(timeLife);
                          
         }
         else if (stream.IsReading)
         {
             timeLife = (float) stream.ReceiveNext();
             
             
         }
     }
}
