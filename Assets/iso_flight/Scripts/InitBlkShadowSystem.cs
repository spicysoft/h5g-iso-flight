using Unity.Entities;
using Unity.Mathematics;
using Unity.Tiny.Core;
using Unity.Tiny.Core2D;

namespace IsoFlight
{
	[UpdateAfter( typeof( BlockGenerateSystem ) )]
	public class InitBlkSystem : ComponentSystem
	{
		protected override void OnUpdate()
		{
#if false
			Entities.ForEach( ( ref BlkShadowInfo shadow, ref WorldPosInfo info, ref Translation trans, ref NonUniformScale scl ) => {
				if( !shadow.IsActive )
					return;
				if( shadow.Initialized )
					return;

				shadow.Initialized = true;
				scl.Value.x = 1f;



			} );
#endif
		}
	}
}
