using FMODUnity;

namespace RoninUtils.RoninFramework {


    public class AudioService : GameService {

        /**
         * 使用这个来工作即可
         */
        public RuntimeManager mFMODManager { get; private set; }

        public override void Init () {
            base.Init();
            mFMODManager = RuntimeManager.GetInstance();
        }

    }
}
