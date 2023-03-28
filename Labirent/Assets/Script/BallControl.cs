using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{   
    public UnityEngine.UI.Button btn;
    public UnityEngine.UI.Text time, life, status;
    private Rigidbody rg;
    public float speed = 1.5f;
    float timeCounter = 30;
    float lifeCounter = 5;
    bool gameContinue=true;
    bool gameCompleted=false;
    // Start is called before the first frame update
    void Start()
    {
        life.text = lifeCounter+"";
        time.text = timeCounter+"";
        rg = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {   if(gameContinue && !gameCompleted){
            timeCounter -= Time.deltaTime; // timeCounter = timeCounter - deltaTime;
            time.text= (int)timeCounter+"";
        }else{
            status.text = "Game Over!";
            btn.gameObject.SetActive(true);
        }
        if(timeCounter<0) gameContinue=false;
    }

    void FixedUpdate()
    {
        if(gameContinue){
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 force = new Vector3(-vertical,0,horizontal);
        rg.AddForce(force*speed);
        }else {
            rg.velocity = Vector3.zero;
            rg.angularVelocity = Vector3.zero;
        }
    }

    void OnCollisionEnter(Collision cls)
    {
        string objName = cls.gameObject.name;
        if(objName.Equals("Finish")){
            gameCompleted=true;
            gameContinue=false;
            status.text="Game Completed";
            btn.gameObject.SetActive(true);
        }else if(!objName.Equals("MazeGround")&& !objName.Equals("OutsideGround"))
        {
            lifeCounter -= 1;
            life.text = (int) lifeCounter+"";
            if(lifeCounter==0) gameContinue=false;
        }

    }
}
