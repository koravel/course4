using UnityEngine;
using UnityEngine.UI;

public class TestBuilderDirector : MonoBehaviour
{
    private ArcadeLevelBuilder builder = new ArcadeLevelBuilder();
    public Camera cam;
    public Image background;

	void Start ()
    {
        background.sprite = Resources.Load<Sprite>("Textures\\PlayerSprite64x64");
        builder.level = builder.BuildLevel("Testing_Area", "");
        builder.BuildPlayer(new PlayerModel(new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0), false, false, true, 2000, 2000, new float[] { -0.3f, 0.3f }, 1, 0.1f), ref cam);
        builder.BuildMeleeEnemy(new MeleeModel(new Vector3(80, 80, 0), new Quaternion(0, 0, 0, 0), false, false, false, 1000, 1500, 1, 30));
        builder.BuildRangeEnemy(new RangeModel(new Vector3(70, 70, 0), new Quaternion(0, 0, 0, 0), false, false, false, 1500, 1500, new float[] { -0.3f, 0.3f }, 2, 0.5f, 40, 50));
        builder.BuildWall(new WallModel(new Vector3(25, 25, 0), new Quaternion(0, 0, 0, 0), new Vector3(1, 15, 0)));
    }
}
