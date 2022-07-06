# Research-Ravine
This project is the result of my master thesis about noise-based generation of 3D worlds for a VR flight simulation. The main goal was the creation of a VR game with a mostly procedurally generated world. To offer a more immersive experience for the players an [Icaros](https://www.icaros.com/en/products/icaros-pro "Website of Icaros") system is used. This allows players to control their virtual movements by shifting their point of gravity on the Icaros.

The overall story is set in a desert on a mostly unexplored planet. The player is part of a research/mining operation with the goal to collect mineral samples. The overall goal of the game is to collect as many samples as possible while flying through the randomly generated terrain. The main way of collecting samples is by being in proximity of the terrain. The longer and faster the player flys closely along the terrain the more samples they gather. Alternatively, the player can pick up certain items to increase their score.

The terrain itself uses a isosurface extraction method called [Dual Marchin Cubes](https://www.researchgate.net/publication/4112407_Dual_Marching_Cubes "Nielson's Dual Marching Cubes Paper"). The terrain is subdivided into a row of chunks to improve performance in combination with an octree-based LOD system. The chunk meshes themselves are created on the GPU using a compute shader. Thanks to the chunk-based approach the meshes of neighbouring chunks are not directly connected to each other. To fill this gap at the chunk seams a transition mesh is generated. The generation of these seam meshes also makes use of a compute shader to offload the calculation to the GPU.

Although performance-wise the terrain generation is acceptable for the most part, there are two major problems with my implementation.
One being the readback of the generated chunk mesh data. While the whole mesh generation is relatively fast, there is still a noticable delay thanks to the asynchronous nature of the readback. This occasionally leads to situations where updates to the LOD level of the terrain are quite noticable.
The second major problem is the seam mesh generation. Although it works similarly well to the generation of the actual chunk meshes, there are relatively rare but still existing instances where triangles in the seam mesh are not generated correctly, which in turn leads to holes in the terrain. Instead, a perhaps better approach could be the adaption of the method used in [Lengyel's Transvoxel algorithm](https://transvoxel.org/Lengyel-VoxelTerrain.pdf "Website of the Transvoxel algorithm").



![Screenshot 1 of Research Ravine](https://github.com/DennisVidal/Research-Ravine/blob/main/Screenshots/Screenshot1.png)

![Screenshot 2 of Research Ravine](https://github.com/DennisVidal/Research-Ravine/blob/main/Screenshots/Screenshot2.png)

![Screenshot 3 of Research Ravine](https://github.com/DennisVidal/Research-Ravine/blob/main/Screenshots/Screenshot3.png)

![Screenshot 4 of Research Ravine](https://github.com/DennisVidal/Research-Ravine/blob/main/Screenshots/Screenshot4.png)

![Screenshot 5 of Research Ravine](https://github.com/DennisVidal/Research-Ravine/blob/main/Screenshots/Screenshot5.png)

![Screenshot 6 of Research Ravine](https://github.com/DennisVidal/Research-Ravine/blob/main/Screenshots/Screenshot6.png)

![Screenshot 7 of Research Ravine](https://github.com/DennisVidal/Research-Ravine/blob/main/Screenshots/Screenshot7.png)
