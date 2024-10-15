using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public Rigidbody rb;
    
    
    private AudioManager audio;

    public Light Light;
    public float speed = 15;

    private bool istraveling;
    private Vector3 TravelDir;
    private Vector3 nextcolPos;

    public int minSwipeRecognition=500;
    private Vector2 swipeCurrentframe;
    private Vector2 swipeLastframe;
    private Vector2 currentSwipe;

    private Color solveColor;

    private void Start()
    {
        
        solveColor = Random.ColorHSV(0.5f, 1f);
        GetComponent<MeshRenderer>().material.color = solveColor;
        
        Light.color = solveColor;
        
        audio = FindObjectOfType<AudioManager>();        
    }
    private void FixedUpdate()
    {
        
        if (istraveling)
        {
            rb.velocity = TravelDir* speed;
           
        }

        Collider[] HitColliders = Physics.OverlapSphere(transform.position - (Vector3.up / 2), 0.05f);
        int i = 0;
        while (i < HitColliders.Length) 
        {
            GroundScript GroundPiece = HitColliders[i].transform.GetComponent<GroundScript>();
            if(GroundPiece && !GroundPiece.isColored)
            {
                GroundPiece.ColorChange(solveColor);
            }
            i++;
        }
        
        if(nextcolPos != Vector3.zero) { 
            if(Vector3.Distance(transform.position,nextcolPos)<1)
            {
                istraveling = false;
                TravelDir = Vector3.zero;
                nextcolPos = Vector3.zero;
            }
        }

        if (istraveling)    //if once swiped and ball is traveling then no more swipes until it stops
            return;         //return to upper and don't go through lower swipe

        if (Input.GetMouseButton(0))
        {
           
            swipeCurrentframe = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            if (swipeLastframe != Vector2.zero)
            {
                currentSwipe = swipeCurrentframe - swipeLastframe;// direction of swipe
                if(currentSwipe.sqrMagnitude< minSwipeRecognition) {
                    return;
                }
                currentSwipe.Normalize(); //normalize the direction
                //if swipe is under the specific quadrant of screen then swipe.
                if(currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                {
                    SetDestination(currentSwipe.y >0 ?Vector3.forward: Vector3.back );  
                }
                if (currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                {
                    SetDestination(currentSwipe.x > 0 ? Vector3.right : Vector3.left);
                }
            }
            swipeLastframe =swipeCurrentframe;

            if (Input.GetMouseButtonUp(0))
            {//we done the swipe.
                swipeLastframe = Vector3.zero;
                currentSwipe = Vector3.zero;
            }
        }
        
        
    }
    private void SetDestination(Vector3 Dir)
    {
        TravelDir = Dir;

        RaycastHit hit;
        if(Physics.Raycast(transform.position,Dir,out hit,100f)) 
        {
            nextcolPos = hit.point;
          
        }
        istraveling = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        audio.BallSFX();
    }
}
