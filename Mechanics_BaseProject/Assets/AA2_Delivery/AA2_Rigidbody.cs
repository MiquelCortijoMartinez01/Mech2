using static AA1_ParticleSystem;
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class AA2_Rigidbody
{
    [System.Serializable]
    public struct Settings
    {
        public uint nCubos;
        public Vector3C gravity;
        public float bounce;
    }
    public Settings settings;

    [System.Serializable]
    public struct SettingsCollision
    {
        public PlaneC[] planes;
    }
    public SettingsCollision settingsCollision;


    [System.Serializable]
    public struct CubeRigidbody
    {
        
        public Vector3C position;
        public Vector3C size;
        public Vector3C euler;
        public CubeRigidbody(Vector3C _position, Vector3C _size, Vector3C _euler)
        {
            position = _position;
            size = _size;
            euler = _euler;
        }
    }
    public CubeRigidbody crb = new CubeRigidbody(Vector3C.zero, new(.1f,.1f,.1f), Vector3C.zero);
    CubeRigidbody[] cubeRList = null;
    
    public void Update(float dt)
    {
        cubeRList = new CubeRigidbody[settings.nCubos];
        for (int i = 0; i < cubeRList.Length; ++i)
        {
            //cubeRList[i].AddForce(settings.gravity * dt);
            //cubeRList[i].lastPosition = particles[i].position;
            //cubeRList[i].position += cubeRList[i].acceleration * dt;
            cubeRList[i].position += settings.gravity;
            

            //for (int j = 0; j < settingsCollision.planes.Length; ++j)
            //{
            //    Vector3C distanceVector = particles[i].position - settingsCollision.planes[j].NearestPoint(particles[i].position);
            //    float distance = distanceVector.magnitude;
            //    float factor = particles[i].size;
            //    bool collision = false;
            //    bool passed = false;
            //    if (distance <= factor * 2)
            //        passed = true;
            //    if (distance <= particles[i].size + factor)
            //        collision = true;
            //    if (collision)
            //    {
            //        int counter = 2;
            //        while (passed)
            //        {
            //            particles[i].position = particles[i].lastPosition;
            //            particles[i].position += particles[i].acceleration * dt / counter;
            //            distanceVector = particles[i].position - settingsCollision.planes[j].NearestPoint(particles[i].position);
            //            distance = distanceVector.magnitude;

            //            if (distance > 0)
            //                passed = false;
            //            else
            //                counter *= 2;
            //        }
            //        //Calcular componente normal
            //        float vnMagnitude = Vector3C.Dot(particles[i].acceleration, settingsCollision.planes[j].normal);
            //        Vector3C vn = settingsCollision.planes[j].normal * vnMagnitude;
            //        //Calcular componente tangencial
            //        Vector3C vt = particles[i].acceleration - vn;
            //        //Calcular nueva velocidad
            //        Vector3C newVelocity = -vn + vt;

            //        particles[i].AddForce(-(particles[i].acceleration));
            //        particles[i].AddForce(newVelocity * settings.bounce);


            //        collision = false;

            //    }
            //}
            
            
        }
    }

    public void Debug()
    {
        foreach (var item in settingsCollision.planes)
        {
            item.Print(Vector3C.red);
        }
    }
}
