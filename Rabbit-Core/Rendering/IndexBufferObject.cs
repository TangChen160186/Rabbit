using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;

namespace Rabbit_Core.Rendering
{
    public class IndexBufferObject:IDisposable
    {
        private readonly int _id;
        public  int Length { get; }
        public IndexBufferObject(uint[] indices)
        {
            Length = indices.Length;
            _id = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer,_id);
            GL.BufferData(BufferTarget.ElementArrayBuffer,sizeof(uint) * indices.Length,indices,BufferUsageHint.StaticDraw);
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
