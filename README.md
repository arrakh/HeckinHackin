# HeckinHackin
HeckinHackin is a game where you gather as many points either by killing monsters, or stealing other player's points!
![Gameplay Preview](https://imgur.com/a/3wWVONt)
Walk with WASD, run with Shift, and attack with Enter!

This project is made with Unity (2019.4.7f1) using Mirror with KcP Transport.

## Architecture Overview
![Architecture](https://user-images.githubusercontent.com/33478892/124793269-03567780-df78-11eb-923e-4d60468d5262.jpg)

In essence, HeckinHackin's Architecture relies on the RPGNetworkManager that is derived from Mirror's NetworkManager, and NetworkIdentity that will help sync various variables such as the Transform or Animator via serialization with the help of NetworkManager. Below are the breakdown for each essential class listed in the picture above:

* RPGNetworkManager: Derives from Mirror's base NetworkManager. Controls player creation when after joining the lobby and assigns the player data. Not only that, it also controls server-authoritative monster spawning so everyone can have the same monster spawn. More info regarding mirror's base NetworkManager can be found [here](https://mirror-networking.com/docs/api/Mirror.NetworkManager.html)
* VFXManager: Monobehaviour script that spawns in floating text, blood splatter, and many more. Only activated locally.
* AudioManager: Monobehaviour script that plays BGM and One-shot SFXs. Can also spawn in 3D Audio. Only activated locally.
* MonsterSpawner: MonoBehaviour script that spawns monsters on a random location in a radius, visualized by Unity's Gizmo. Only activated on the Server.
* Player Prefab:
  * PlayerScript: Controls player movement and combat. Also stores Player Data from RPGNetworkManager and basic information like Health, Stamina, and Coin.
* Enemy Prefab:
  * EnemyScript: Controls enemy health and function when it dies.
  * EnemyWander: Small AI script that makes enemies wander around aimlessly
* Mirror Network Components:
  * NetworkIdentity: The identifier of the object that lets the server be aware of the object and its change (if the local player has authority). More info can be found [here](https://mirror-networking.gitbook.io/docs/components/network-identity)
  * NetworkTransform: Syncs the transform (Position, Rotation, Scale) of the object to the server and all the clients.
  * NetworkAnimator: Syncs Unity's Animator changes accross clients.

## Credits
Thank you to Phantaminum for the awesome Title BGM, TinyWorld for the game's Ambience, and to my colleagues and professors in Electronic Engineering Polytechnic Institute of Surabaya for supporting me.

