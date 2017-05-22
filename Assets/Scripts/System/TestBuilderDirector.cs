using UnityEngine;

public class TestBuilderDirector : MonoBehaviour
{
    ArcadeLevelBuilder builder = new ArcadeLevelBuilder();
    public Camera cam;

	void Start ()
    {
        builder.level = builder.BuildLevel("Testing_Area");
        builder.BuildPlayer(new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0), false, false, true, 2000, 2000, new float[] { -0.3f, 0.3f }, 2, 0.1f, ref cam);
        builder.BuildMeleeEnemy(new Vector3(20, 20, 0), new Quaternion(0, 0, 0, 0), false, false, false, 1000, 1500);
        builder.BuildRangeEnemy(new Vector3(10, 10, 0), new Quaternion(0, 0, 0, 0), false, false, false, 1500, 1500, new float[] { -0.3f, 0.3f }, 2, 0.5f, 40);
        builder.BuildWall(new Vector3(25, 25, 0), new Quaternion(0, 0, 0, 0), new Vector3(1, 15, 0));
    }
}
