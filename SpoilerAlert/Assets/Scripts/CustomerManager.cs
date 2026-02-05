using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    [SerializeField] private GameObject customerPrefab;
    [SerializeField] private int customerCount;
    [SerializeField] private Vector3 seatOffset = new Vector3(0, -0.5f, 0);
    [SerializeField] private PathPoint[] seats;
    [SerializeField] private GameOverScript gameOverScript;

    public static CustomerManager Instance;
    public int customersRemaining;

    private void Awake()
    {
        Instance = this;
        foreach (var seat in seats)
        {
            seat.Used = false;
            seat.Spoiled = false;
        }
    }

    private void Start()
    {
        SpawnCustomers();
    }

    private void SpawnCustomers()
    {
        List<PathPoint> AvailableSeats = new List<PathPoint>();

        foreach (var seat in seats)
        {
            if (!seat.Used)
                AvailableSeats.Add(seat);
        }
        
        int spawnCount = Mathf.Min(customerCount, AvailableSeats.Count);
        customersRemaining = spawnCount;

        for(int i = 0; i < spawnCount; i++)
        {
            int rand = Random.Range(0, AvailableSeats.Count);
            PathPoint seat = AvailableSeats[rand];
            AvailableSeats.RemoveAt(rand);

            Vector3 SpawnPos = seat.transform.position + seatOffset;

            GameObject customer = Instantiate(customerPrefab, SpawnPos, Quaternion.identity, transform);
            if (customer.TryGetComponent(out CustomerMovement movement))
            {
                movement.AssignSeat(seat);
            }

            seat.Used = true;
        }
    }

    public void CustomerLeft()
    {
        customersRemaining--;

        if (customersRemaining <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        AudioManager.instance.playGameOverBGM(customersRemaining);
        gameOverScript.SetUp();
        Time.timeScale = 0f;
    }

}
