using Unity.Entities;
using Unity.Tiny.Core;
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
					reqGen = true;
				}

				float dt = World.TinyEnvironment().frameDeltaTime;


			} );

			if( reqGen ) {
				for( int i = 0; i < 12; i++ ) {
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

					//Debug.LogFormatAlways( "bulcnt {0} recycled {1}", bulCnt, recycled );

					if( !recycled ) {
						var env = World.TinyEnvironment();
						SceneService.LoadSceneAsync( env.GetConfigData<GameConfig>().BlockScn );
					}
				}
			}
		}
	}
}
