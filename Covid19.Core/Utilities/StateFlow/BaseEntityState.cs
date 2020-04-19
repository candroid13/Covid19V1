using Covid19.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Covid19.Core.Utilities.StateFlow
{
    [Serializable]
    public abstract class BaseEntityState
    {
        public abstract IEnumerable<Func<StateParams>> Create();

        public Func<StateParams> Allow(Func<bool> predicate, Func<StateParams> action)
        {
            return predicate() ? action : null;
        }
        public StateParams Run()
        {
            return (from action in Create() where action != null select action()).FirstOrDefault();
        }
    }
}
