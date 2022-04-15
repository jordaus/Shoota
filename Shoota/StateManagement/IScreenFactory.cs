using System;
using System.Collections.Generic;
using System.Text;

namespace Shoota.StateManagement
{
    public interface IScreenFactory
    {
        GameScreen CreateScreen(Type screenType);
    }
}
