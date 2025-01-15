#ifndef UNITY_PLUGIN_H
#define UNITY_PLUGIN_H

extern "C"
{
    __declspec(dllexport) float AddNumbers(float a, float b);

    __declspec(dllexport) const char* GetGreeting();

    __declspec(dllexport) float* GetSquares(int count);

    __declspec(dllexport) double ApproximatePi(int iterations);
}

#endif
