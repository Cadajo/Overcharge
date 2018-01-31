# GGJ2018

Overcharged is a retro-futuristic racing game inspired by the 80s neon theme.

Grab the controls of your racing pod and compete against your friends through lan connection in a frenetic and crazy race.

## How To Play

Ships have an energy tank and four thrusters. Energy can be transferred from the energy tank to the thrusters, and viceversa. The more energy a thruster has, the bigger its thrust. Each thruster has a pair of keys associated to it: there's one to increase its energy and one to decrease its energy. 

- <kbd>Q</kbd>/<kbd>A</kbd>: Top Left Thruster.
- <kbd>W</kbd>/<kbd>S</kbd>: Bottom Left Thruster.
- <kbd>E</kbd>/<kbd>D</kbd>: Bottom Right Thruster.
- <kbd>R</kbd>/<kbd>F</kbd>: Top Right Thruster.

The energy tank has a capacity of 4 units of energy. Each thruster has a capacity of 2 units of energy, and it becomes overcharged when it holds more than 1 unit of energy. When a thruster is overcharged, it becomes harder to predict its thrust, and it also suffers damage proportional to the amount of overcharge. Distributing the 4 points of energy among the 4 thrusters gets you 1 energy point in each thruster, which is just below the overcharge threshold.

Collisions may damage the thrusters, lowering their energy capacity, and their max thrust as a consequence. Completely damaging a thruster will destroy it and detach it from the vehicle (unimplemented). If the thruster is not completely damaged, it can be repaired by hovering over the recovery bay at the end of the lap.

Despite the fact that vehicles start with 4 units of energy in its system, it is possible for the system to hold more than 4 units of energy, which increases its max thrust potential. The same recovery bay that repairs the thrusters also fills your energy tank, unless its full. Therefore, if you leave space in your energy tank by transferring your energy to the thrusters, the recovery bay will cause your system to go over the starting amount of energy. It is possible to have all your four thrusters simultaneously overcharged. Good luck!
