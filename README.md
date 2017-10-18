[![Build Status](https://travis-ci.org/axle-h/Retro.Net.svg?branch=master)](https://travis-ci.org/axle-h/Retro.Net)

# Retro.Net
Retro hardware libraries in .net core

Currently:

* Z80 based CPU & MMU configurable for 8080, Z80 and GameBoy.
  * Simple interpreted core.
  * "Dynamically Re-compiling" core. Instead of executing each block of Z80 operations immedietely, it builds an expression tree representing the block, which can be cached for increased speed. It's about 2.5x faster than the simple interpreted core. And far cooler.
* Complete Z80 instruction set decode tests.
* Complete Z80 execution tests.
* Integration tests for the Gameboy Blargg test roms.
* WIP GameBoy hardware. Just enough to play Tetris at full speed!

TODO:
* Front end? I've been using a full fat WPF app but thinking something more .net core. Maybe websockets.
* Gameboy sound.
* Gameboy GPU window rendering. Background and sprites are enough to play Tetris for now.
* Debugger.