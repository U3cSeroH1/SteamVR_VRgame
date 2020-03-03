using System.Collections.Generic;
using UnityEngine;

namespace NaughtyWaterBuoyancy
{
    //[RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    //[RequireComponent(typeof(MeshFilter))]
    public class FloatingObject : MonoBehaviour
    {
        [SerializeField]
        private bool calculateDensity = false;

        [SerializeField]
        private float density = 0.75f;

        [SerializeField]
        [Range(0f, 1f)]
        private float normalizedVoxelSize = 0.5f;

        [SerializeField]
        private float dragInWater = 1f;

        [SerializeField]
        private float angularDragInWater = 1f;

        private WaterVolume water;
        private new Collider collider;
        private new Rigidbody rigidbody;
        private float initialDrag;
        private float initialAngularDrag;
        private Vector3 voxelSize;
        private Vector3[] voxels;



        //fixedUPD
        private Vector3 forceAtSingleVoxel;
        private Bounds bounds;
        private float voxelHeight;
        private float submergedVolume;
        private Vector3 worldPoint;
        private float waterLevel;
        private float deepLevel;
        private float submergedFactor;
        private Vector3 surfaceNormal;
        private Quaternion surfaceRotation;
        private Vector3 finalVoxelForce;

        //CalculateMaxBuoyancyForce()
        private float objectVolume;
        private Vector3 maxBuoyancyForce;

        // CutIntoVoxels()
        private Quaternion initialRotation;
        List<Vector3> voxelsCIV = new List<Vector3>();



        protected virtual void Awake()
        {
            if (this.collider)
            {
                this.collider = this.GetComponent<Collider>();
            }
            else
            {

                this.collider = this.GetComponentInChildren<Collider>();
            }
            if (this.rigidbody)
            {

                this.rigidbody = this.GetComponent<Rigidbody>();
            }
            else
            {
                this.rigidbody = this.GetComponentInChildren<Rigidbody>();
            }


            this.initialDrag = this.rigidbody.drag;
            this.initialAngularDrag = this.rigidbody.angularDrag;

            if (this.calculateDensity)
            {
                float objectVolume = MathfUtils.CalculateVolume_Mesh(this.GetComponent<MeshFilter>().mesh, this.transform);
                this.density = this.rigidbody.mass / objectVolume;
            }

        }

        protected virtual void FixedUpdate()
        {
            if (this.water != null && this.voxels.Length > 0)
            {
                bounds = this.collider.bounds;
                voxelHeight = bounds.size.y * this.normalizedVoxelSize;

                submergedVolume = 0f;

                forceAtSingleVoxel = this.CalculateMaxBuoyancyForce() / this.voxels.Length;

                for (int i = 0; i < this.voxels.Length; i++)
                {
                    worldPoint = this.transform.TransformPoint(this.voxels[i]);
                    
                    waterLevel = this.water.GetWaterLevel(worldPoint);
                    deepLevel = waterLevel - worldPoint.y + (voxelHeight / 2f); // How deep is the voxel                    
                    submergedFactor = Mathf.Clamp(deepLevel / voxelHeight, 0f, 1f); // 0 - voxel is fully out of the water, 1 - voxel is fully submerged
                    submergedVolume += submergedFactor;

                    surfaceNormal = this.water.GetSurfaceNormal(worldPoint);
                    surfaceRotation = Quaternion.FromToRotation(this.water.transform.up, surfaceNormal);
                    surfaceRotation = Quaternion.Slerp(surfaceRotation, Quaternion.identity, submergedFactor);

                    finalVoxelForce = surfaceRotation * (forceAtSingleVoxel * submergedFactor);
                    this.rigidbody.AddForceAtPosition(finalVoxelForce, worldPoint);

                    Debug.DrawLine(worldPoint, worldPoint + finalVoxelForce.normalized, Color.blue);
                }

                submergedVolume /= this.voxels.Length; // 0 - object is fully out of the water, 1 - object is fully submerged

                this.rigidbody.drag = Mathf.Lerp(this.initialDrag, this.dragInWater, submergedVolume);
                this.rigidbody.angularDrag = Mathf.Lerp(this.initialAngularDrag, this.angularDragInWater, submergedVolume);
            }
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(WaterVolume.TAG))
            {
                this.water = other.GetComponent<WaterVolume>();
                if (this.voxels == null)
                {
                    this.voxels = this.CutIntoVoxels();
                }
            }
        }

        protected virtual void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(WaterVolume.TAG))
            {
                this.water = null;
            }
        }

        protected virtual void OnDrawGizmos()
        {
            if (this.voxels != null)
            {
                for (int i = 0; i < this.voxels.Length; i++)
                {
                    Gizmos.color = Color.magenta - new Color(0f, 0f, 0f, 0.75f);
                    Gizmos.DrawCube(this.transform.TransformPoint(this.voxels[i]), this.voxelSize * 0.8f);
                }
            }
        }

        private Vector3 CalculateMaxBuoyancyForce()
        {
            objectVolume = this.rigidbody.mass  / this.density;
            maxBuoyancyForce = this.water.Density * objectVolume * -Physics.gravity;

            return maxBuoyancyForce;
        }

        private Vector3[] CutIntoVoxels()
        {
            initialRotation = this.transform.rotation;
            this.transform.rotation = Quaternion.identity;

            bounds = this.collider.bounds;
            this.voxelSize.x = bounds.size.x * this.normalizedVoxelSize;
            this.voxelSize.y = bounds.size.y * this.normalizedVoxelSize;
            this.voxelSize.z = bounds.size.z * this.normalizedVoxelSize;
            int voxelsCountForEachAxis = Mathf.RoundToInt(1f / this.normalizedVoxelSize);
            voxelsCIV = new List<Vector3>(voxelsCountForEachAxis * voxelsCountForEachAxis * voxelsCountForEachAxis);

            for (int i = 0; i < voxelsCountForEachAxis; i++)
            {
                for (int j = 0; j < voxelsCountForEachAxis; j++)
                {
                    for (int k = 0; k < voxelsCountForEachAxis; k++)
                    {
                        float pX = bounds.min.x + this.voxelSize.x * (0.5f + i);
                        float pY = bounds.min.y + this.voxelSize.y * (0.5f + j);
                        float pZ = bounds.min.z + this.voxelSize.z * (0.5f + k);

                        Vector3 point = new Vector3(pX, pY, pZ);
                        if (ColliderUtils.IsPointInsideCollider(point, this.collider, ref bounds))
                        {
                            voxelsCIV.Add(this.transform.InverseTransformPoint(point));
                        }
                    }
                }
            }

            this.transform.rotation = initialRotation;

            return voxelsCIV.ToArray();
        }
    }
}
