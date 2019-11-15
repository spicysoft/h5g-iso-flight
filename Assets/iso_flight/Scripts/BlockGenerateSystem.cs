using Unity.Entities;
using Unity.Tiny.Core;
using Unity.Tiny.Debugging;
using Unity.Tiny.Scenes;

namespace IsoFlight
{
	public class BlockGenerateSystem : ComponentSystem
	{
		private const int BallMax = 60;

		protected override void OnUpdate()
		{
			bool reqGen = false;
			bool isPause = false;

#if false
			Entities.ForEach( ( ref GameMngr mngr ) => {
				isPause = mngr.IsPause;
			} );
#endif
			if( isPause )
				return;


			Entities.ForEach( ( Entity entity, ref BlockGenerateInfo gen ) => {
				if( !gen.Initialized ) {
					gen.Initialized = true;
					gen.Timer = 0;
					reqGen = true;  // test
					return;
				}

				float dt = World.TinyEnvironment().frameDeltaTime;
				gen.Timer += dt;
				if( gen.Timer > 10f ) {
					gen.Timer = 0;
					reqGen = true;
				}

			} );

			if( reqGen ) {

				Entities.ForEach( ( ref InitBlockInfo info ) => {
					info.BlockCnt = 0;
					info.GeneretedNum = 8;
				} );

				Debug.LogAlways( "--reqgen" );

				for( int i = 0; i < 8; i++ ) {
					bool recycled = false;
					Entities.ForEach( ( Entity entity, ref BlockInfo block ) => {
						if( !recycled ) {
							if( !block.IsActive ) {
								block.IsActive = true;
								block.Initialized = false;
								recycled = true;
							}
						}
					} );

					//Debug.LogFormatAlways( "recycled {0}", recycled );

					if( !recycled ) {
						var env = World.TinyEnvironment();
						SceneService.LoadSceneAsync( env.GetConfigData<GameConfig>().BlockScn );
					}
				}
			}
		}
	}
}
