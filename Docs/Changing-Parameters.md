# How to change parameters in the Brick-Building Simulator
You can change some parameters in the Brick-Building Simulator if they do not fit with your desired Serious Play experience. 

### Show a debugging grid
Follow these steps to create a debugging grid:
1. Open the _GridManager_ from the [BuildingScene](/Brick-Building-Simulator/Assets/Scenes/BuildingScene.unity) in the Inspector.
2. Check the _showDebug_ box.
3. A debugging grid will now be shown after restarting the scene.

### Changing the grid size
Follow these steps, to change the size of the grid:
1. Open the _GridManager_ from the [BuildingScene](/Brick-Building-Simulator/Assets/Scenes/BuildingScene.unity) in the Inspector.
2. Change the _size_ parameter.

### Change camera zoom speed and distance
Open the _InputManager_ from the [BuildingScene](/Brick-Building-Simulator/Assets/Scenes/BuildingScene.unity) in the Inspector.

Here you can change:
+ The _zoomSpeed_,
+ _minZoom_,
+ and _maxZoom_.

### Change how the inventory is being filled
Open the _InventoryRandomizer_ from the [BuildingScene](/Brick-Building-Simulator/Assets/Scenes/BuildingScene.unity) in the Inspector. (Can be found by _Managers_ in the Hierarchy). 
To generate the random inventory, a [normal distribution](https://en.wikipedia.org/wiki/Normal_distribution) is used.
Reading about [normal distribution](https://en.wikipedia.org/wiki/Normal_distribution) first can help understand the paramerters.

You can change the following parameters:
| Parameter Name | Meaning |
| - | - |
| meanNumberOfBlocksPerType | Mean number of blocky you will receive of each type. It is equivalent to the μ-parameter of a normal distribution. |
| standardDeviation | Corresponds to the randomness of the result. It is equivalent to the σ-parameter of a normal distribution. |
| minBlocks | Corresponds to the minimum number of blocks received per type. |
| maxBlocks | Corresponds to the maximum number of blocks received per type. |
| numberOfItemTypes | Sets how many items-types will be added to the inventory. Should __not__ be grater, then the total number of [Scriptable Objects](</Brick-Building-Simulator/Assets/Resources/Scriptable Objects/>) (BuildingBlocks). |

### Change block ghost (transparent block near mouse pointer) speed
Open the _GhostManager_ from the [BuildingScene](/Brick-Building-Simulator/Assets/Scenes/BuildingScene.unity) in the Inspector.

+ To change the speed at which the block ghost follows the mouse pointer, change the _repositionSpeed_.
+ To change the speed at which the block ghost rotates, change the _rotationSpeed_.