// UnityPlugin.cpp
#include <iostream>

extern "C" {
    __declspec(dllexport) float AddNumbers(float a, float b) {
        return a + b;
    }

    __declspec(dllexport) const char* GetGreeting() {
        return "Hello from C++ Plugin!";
    }
}
