using Unity.Entities;
using Unity.Tiny.Core;
using Unity.Tiny.Core2D;
using Unity.Mathematics;
using Unity.Tiny.Debugging;
using Unity.Tiny.Text;

namespace IsoFlight
{
	public class CameraSystem : ComponentSystem
	{
		protected override void OnUpdate()
		{
			float vsize = 0;
			float hsize = 0;

			Entities.ForEach( ( ref CameraInfo info, ref Camera2D camera ) => {
				if( !info.Initialized ) {

					// ディスプレイ情報.
					var displayInfo = World.TinyEnvironment().GetConfigData<DisplayInfo>();
					float frameW = (float)displayInfo.frameWidth;
					float frameH = (float)displayInfo.frameHeight;
					float frameAsp = frameH / frameW;

					// カメラ情報.
					float rectW = 750f;
					float rectH = 1110f;
					float rectAsp = rectH / rectW;

					camera.halfVerticalSize *= frameAsp / rectAsp;

					vsize = frameW;
					hsize = frameH;

					//Debug.LogFormat( "----	halfvert {0}", camera.halfVerticalSize );
					//Debug.LogFormat( "----	frame {0} buffer {1}", displayInfo.frameWidth, displayInfo.framebufferHeight );

					info.Initialized = true;
					return;
				}
			} );


			/*if( vsize > 0 ) {
				Entities.WithAll<DebTextTag>().ForEach( ( Entity entity ) => {
					EntityManager.SetBufferFromString<TextString>( entity, vsize.ToString()+", "+hsize.ToString() );
				} );
			}*/
		}
	}
}
