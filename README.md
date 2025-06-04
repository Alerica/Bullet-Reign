# Bullet Reign

**Bullet Reign** is a fast-paced **top-down shooter with roguelike elements**, developed to experiment with **Scriptable Objects in Unity**. Players will dodge enemy fire, collect powerful skills, and upgrade their character as they progress through procedurally generated levels.

## Scripts and Feature
Below are the commonly used scripts along with their descriptions. There are additional scripts in the project, but these are the core ones that define the general mechanics.
<table>
  <tr>
    <th>Feature</th>
    <th>Description</th>
    <th>Script</th>
  </tr>
  <tr>
    <td><b>Room Generation</b></td>
    <td>Procedurally generates rooms using a grid system and connects them logically.</td>
    <td>Room.cs, RoomManager.Cs, Door.cs, EnemySpawner.cs</td>
  </tr>
  <tr>
    <td><b>Combat System</b></td>
    <td>Handles shooting mechanics, enemy AI, and bullet patterns.</td>
    <td>EnemyAI.cs, UndeadAttack.cs, EnemyProjectile.cs, BossSetting.cs, FlyingAttack.cs</td>
  </tr>
  <tr>
    <td><b>Upgrade System</b></td>
    <td>Applies buffs after completing a stage.</td>
    <td>UpgradeData.cs</td>
  </tr>
  <tr>
    <td><b>Skill System</b></td>
    <td>Uses <b>Scriptable Objects</b> to manage different skill effects.</td>
    <td>Skill.cs, FireBallSkill.cs, IceBallSkill.cs, PoisonSkill.cs, VoidSkill.cs</td>
  </tr>
</table>

## How Room Generation Works
- The game starts with a **central room** and expands outward.  
- Rooms are placed based on a **queue system**, ensuring proper connectivity.  
- Each room is assigned a **grid position**, and adjacent rooms may be connected.  
- If the generated room count is too low, it **regenerates** to meet the minimum required rooms.  
- **Boss rooms** are placed last, ensuring a structured progression.  
- After all room are generated, opening each door inside the room

## Preview
<img src="https://github.com/Alerica/Alerica/blob/main/Bullet-Reign-Clip.gif" alt="2" style="width:100%;height:auto;">

## Controls
<table> 
   <tr>
     <th>Keybind</th>
     <th>Description</th>
   </tr>
   <tr>
     <td>WASD</td>
     <td>Move</td>
   </tr>
   <tr>
     <td>Left Click</td>
     <td>Shoot  </td>
   </tr>
   <tr>
     <td>1,2,3,4</td>
     <td>Use Skills </td>
   </tr>
   <tr>
     <td>M</td>
     <td>Toggle Map </td>
   </tr>
   <tr>
     <td>Tab</td>
     <td>Toggle Stats</td>
   </tr>
</table>

## Skills & Upgrades
- Enemies have a chance to drop **skills** that grant special abilities.  
- At the end of each stage, players can **choose an upgrade** to enhance their power.  
- Skills and Upgrade are both scriptable object

## Assets
### Character
https://momongaa.itch.io/16x16-sci-fi-shooter-tileset-dungeon-crawler

### Bullet & Skills
- https://bdragon1727.itch.io/fire-pixel-bullet-16x16
- https://pimen.itch.io/fire-spell-effect-02

### Skill
https://lordfitoi.itch.io/elementalis-skill

### UI
https://wenrexa.itch.io/ui-different03

### Sound
https://maygenko.itch.io/retro-8-bit-rpg-music-pack-by-may-genko

---

This project is mainly for **learning purposes** and serves as a stepping stone toward more advanced game development. 

---
