using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// code from https://www.youtube.com/watch?v=_TcEfIXpmRI
// BlackThornPod trail effect tutorial
public class EchoEffect : MonoBehaviour
{
    // Start is called before the first frame update

    //public float timeBtwSpawns;
    //public float startTimeBtwSpawns;

    public GameObject echo;
    public float echoLifetime = 1.5f;

    public void CreateEcho()
    {
        GameObject instance = Instantiate(echo, transform.position, Quaternion.identity);
        Destroy(instance, echoLifetime);
    }
}
    //private Player player;
    // Update is called once per frame
    //*void Start()
    //{
        //player = GetComponent<Player>();
    //}
    //void Update()
   // {
        //if(player.moveInput != 0)
       // {
    //    if(timeBtwSpawns <= 0)
     //   {
      //      GameObject instance = (GameObject)Instantiate(echo, transform.position, Quaternion.identity);
       //     Destroy(instance, 8f);
        //    timeBtwSpawns = startTimeBtwSpawns;
        //} else {
         //   timeBtwSpawns -= Time.deltaTime;
        //}
        //} 
   // }/*