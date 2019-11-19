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

#if true
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

				int[] ptn = BlockPattern.Tbl[0];
				int num = 0;
				for( int i = 0; i < ptn.Length; ++i ) {
					if( ptn[i] != 0 )
						num++;
				}


				Entities.ForEach( ( ref InitBlockInfo info ) => {
					info.BlockCntr = 0;
					info.GeneretedNum = num;
				} );

				//Debug.LogAlways( "--reqgen" );

				for( int i = 0; i < num; i++ ) {
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
