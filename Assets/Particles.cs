using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles : MonoBehaviour {

    private struct Particle {
        public Vector3 position;
    }

    private int PARTICLE_SIZE = System.Runtime.InteropServices.Marshal.SizeOf(typeof(Particle));

    public int particleCount = 1000;

    [Range(1, 10000)]
    public float radius = 1.0f;

    [Range(0.01f, 3f)]
    public float amplitude = 1.0f;
    
    [Range(0.01f, 3f)]
    public float wavelength = 1.0f;

    private int dimension;

    public Material particleMaterial;

    private Particle[] particles;
    private ComputeBuffer particleBuffer;

    private void Start() {
        dimension = Mathf.FloorToInt(Mathf.Sqrt(particleCount));
        if (dimension < Mathf.Sqrt(particleCount))
            particleCount -= 1;
        
        particles = new Particle[particleCount];
        
        for (int i = 0, z = 0; z < dimension; ++z) {
            float v = (z + 0.5f) * (2.0f / particleCount) * radius;
            for (int x = 0; x < dimension; ++x, ++i) {
                float u = (x + 0.5f) * (2.0f / particleCount) * radius;
                particles[i].position = new Vector3(u, 0, v);
                }
        }

        particleBuffer = new ComputeBuffer(particleCount, PARTICLE_SIZE);
        particleBuffer.SetData(particles);

        particleMaterial.SetBuffer("particleBuffer", particleBuffer);
    }

    private void Update() {
        for (int i = 0, z = 0; z < dimension; ++z) {
            float v = (z + 0.5f) * (2.0f / particleCount) * radius;
            for (int x = 0; x < dimension; ++x, ++i) {
                float u = (x + 0.5f) * (2.0f / particleCount) * radius;
                particles[i].position.x = u;
                particles[i].position.y = Mathf.Sin(Mathf.PI * (u + Time.time) * wavelength) * amplitude;
                particles[i].position.y += Mathf.Sin(Mathf.PI * (v + Time.time) * wavelength) * amplitude;
                particles[i].position.z = v;
                }
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
