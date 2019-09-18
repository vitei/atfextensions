using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Vitei.ATFExtensions
{
    public static class RectangleExtensions
    {

        public static Point GetCenter(this Rectangle p_rect)
        {
            return new Point(
                p_rect.X + (p_rect.Width / 2),
                p_rect.Y + (p_rect.Height / 2)
            );
        }
    }
}
