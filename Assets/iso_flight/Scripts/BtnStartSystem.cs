using Unity.Entities;
using Unity.Tiny.Core;
using Unity.Tiny.Debugging;
using Unity.Tiny.Scenes;
using Unity.Tiny.Text;
using Unity.Tiny.UIControls;

namespace IsoFlight
{
	public class BtnStartSystem : ComponentSystem
	{
		protected override void OnUpdate()
		{
			bool btnOn = false;
			Entities.WithAll<BtnStartTag>().ForEach( ( Entity entity, ref PointerInteraction pointerInteraction ) => {
				if( pointerInteraction.clicked ) {
					//Debug.LogAlways( "btn ret click" );
					btnOn = true;
				}
			} );


			if( btnOn ) {
				// タイトルシーンアンロード.
				SceneReference titleScn = new SceneReference();
				titleScn = World.TinyEnvironment().GetConfigData<GameConfig>().TitleScn;
				SceneService.UnloadAllSceneInstances( titleScn );

				SceneReference mainScn = World.TinyEnvironment().GetConfigData<GameConfig>().MainScn;
				SceneService.LoadSceneAsync( mainScn );
			}
		}
	}
}
