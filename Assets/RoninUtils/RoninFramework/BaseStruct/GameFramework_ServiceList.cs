using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoninUtils.RoninFramework {
    public partial class GameFramework {

        public static ServiceType GetService<ServiceType> () where ServiceType : GameService {
            return Instance.mGameServices[typeof(ServiceType)] as ServiceType;
        }

        private void AddService (GameService service) { mGameServices.Add(service.GetType(), service); }

        private void AddAllServices() {
            AddBasicServices();
            AddGameLogicService();
        }

        private void AddBasicServices() {
            AddService(new EventService());
            AddService(new ConfigService());
            AddService(new PrefabService());
            AddService(new PoolService());
        }


        private void AddGameLogicService() {

        }



    }
}
