# Usage
it's intended to be launched with command line arguments.

## Command line arguments

### Buttons:

- a
- b
- x
- y
- start
- back
- guide
- lb
- rb
- ls
- rs
- up
- down
- left
- right

### Parametrs:
- `-d`, `--duration`, `-t` or `--time` – how long the buttons are pressed. Default: 100 ms
- `-p` or --pause – pause before executing buttons press emulation.

### Example:
- `"Gamepad Emulation.exe" a lb -p 2000 -d 300` – wait 2 seconds, then press "A" button and Left Shoulder simultaniusly for 300 ms.
- `"Gamepad Emulation.exe" x` – press "X" button for 100 ms

### Credits
Uses Nefarius.ViGEm.NET for gamepad emulation
