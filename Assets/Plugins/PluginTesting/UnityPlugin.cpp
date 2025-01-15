// UnityPlugin.cpp
#include "UnityPlugin.h"
#include <iostream>
#include <string>
#include <vector>

float AddNumbers(float a, float b)
{
    return a + b;
}

const char* GetGreeting()
{
    std::string greeting = "Hello from C++!";
    static std::string persistentGreeting = greeting;
    return persistentGreeting.c_str();
}

float* GetSquares(int count)
{
    static std::vector<float> squares;
    squares.clear();
    for (int i = 0; i < count; i++)
    {
        squares.push_back(static_cast<float>(i * i));
    }
    return squares.data();
}

double ApproximatePi(int iterations)
{
    double pi = 0.0;
    double numerator = 4.0;
    double denominator = 1.0;
    bool subtract = false;

    for (int i = 0; i < iterations; ++i)
    {
        if (subtract)
        {
            pi -= numerator / denominator;
        }
        else
        {
            pi += numerator / denominator;
        }
        subtract = !subtract;
        denominator += 2.0;
    }

    return pi;
}
