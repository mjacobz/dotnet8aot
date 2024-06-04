#include <iostream>
#include <dlfcn.h>

typedef int (*AddFunc)(int, int);

int main() {
    // Load the shared library
    void* handle = dlopen("/usr/lib64/apci/NativeAdder.so", RTLD_LAZY);
    if (!handle) {
        std::cerr << "Cannot open library: " << dlerror() << '\n';
        return 1;
    }

    // Load the symbol
    AddFunc add = (AddFunc)dlsym(handle, "Add");
    const char* dlsym_error = dlerror();
    if (dlsym_error) {
        std::cerr << "Cannot load symbol 'Add': " << dlsym_error << '\n';
        dlclose(handle);
        return 1;
    }

    // Use the function
    int result = add(3, 4);
    std::cout << "The result is: " << result << '\n';

    // Close the library
    dlclose(handle);
    return 0;
}