using UnityEngine;

public class GrassScript : MonoBehaviour
{
    public float slowSpeed = 2;
    private float originalMoveSpeed;
    //make sure grass collider set to trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //setting original move speed so we can set it back when you exit the grass
        originalMoveSpeed =  collision.gameObject.GetComponent<CarControllerRB>().forwardSpeed;
        //divide the move speed of the car by the slowspeed float
        collision.gameObject.GetComponent<CarControllerRB>().forwardSpeed /= slowSpeed;

        //Debug.Log("collision with grass detected");
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //sets your move speed back to what it was before the grass when you exit the collider
        collision.gameObject.GetComponent<CarControllerRB>().forwardSpeed = originalMoveSpeed;
    }
}
