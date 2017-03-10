using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AseetBudle_JJ : MonoBehaviour
{

    AssetBundle assetbundle = null;
    void Start()
    {
        CreatImage(loadSprite("guvi"));
        CreatImage(loadSprite("Heavy_Armor_Chest_03.tex"));
    }

    private void CreatImage(Sprite sprite)
    {
        GameObject go = new GameObject(sprite.name);
        go.layer = LayerMask.NameToLayer("UI");
        go.transform.parent = transform;
        go.transform.localScale = Vector3.one;
        Image image = go.AddComponent<Image>();
        image.sprite = sprite;
        image.SetNativeSize();
    }

    private Sprite loadSprite(string spriteName)
    {
#if USE_ASSETBUNDLE
		if(assetbundle == null)
			assetbundle = AssetBundle.CreateFromFile(Application.streamingAssetsPath +"/weapon.assetbundle");
				return assetbundle.Load(spriteName) as Sprite;
#else
        return Resources.Load<GameObject>("Sprite/" + spriteName).GetComponent<SpriteRenderer>().sprite;
#endif
    }

}