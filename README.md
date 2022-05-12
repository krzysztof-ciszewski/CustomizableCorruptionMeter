# CustomizableCorruptionMeter

Change position and opacity of the corruption meter of void fiend

Default configuration changes the meter position overlay the crosshair and changes the opacity to 30%

I might add config hot reload to ease the customization process, but for now the console command doesnt't work.

![Default uncorrupted](https://raw.githubusercontent.com/krzysztof-ciszewski/CustomizableCorruptionMeter/main/screenshot1.jpg)

![Default corrupted](https://raw.githubusercontent.com/krzysztof-ciszewski/CustomizableCorruptionMeter/main/screenshot2.jpg)


## Configuration
### Position
Vector3 with the position of the meter
By playing around with the UnityExplorer I found coordinates to overlay the crosshair to be:
`{"x":147.3701934814453,"y":-59.221500396728519,"z":1.0}`

### DisableCorruptionText
Bool whether to hide the percentage text

### MeterOpacity
Float from 0 to 1, where 0 is transparent and 1 is fully opaque

## Changelog

**1.0.2**

* made the mod client only

**1.0.1**

* added dll to the thunderstore package, updated readme

**1.0.0**

* Initial release - (missing dll on thunderstore)
