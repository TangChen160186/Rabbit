using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;

namespace Rabbit_Core.Rendering.Buffer
{
    public struct VertexBufferLayout
    {
        public int Count;
        public bool IsNormalized;
        public int Stride;
    }
    public class VertexBufferObject: IDisposable
    {

        private readonly int _id;
        public List<VertexBufferLayout> VertexBufferLayouts = new List<VertexBufferLayout>();


        public VertexBufferObject(float[] vertices)
        {
            _id = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer,_id);
            GL.BufferData(BufferTarget.ArrayBuffer,vertices.Length * sizeof(float),vertices,BufferUsageHint.StaticDraw);
        }

        public void AddBufferElement(VertexBufferLayout layout)
        {
            VertexBufferLayouts.Add(layout);
        }

public void Bind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, _id);
        }

        public void UnBind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
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

        ~VertexBufferObject()
        {
            ReleaseUnmanagedResources();
        }
    }
}
