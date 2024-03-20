using System;
using System.Threading.Tasks;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class AA2_Cloth
{
    [System.Serializable]
    public struct Settings
    {
        public Vector3C gravity;
        [Min(1)]
        public float width;
        [Min(1)]
        public float height;
        [Min(2)]
        public int xPartSize;
        [Min(2)]
        public int yPartSize;
    }
    public Settings settings;
    [System.Serializable]
    public struct ClothSettings
    {
        public float structuralElasticCoef;
        public float structuralDampCoef;

        public float structuralSpring;
    }
    public ClothSettings clothSettings;

    [System.Serializable]
    public struct SettingsCollision
    {
        public SphereC sphere;
    }
    public SettingsCollision settingsCollision;
    public struct Vertex
    {
        public Vector3C lastPosition;
        public Vector3C actualPosition;
        public Vector3C velocity;
        public Vertex(Vector3C _position)
        {
            this.actualPosition = _position;
            this.lastPosition = _position;
            this.velocity = new Vector3C(0, 0, 0);
        }

        public void Euler(Vector3C force, float dt)
        {
            lastPosition = actualPosition;
            velocity += force * dt;
            actualPosition += velocity * dt;
        }
    }
    public Vertex[] points;
    public void Update(float dt)
    {
        int totalVertices = settings.xPartSize + 1;
        for (int i = settings.xPartSize + 1; i < points.Length; i++)
        {
            
            float magnitudeY = (points[i - totalVertices].actualPosition
                - points[i].actualPosition).magnitude - clothSettings.structuralSpring;//Falta distancia
            Vector3C forceVector = (points[i - totalVertices].actualPosition
                - points[i].actualPosition).normalized * magnitudeY;

            Vector3C dampingForce = (points[i].velocity - points[i - totalVertices].velocity  * clothSettings.structuralDampCoef);
            Vector3C structuralYSpringForce = (forceVector * clothSettings.structuralElasticCoef) - dampingForce;

            points[i].Euler(settings.gravity + structuralYSpringForce, dt);
        }
    }

    public void Debug()
    {
        settingsCollision.sphere.Print(Vector3C.blue);

        if (points != null)
            foreach (var item in points)
            {
                item.lastPosition.Print(0.05f);
                item.actualPosition.Print(0.05f);
            }
    }
}
