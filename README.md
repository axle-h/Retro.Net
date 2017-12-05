[![Build Status](https://travis-ci.org/axle-h/Retro.Net.svg?branch=master)](https://travis-ci.org/axle-h/Retro.Net)

# Retro.Net
Retro hardware libraries in .NET core.

To run a GameBoy on ASP.NET Core in a docker container:

    docker run -p 2500:2500 alexhaslehurst/server-side-gameboy

Then browse to `localhost:2500`.

See the included docker-compose file for running this behind a secure reverse proxy. 

[See it running on Azure](https://gb.ax-h.com)

I'm using my MSDN license to run it in Azure on a Basic A1 instance (1 Core, 1.75 GB memory) so the performance isn't great and this isn't even guaranteed to be up 24/7 according to M$.

Currently:

* Z80 based CPU and MMU configurable for 8080, Z80 and GameBoy.
  * Simple interpreted core.
  * "Dynamically Re-compiling" core. Instead of executing each block of Z80 operations immediately, it builds an expression tree representing the block, which can be cached for increased speed. It's about 2.5x faster than the simple interpreted core. And far cooler.
* Complete Z80 instruction set decode tests.
* Complete Z80 execution tests.
* Integration tests for the GameBoy Blargg test roms.
* WIP GameBoy hardware. Just enough to play Tetris at full speed!
* An Angular user interface using websockets. See `gameboy-client`.

TODO:
* GameBoy sound.
* GameBoy GPU window rendering. Background and sprites are enough to play Tetris for now.
* Debugger.