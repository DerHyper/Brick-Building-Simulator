# How to run the Brick-Building Simulator

You can either run a release version of the Brick-Building Simulator or build the newest version using Unity.

## Run a release version

This can be done by following these simple steps.

### 1. Download
All release can be found [here](https://github.com/DerHyper/Brick-Building-Simulator/releases/).

Simply click on the version you want to download (e.g., _Win64.zip_ for 64 Bit, Windows 7 or newer). It is recommended to download the newest stable release.


### 2. Unzip
Unzip the downloaded file using a program like [7-Zip](https://7-zip.com/download.html) or [WinRAR](https://www.rarlab.com/download.htm).

### <a name="run"></a> 3. Run

To run the Brick-Building Simulator, simply execute the binary file located inside the directory. It should be called _Brick-Building-Simulator.exe_, or similarly, depending on your platform.

>**_Note:_** When using __Linux__ it is also nessecary to make the file executable. \
To do this, simply open a new terminal inside the directory and write `chmod +x Linux.x86_64`.

>**_Note:_** If you want to use the __WebGL__ version on your __browser__ you need to hoast a server. Using [Python](https://www.python.org/) you can start a local server with `python -m http.server --cgi 8080` inside the WebGL directory. You can then access it via [http://localhost:8080/index.html](http://localhost:8080/index.html). A detaild video tutorial can be found [here](https://www.youtube.com/watch?v=Ceqbmm7ydS8).

## Build the newest version
If you want to use the newest version of the Brick-Building Simulator on a platform of your choice, you first need to build it. A tutorial on how to do this can be found [here](/Docs/How-To-Build.md).

You can then run the newly build version like described in Section [3. Run](#run)
