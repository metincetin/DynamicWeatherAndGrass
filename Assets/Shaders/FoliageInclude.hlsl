
void DecodeFlowmap_float(in float4 FlowMap, out float2 Out)
{
    Out = FlowMap.xy * 2 - 1;
}

void DistortionFalloff_float(in float ZDistance, out float Falloff)
{
    float d = 1 + abs(ZDistance);

    Falloff = 1 / pow(d, 10);
}

void ClampMagnitude_float(in float4 Vector, in float Magnitude, out float4 Clamped)
{
    float clamped = clamp(length(Vector), 0, Magnitude);
    Clamped = normalize(Vector) * Magnitude;
}