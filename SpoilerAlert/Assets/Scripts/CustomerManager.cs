using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    [SerializeField] private GameObject customerPrefab;
    [SerializeField] private int customerCount = 12;
    [SerializeField] private Vector3 seatOffset = new Vector3(0, -0.5f, 0);
    [SerializeField] private PathPoint[] seats;

    private void Awake()
    {
        foreach (var seat in seats)
        {
            seat.Used = false;
        }
    }

    private void Start()
    {
        SpawnCustomers();
    }

    private void SpawnCustomers()
    {
        List<PathPoint> AvailableSeats = new List<PathPoint>(seats);

        foreach (var seat in seats)
        {
            if (!seat.Used)
                AvailableSeats.Add(seat);
        }
        
        int spawnCount = Mathf.Min(12, AvailableSeats.Count);

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
}
