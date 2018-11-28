using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Core {
    public interface IClosed {
        event EventHandler<CancelEventArgs> Closed;
    }
    public interface ICanClosed {
        bool CanClosed();
    }
}
