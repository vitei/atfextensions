using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Sce.Atf.VectorMath;

namespace Vitei.ATFExtensions
{
    public static class ScreenSpaceExtensions
    {
        public static Ray3F GetRay(this Point scrPt, Matrix4F mtrx, Size controlSize)
        {
            Vec3F min = Unproject(new Vec3F(scrPt.X, scrPt.Y, 0), mtrx, controlSize);
            Vec3F max = Unproject(new Vec3F(scrPt.X, scrPt.Y, 1), mtrx, controlSize);
            Vec3F dir = Vec3F.Normalize(max - min);
            Ray3F ray = new Ray3F(min, dir);

            return ray;
        }

        public static Vec3F Unproject(this Vec3F scrPt, Matrix4F wvp, Size controlSize)
        {
            float width = controlSize.Width;
            float height = controlSize.Height;
            Matrix4F invWVP = new Matrix4F();
            invWVP.Invert(wvp);
            Vec3F worldPt = new Vec3F();
            worldPt.X = scrPt.X / width * 2.0f - 1f;
            worldPt.Y = -(scrPt.Y / height * 2.0f - 1f);
            worldPt.Z = scrPt.Z;

            float w = worldPt.X * invWVP.M14 + worldPt.Y * invWVP.M24 + worldPt.Z * invWVP.M34 + invWVP.M44;
            invWVP.Transform(ref worldPt);
            worldPt = worldPt / w;

            return worldPt;
        }
    }
}
