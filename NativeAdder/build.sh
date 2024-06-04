docker build -t nativeadder .
docker create --name nativeadder_container nativeadder
docker cp nativeadder_container:/app .
docker rm nativeadder_container

echo "Building the cpp file"
aarch64-linux-gnu-g++ -o ./main main.cpp

echo "Copying everything to qemu"
scp NativeAdder.so root@192.168.7.2:/usr/share/lib-test/
