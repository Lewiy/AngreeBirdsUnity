using UnityEngine;
using System.Collections;
using Assets.Scripts;

[RequireComponent(typeof(Rigidbody2D))]
public class Bird : MonoBehaviour
{
    public virtual float ThrowSpeed { get { return 10; } }
    public virtual Vector2 Gravity { get { return Physics2D.gravity; } }

    private string birdType { get { return "Bird" ; } }
// Use this for initialization
void Start()
    {
        
      
        GetComponent<TrailRenderer>().enabled = false;
        GetComponent<TrailRenderer>().sortingLayerName = "Foreground";
     
        GetComponent<Rigidbody2D>().isKinematic = true;
       
        GetComponent<CircleCollider2D>().radius = Constants.BirdColliderRadiusBig;
        State = BirdState.BeforeThrown;
    }

   /* public float ThrowSpeed()
    {
        return 20;
    }*/

    void FixedUpdate()
    {
       
        if (State == BirdState.Thrown &&
            GetComponent<Rigidbody2D>().velocity.sqrMagnitude <= Constants.MinVelocity)
        {
           
            StartCoroutine(DestroyAfter(2));
        }
    }

    public virtual void OnThrow()
    {
      
        GetComponent<AudioSource>().Play();
        
        GetComponent<TrailRenderer>().enabled = true;
    
        GetComponent<Rigidbody2D>().isKinematic = false;
        
        GetComponent<CircleCollider2D>().radius = Constants.BirdColliderRadiusNormal;
        State = BirdState.Thrown;
    }

    IEnumerator DestroyAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }

    public BirdState State
    {
        get;
        private set;
    }
}
