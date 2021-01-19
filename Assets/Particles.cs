using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles : MonoBehaviour {

    private struct Particle {
        public Vector3 position;
    }

    private int PARTICLE_SIZE = System.Runtime.InteropServices.Marshal.SizeOf(typeof(Particle));

    public int particleCount = 1000;
    
    [Range(1, 15)]
    public float radius = 1.0f;

    public Material particleMaterial;

    private Particle[] particles;
    private ComputeBuffer particleBuffer;

    private void Start() {
        particles = new Particle[particleCount];
        
        for (int i = 0; i < particleCount; ++i) {
            particles[i].position = Random.insideUnitSphere * radius;
        }

        particleBuffer = new ComputeBuffer(particleCount, PARTICLE_SIZE);
        particleBuffer.SetData(particles);

        particleMaterial.SetBuffer("particleBuffer", particleBuffer);
    }

    private void Update() {
        for (int i = 0; i < particleCount; ++i) {
            particles[i].position = Random.insideUnitSphere * radius;
        }

        particleBuffer.SetData(particles);
        }

    private void OnDestroy() {
        if (particleBuffer != null) particleBuffer.Release();
    }

    private void OnRenderObject() {
        particleMaterial.SetPass(0);
        Graphics.DrawProceduralNow(MeshTopology.Points, 1, particleCount);
    }

}
