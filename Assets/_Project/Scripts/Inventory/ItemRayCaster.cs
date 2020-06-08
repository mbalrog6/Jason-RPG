using JasonRPG.Entity;
using UnityEngine;

public class ItemRayCaster : ItemComponent
{
    [SerializeField] private float _delay;
    [SerializeField] private float _range = 10f;
    [SerializeField] private int damage = 1;
    
    private RaycastHit[] _results = new RaycastHit[100];
    private int _layermask;

    private void Awake()
    {
        _layermask = LayerMask.GetMask("Default");
    }

    public override void Use()
    {
        _nextUseTime = Time.time + _delay;

        Ray ray = Camera.main.ViewportPointToRay(Vector3.one / 2f);
        var hits = Physics.RaycastNonAlloc(ray, _results, _range, _layermask, QueryTriggerInteraction.Collide);

        RaycastHit nearest = new RaycastHit();
        double nearestDistance = double.MaxValue;
        for (int i = 0; i < hits; i++)
        {
            var distance = Vector3.Distance(transform.position, _results[i].point);
            if (distance < nearestDistance)
            {
                nearest = _results[i];
                nearestDistance = distance;
            }
        }

        if (hits > 0)
        {
            var takeHits = nearest.collider.GetComponent<ITakeHit>();
            takeHits?.TakeHit(damage);
        }
    }
}