# Dynamic Weather and Interactable Grass Demo

**Unity version:** 2022.3.17f1

**This demo includes:**
- Dynamic wind
- Grass interaction
- Grass wind animation
- Precipitation with wind influence
- Dynamic wind manipulation
- Snow deformation
- Smoothness offsetting depending on rain

It uses a compute shader to generate wind flow map, using [OpenSimplex2](https://github.com/KdotJPG/OpenSimplex2), and write manipulation data over the texture. It also uses good old blitting operations to do snow and grass deformation.

VFX graph is used for precipitation and leaf visuals. Due to this [bug](https://issuetracker.unity3d.com/issues/vfx-cannot-sample-rendertexture-anymore), render textures can't be assigned to Sample Texture node in VFX graph, thus, 2022.3.15f1+ is needed. I am not sure if it is needed for older versions, as I have already assigned them, but one can bind the texture from VFXWeatherBinder class if need be.

**Areas to improve**
- Wind deformation parameters are not too working as expected, low power input results in unexpectedly sharp deformation
- Proper parameters for wind. Current wind parameters have parameters that make little sense, like "wind change power". It acts as frequency for z component of noise calculation, but a better approach might be more friendly
- Wind calculation for Y axis might provide better results, current implementation only uses XZ plane for wind, which leads to some loss of control especially over the percipitation particles' reaction to the wind.
- Wind calculation could definitely be improved.


## Attribution
- Some Foliage by [soidev](https://sketchfab.com/soidev) [source](https://sketchfab.com/3d-models/some-foliage-5e806681504a4642a16a59c057d34e8d) [CC BY 4.0](https://creativecommons.org/licenses/by/4.0/)
- Textures from [Poly Haven](https://polyhaven.com/textures) Creative Commons CC0
- Leaf texture from [ambientCG](https://ambientcg.com/) Creative Commons CC0
- Particle pack from [kenney](https://www.kenney.nl/) Creative Commons CC0
- OpenSimplex2 [source](https://github.com/KdotJPG/OpenSimplex2)
- bitangent_noise [source](https://github.com/atyuwen/bitangent_noise)
