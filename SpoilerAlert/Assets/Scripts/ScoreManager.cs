using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public int customersLeft;
    public string rank;
    public TextMeshProUGUI CustomerCount;
    public TextMeshProUGUI Rank;
    
    [SerializeField] private CustomerManager customerManager;

    void Update()
    {
        customersLeft = customerManager.GetComponent<CustomerManager>().customersRemaining;
        
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

        CustomerCount.text = "Customers left: " + customersLeft;
        Rank.text = "Rank: " + rank;

    }
}
