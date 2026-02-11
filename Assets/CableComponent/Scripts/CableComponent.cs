using UnityEngine;
using System.Collections;

public class CableComponent : MonoBehaviour
{
    #region Class members
    [Header("Anchors")]
    [SerializeField] private Transform endPoint;
    [SerializeField] private Transform midPoint;
    [SerializeField] private Material cableMaterial;

    [Header("Breadboard Wire Config")]
    [SerializeField] private float cableLength = 2.0f;
    [SerializeField] private int totalSegments = 20; // High segments = smooth arch
    [SerializeField] private float cableWidth = 0.03f;

    [Range(0, 5)]
    [SerializeField] private float archHeight = 1.0f; // Push the wire UP
    [Range(0, 1)]
    [SerializeField] private float wireStiffness = 0.2f; // How hard it holds the arch

    [Header("Solver Settings")]
    [SerializeField] private int solverIterations = 5;

    private LineRenderer line;
    private CableParticle[] points;
    private int segments = 0;
    #endregion

    void Start()
    {
        InitCableParticles();
        InitLineRenderer();
    }

    void InitCableParticles()
    {
        segments = totalSegments;
        if (segments % 2 != 0) segments++;

        points = new CableParticle[segments + 1];
        int midIdx = segments / 2;

        // Initial placement
        for (int i = 0; i <= segments; i++)
        {
            float t = (float)i / segments;
            Vector3 pos = Vector3.Lerp(transform.position, endPoint.position, t);
            points[i] = new CableParticle(pos);
        }

        points[0].Bind(this.transform);
        points[midIdx].Bind(midPoint);
        points[segments].Bind(endPoint);
    }

    void InitLineRenderer()
    {
        line = GetComponent<LineRenderer>();
        if (line == null) line = gameObject.AddComponent<LineRenderer>();
        line.startWidth = cableWidth;
        line.endWidth = cableWidth;
        line.positionCount = segments + 1;
        line.material = cableMaterial;
        line.numCornerVertices = 8; // Makes the "bends" look round
    }

    void Update() => RenderCable();

    void FixedUpdate()
    {
        // 1. Verlet Step (No Gravity for breadboard wires, or very low gravity)
        foreach (CableParticle particle in points)
        {
            // We use a tiny bit of negative gravity to help the arch "float"
            Vector3 antiGravity = Vector3.up * archHeight * 0.1f;
            particle.UpdateVerlet(antiGravity * Time.fixedDeltaTime);
        }

        // 2. Constraints Step
        for (int i = 0; i < solverIterations; i++)
        {
            SolveDistanceConstraints();
            ApplyArchForce(); // This creates the "wire" shape
        }
    }

    void SolveDistanceConstraints()
    {
        float segmentLength = cableLength / segments;
        for (int i = 0; i < segments; i++)
        {
            SolveDistanceConstraint(points[i], points[i + 1], segmentLength);
        }
    }

    void SolveDistanceConstraint(CableParticle a, CableParticle b, float len)
    {
        Vector3 delta = b.Position - a.Position;
        float dist = delta.magnitude;
        if (dist <= 0.001f) return;
        float error = (dist - len) / dist;
        Vector3 correction = delta * error;

        if (a.IsFree() && b.IsFree()) { a.Position += correction * 0.5f; b.Position -= correction * 0.5f; }
        else if (a.IsFree()) a.Position += correction;
        else if (b.IsFree()) b.Position -= correction;
    }

    /**
	 * Pushes the wire into an arch shape
	 */
    void ApplyArchForce()
    {
        int midIdx = segments / 2;

        // Section 1: Start to Mid
        ApplyArchToSection(0, midIdx, transform.position, midPoint.position);
        // Section 2: Mid to End
        ApplyArchToSection(midIdx, segments, midPoint.position, endPoint.position);
    }

    void ApplyArchToSection(int start, int end, Vector3 startPos, Vector3 endPos)
    {
        int count = end - start;
        for (int i = 1; i < count; i++)
        {
            int idx = start + i;
            if (!points[idx].IsFree()) continue;

            float t = (float)i / count;
            // Calculate a Parabola height: h = 4 * H * t * (1 - t)
            float parabola = 4f * archHeight * t * (1f - t);

            Vector3 linearPos = Vector3.Lerp(startPos, endPos, t);
            Vector3 targetArchPos = linearPos + (Vector3.up * parabola);

            // Move the particle toward the arch
            points[idx].Position = Vector3.Lerp(points[idx].Position, targetArchPos, wireStiffness);
        }
    }

    void RenderCable()
    {
        for (int i = 0; i <= segments; i++) line.SetPosition(i, points[i].Position);
    }
}