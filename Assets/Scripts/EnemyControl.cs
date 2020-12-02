using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public GameObject goTarget;
    public GameObject prefProjectile;
    private ObjectTracker objectTracker;

    public float fTimeTilTarget = 1.2f;
    public bool bUseConstantSpeed = false;
    public float fSpeed = 20;
    public int iMaxIterations = 100;
    public float fBaseCheckTime = 0.15f;
    private float fTimePerCheck = 0.05f;

    private float fDistance = 9999;
    // Start is called before the first frame update
    void Start()
    {
        objectTracker = GetComponent<ObjectTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(goTarget.transform.position);

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (bUseConstantSpeed)
            {
                int iIterations = 0;
                Projectile bullet = GameObject.Instantiate(prefProjectile, this.transform.position, transform.rotation).GetComponent<Projectile>();

                float fCheckTime = fBaseCheckTime;
                Vector3 v3TargetPosition = objectTracker.GetProjectedPosition(fBaseCheckTime);
                Debug.DrawLine(this.transform.position, v3TargetPosition, Color.red, 1);

                //Predict projectile position
                Vector3 v3PredictedProjectilePosition = this.transform.position + ((v3TargetPosition - this.transform.position).normalized * fSpeed * fCheckTime);
                Debug.DrawLine(this.transform.position, v3PredictedProjectilePosition, Color.green, 3);
                fDistance = (v3TargetPosition - v3PredictedProjectilePosition).magnitude;

                while (fDistance > 1.5f && iIterations < iMaxIterations)
                {
                    iIterations++;
                    fCheckTime += fTimePerCheck;
                    v3TargetPosition = objectTracker.GetProjectedPosition(fCheckTime);
                    Debug.DrawLine(goTarget.transform.position, v3TargetPosition, Color.red, 3);

                    v3PredictedProjectilePosition = this.transform.position + ((v3TargetPosition - this.transform.position).normalized * fSpeed * fCheckTime);
                    Debug.DrawLine(this.transform.position, v3PredictedProjectilePosition, Color.green, 3);
                    fDistance = (v3TargetPosition - v3PredictedProjectilePosition).magnitude;
                }

                Vector3 v3Velocity = v3TargetPosition - this.transform.position;
                bullet.Shoot(v3Velocity.normalized, fSpeed);
            }
            else
            {
                Vector3 v3TargetPosition = objectTracker.GetProjectedPosition(fTimeTilTarget);
                Debug.DrawLine(this.transform.position, v3TargetPosition, Color.red, 1);
                Projectile bullet = GameObject.Instantiate(prefProjectile, this.transform.position, transform.rotation).GetComponent<Projectile>();
                Vector3 v3Velocity = v3TargetPosition - this.transform.position;
                float fVelocity = v3Velocity.magnitude / fTimeTilTarget;
                bullet.Shoot(v3Velocity.normalized, fVelocity);
            }
        }
    }
}
