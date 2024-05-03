using static AA1_ParticleSystem;
using UnityEngine;
using System.Collections.Generic;
using System.Security.Cryptography;
//obligatori utilitzar quaternions per a aquesta activitat
//permetré excepcionalment utilitzar la classe System.Numerics.Quaternion, NO la UnityEngine.Quaternion
//https://learn.microsoft.com/en-us/dotnet/api/system.numerics.quaternion?view=net-8.0
//Aquells que creïn la seva pròpia estructura de quaternion dins de la carpeta Common_Delivery rebran
//un punt extra en la nota final d'aquesta activitat
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
        public Vector3C lastPosition;
        public Vector3C lastPosition2;
        public Vector3C size;
        public Vector3C euler;
        public Vector3C aVelocity;
        public float density;
        public CubeRigidbody(Vector3C _position, Vector3C _lastPosition, Vector3C _lastPosition2, Vector3C _size, Vector3C _euler, Vector3C _aVeclocity,float _density)
        {
            position = _position;
            lastPosition = _lastPosition;
            lastPosition2 = _lastPosition2;
            size = _size;
            euler = _euler;
            aVelocity = _aVeclocity;
            density = _density;
        }
    }
    public CubeRigidbody crb = new CubeRigidbody(Vector3C.zero, Vector3C.zero, Vector3C.zero, new(.1f,.1f,.1f), Vector3C.zero, Vector3C.zero, 100);

    public void GetOthersRigidbodysArray(AA2_Rigidbody[] allRigidbodies)
    {
        AA2_Rigidbody[] othersRigidbodys = new AA2_Rigidbody[allRigidbodies.Length - 1];
        int index = 0;
        for (int i = 0; i < allRigidbodies.Length; i++)
        {
            if (allRigidbodies[i] != this)
            {
                othersRigidbodys[index++] = allRigidbodies[i];
            }
        }
        // Aquest array conté els altres rigidbodys amb els quals podreu interactuar.
    }
    //L'array othersRigidbodys us retorna els altres cossos rígids en l'escena per poder implementar les col·lisions.

    public void Update(float dt)
    {
        crb.lastPosition2 = crb.lastPosition;
        crb.lastPosition = crb.position;
        float volume = crb.size.x * crb.size.y * crb.size.z;
        Vector3C force;
        force = settings.gravity * crb.density * volume;
        crb.position += force * dt;
        crb.euler += crb.aVelocity * dt;
        //VERLET:
        //crb.position = crb.lastPosition*2 - crb.lastPosition2 + settings.gravity * Mathf.Pow(dt, 2);


        for (int j = 0; j < settingsCollision.planes.Length; ++j)
        {
            Vector3C distanceVector = crb.position - settingsCollision.planes[j].NearestPoint(crb.position);
            float distance = distanceVector.magnitude;
            float factor = crb.size.x;
            bool collision = false;
            bool passed = false;
            if (distance <= factor * 2)
                passed = true;
            if (distance <= crb.size.x + factor)
                collision = true;
            if (collision)
            {
                //int counter = 2;
                //while (passed)
                //{
                //    crb.position = crb.lastPosition;
                //    //crb.position += crb.acceleration * dt / counter;
                //    crb.position += settings.gravity * dt / counter;
                //    distanceVector = crb.position - settingsCollision.planes[j].NearestPoint(crb.position);
                //    distance = distanceVector.magnitude;

                //    if (distance > 0)
                //        passed = false;
                //    else
                //        counter *= 2;
                //}
                ////Calcular componente normal
                //Vector3C direction = (crb.position - crb.lastPosition).normalized;
                //float vnMagnitude = Vector3C.Dot(direction, settingsCollision.planes[j].normal);
                ////float vnMagnitude = Vector3C.Dot(particles[i].acceleration, settingsCollision.planes[j].normal);
                //Vector3C vn = settingsCollision.planes[j].normal * vnMagnitude;
                //    //Calcular componente tangencial
                //Vector3C vt = direction - vn;
                ////Vector3C vt = particles[i].acceleration - vn;
                //    //Calcular nueva velocidad
                //Vector3C newVelocity = -vn + vt;

                //crb.position += newVelocity * force * dt;
                ////particles[i].AddForce(-(particles[i].acceleration));
                ////particles[i].AddForce(newVelocity * settings.bounce);

                collision = false;

            }
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
