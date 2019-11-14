using Unity.Entities;
using Unity.Tiny.Scenes;

namespace IsoFlight
{
	public struct GameConfig : IComponentData
	{
		public SceneReference TitleScn;
		public SceneReference MainScn;
		public SceneReference ResultScn;
		public SceneReference BlockScn;
		public SceneReference BlkShadowScn;
		//public SceneReference ScoreScn;
		public SceneReference GameOverScn;
	}
}
