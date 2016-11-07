using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RoninUtils.Helper;

namespace RoninUtils.RoninFramework {

    public abstract class GameService {

        public virtual void Init() { }

        public virtual void Update() { }

        public virtual void FixedUpdate () { }

    }
}
