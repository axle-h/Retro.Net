[![CircleCI](https://circleci.com/gh/axle-h/Retro.Net/tree/master.svg?style=shield)](https://circleci.com/gh/axle-h/Retro.Net/tree/master)

# Retro.Net
Retro hardware libraries in .NET core.

## Running

To run a GameBoy on ASP.NET Core in a docker container:

    docker container run -p 2500:2500 alexhaslehurst/server-side-gameboy

Then browse to `localhost:2500`.

See the included `docker-compose` file for running this behind a secure reverse proxy. 

## Example

[See it running on Azure](https://gb.ax-h.com).

I'm using my MSDN license to run it in Azure on a Basic A1 instance (1 Core, 1.75 GB memory) so the performance isn't great and this isn't even guaranteed to be up 24/7 according to M$.

## Documentation

I have written a few blog posts about this project, please take a read:

1. [Emulation on .NET](https://ax-h.com/software/development/emulation/2017/11/25/emulation-on-dot-net.html)
2. [Emulating the GameBoy CPU on .NET](https://ax-h.com/software/development/emulation/2017/12/03/emulating-the-gameboy-cpu-on-dot-net.html)

Scott Hanselman has also [blogged about it](https://www.hanselman.com/blog/AMultiplayerServersideGameBoyEmulatorWrittenInNETCoreAndAngular.aspx).

## Features

* Z80 based CPU and MMU configurable for 8080, Z80 and GameBoy.
  * Simple interpreted core.
  * "Dynamically Re-compiling" core. Instead of executing each block of Z80 operations immediately, it builds an expression tree representing the block, which can be cached for increased speed. It's about 2.5x faster than the simple interpreted core. And far cooler.
* Complete Z80 instruction set decode tests.
* Complete Z80 execution tests.
* Integration tests for the GameBoy Blargg test roms.
* WIP GameBoy hardware. Just enough to play Tetris at full speed!
* An Angular user interface using websockets. See `gameboy-client`.

## Known issues

* Timing is completely broken on desktop Hyper-V (Windows 10) resulting in the emulation running way too fast. Docker for Windows runs on Hyper-V so it is also affected.

## TODO

* GameBoy sound.
* Improved GPU rendering to support games other than Tetris. E.g. transparent sprites, 8x16 sprites.
* Debugger.