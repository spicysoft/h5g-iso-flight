using Unity.Entities;
using Unity.Tiny.Core;
using Unity.Tiny.Debugging;
using Unity.Tiny.Scenes;
using Unity.Tiny.Text;
using Unity.Tiny.UIControls;

namespace IsoFlight
{
	public class BtnRetrySystem : ComponentSystem
	{
		protected override void OnUpdate()
		{
			bool btnOn = false;
			Entities.WithAll<BtnRetryTag>().ForEach( ( Entity entity, ref PointerInteraction pointerInteraction ) => {
				if( pointerInteraction.clicked ) {
					//Debug.LogAlways( "btn ret click" );
					btnOn = true;
				}
			} );


			if( btnOn ) {
				// リザルトシーンアンロード.
				SceneReference resultScn = new SceneReference();
				resultScn = World.TinyEnvironment().GetConfigData<GameConfig>().ResultScn;
				SceneService.UnloadAllSceneInstances( resultScn );

				resultScn = World.TinyEnvironment().GetConfigData<GameConfig>().BlockScn;
				SceneService.UnloadAllSceneInstances( resultScn );


#if false
				Entities.ForEach( ( ref GameMngr mngr ) => {
					mngr.Initialized = false;
				} );

				Entities.ForEach( ( ref BlockGenerateInfo info ) => {
					info.Initialized = false;
				} );

#else
				resultScn = World.TinyEnvironment().GetConfigData<GameConfig>().MainScn;
				SceneService.UnloadAllSceneInstances( resultScn );

				SceneReference mainScn = World.TinyEnvironment().GetConfigData<GameConfig>().TitleScn;
				SceneService.LoadSceneAsync( mainScn );
#endif
			}
		}
	}
}
