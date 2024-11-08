# Final Project Proposal: XR Arcade

*Invasion: Survival arcade-style shoot-em-up game*

*Nathan Khazam*

## Project Description

This project is inspired by Terminator Salvation with the goal of making an exciting arcade-style shooter. The game will move the player from location to location after they kill all of the enemies and give them a variety of weapons to use as the game continues. There will be distinct sections (beginning, middle, end) as well as different difficulty modes (if I have time). There are 4 distinct weapons: pistol, smg, shotgun, and grenade launcher and 2 enemies: drone and bug. Each level will increase in difficulty and the player will die if they are hit by any enemy (maybe 1 shot or maybe a little bit of health, will determine during level design). This game should be engaging and run in the 3-5 minute time frame.

## Feature Breakdown

1. **Gunplay:** There will be 4 weapons and each one should have: bullets, shells, shooting particles, and (maybe) muzzle flash to light things up and ammo system.
    - **Estimated Challenge - 3:** The bullets, shells, and shooting particles weren't too hard to implement but the muzzle flash, ammo system, and lighting system may take some time due to complexity, game balance, and optimization
2. **Enemies:** There will be 2 enemies that can move towards the player. I may make the drone also shoot the player (depending on game balance and health). Each enemy will have a certain amount of health and take different damage from different bullet types from different guns
    - **Estimated Challenge - 3:** The basic mechanics of the enemy should be quite simple but keeping track of how many there are may pose a challenge. Adding shooting to the enemy shouldn't be that bad either but will depend on game balance. The health is in the same boat in terms of will be figured out when level designing
3. **Level Design:** Designing a system such that the player moves smoothly between zones and the enemies aren't too easy or hard for the different section. Also need to balance the weapons and decide if the player can keep all of them or just 1 in each hand and where they would unlock new weapons.
    - **Estimated Challenge - 5:** Due to how early I am in the project, this will take quite some time. I need to figure out a general layout before moving onto adding the enemies and creating a flow/balanced game. Additionally, I'm currently still figuring out the movement which may take me a couple of days to do. However, I feel that this will be surmountable and I will most liekly be spending the last few weeks on this.

## Milestones

1. **By 11/15** - Have at least 1 weapon fully implemented (shooting, bullets, shells, sound, particles), 1 enemy fully implemented (moving, health, spawning, global tracker of how many are left), and player movement implemented
2. **By 11/19** - Build a demo level within the city to see if everything flows correctly of if I need to change some things. Potentially add more weapons as well and enemies for the demo to see how testers react.
3. **By the end of Thanksgiving Break** - Have a general idea of game layout and flow so that it can be implemented after break
4. **By 12/5** - Have basic layout setup and simple gameplay done and make sure the flow works
5. **By 12/13** - Have game basically done in terms of flow, gameplay and balance
6. **By showcase* - Finalize game balance and flow

## Inspirations

The main inspirations for this game were:
**Terminator Salvation:**
    - *Link:* https://rawthrills.com/games/terminator-salvation/
    - I want my game to flow very similar to that game where the player gets new weapons and faces enemies while being moved around the game automatically instead of having to move themselves.
**Pistol Whip::**
    - *Link:* https://www.meta.com/experiences/pistol-whip/2104963472963790/
    - Another shoot-em-up vr game that has some element I really liked (such as limited ammo). Its also a nice reference point for things that I can add incase I finish the game early or need to switch directions.