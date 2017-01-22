using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Input
{
    interface InputInterface
    {
        void Update();

        bool ZoomIn();
        bool ZoomOut();
        float Rotate();
    }

    enum InputType
    {
        BinaryState,
        WentDown,
        WentUp,
        RangeZeroOne,
        RangeMinusOneOne
    }
}
