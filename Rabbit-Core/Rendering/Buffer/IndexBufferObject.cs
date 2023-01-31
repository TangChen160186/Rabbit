using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;

namespace Rabbit_Core.Rendering.Buffer
{
    public class IndexBufferObject : IDisposable
    {
        private readonly int _id;
        public IndexBufferObject(uint[] indices)
        {
            _id = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer,_id);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint),indices,BufferUsageHint.StaticDraw);
        }

        public void Bind()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _id);
        }

        public void Unbind()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        }


        private void ReleaseUnmanagedResources()
        {
            GL.DeleteBuffer(_id);
        }

        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }

        ~IndexBufferObject()
        {
            ReleaseUnmanagedResources();
        }
    }
}
