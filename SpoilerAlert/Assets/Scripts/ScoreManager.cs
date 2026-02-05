using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int customersLeft;
    private string rank;
    [SerializeField] private CustomerManager customerManager;

    void Start()
    {
        customersLeft = customerManager.GetComponent<CustomerManager>().customersRemaining;
    }

    
    void Update()
    {
        if(customersLeft == 36)
        {
            rank = "S";
        }else if(customersLeft >= 30 && customersLeft <= 35)
        {
            rank = "A";
        }else if(customersLeft >= 26 && customersLeft <= 29)
        {
            rank = "B";
        }else if(customersLeft >= 16 && customersLeft <= 25)
        {
            rank = "C";
        }else if(customersLeft >= 6 && customersLeft <= 15)
        {
            rank = "D";
        }
        else
        {
            rank = "You're fired.";
        }
    }
}
