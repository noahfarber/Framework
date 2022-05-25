using UnityEditor;

public class SpriteImporter : AssetPostprocessor // AssetPostprocessor gives us a bunch of tools we can use to modifiy assets on import
{
    // This hooks in to texture Preprocessing. that means it happens *before* the texture is processed by unity so we can fiddle with its import settings!
    // This method will be run on ever asset as it is imported. For example, whenever a new png file is added to the project or if you right click -> reimport on a texture asset.
    void OnPreprocessTexture()
    {
        //if (assetPath.Contains("UI") || assetPath.Contains("Sprite")) // Optional filtering. We can set it to only effect files with certain keywords in the path. Uncomment if useful
        {
            TextureImporter textureImporter = (TextureImporter)assetImporter; // create a TextureImporter object, this gives us tools to let us tweak the import settings on the current asset

            if (textureImporter.textureType != TextureImporterType.Sprite) // if the type is already sprite, dont run this test so we dosnt screw existing settings
            {
                textureImporter.textureType = TextureImporterType.Sprite; // Turns the texture into a sprite..
                textureImporter.spriteImportMode = SpriteImportMode.Single; // .. lets assume that each sprite is singular. if its a sprite sheet you have to set that up yourself the normal way

                textureImporter.mipmapEnabled = false; // Turn off mip maps because they are *gross* 

                textureImporter.textureFormat = TextureImporterFormat.AutomaticTruecolor; // and set compression mode to true colour cos it looks pretty~
            }
        }
    }
}