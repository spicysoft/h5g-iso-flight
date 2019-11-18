using Unity.Entities;
using Unity.Tiny.Core;
using Unity.Tiny.Scenes;

namespace IsoFlight
{
	public class EffExplSystem : ComponentSystem
	{
		protected override void OnUpdate()
		{
			var deltaTime = World.TinyEnvironment().frameDeltaTime;
			Entity delEntity = Entity.Null;

			Entities.ForEach( ( Entity entity, ref EffExplInfo eff ) => {
				if( !eff.Initialized )
					return;

				eff.Timer += deltaTime;
				if( eff.Timer > 0.5f ) {
					delEntity = entity;
				}
			} );

			if( delEntity != Entity.Null ) {
				SceneService.UnloadSceneInstance( delEntity );
			}
		}
	}
}
