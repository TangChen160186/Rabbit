using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using Rabbit_Core.Rendering;

namespace Rabbit_Sandbox
{
    internal class Window : GameWindow
    {
        float[] _vertices = {
            0.5f, 0.5f, 0.0f,1,0,0,   // 右上角
            0.5f, -0.5f, 0.0f,0,1,0,  // 右下角
            -0.5f, -0.5f, 0.0f,0,0,1, // 左下角
            -0.5f, 0.5f, 0.0f,1,0,1 // 左上角
        };
        uint[] _indices = {
            // 注意索引从0开始! 
            // 此例的索引(0,1,2,3)就是顶点数组vertices的下标，
            // 这样可以由下标代表顶点组合成矩形

            0, 1, 3, // 第一个三角形
            1, 2, 3  // 第二个三角形
        };

        private VertexArrayObject _vao;
        private VertexBufferObject _vbo;
        private IndexBufferObject _ibo;
        private int _program;
        public Window(int width, int height, string title) : base(GameWindowSettings.Default,
            new NativeWindowSettings() { Size = (width, height), Title = title })
        {

        }

        protected override void OnLoad()
        {
            //GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
            

            _vbo = new VertexBufferObject(_vertices);
            VertexBufferLayout layout = new VertexBufferLayout();
            layout.AddElement(new VertexBufferLayoutElement(0,3),new VertexBufferLayoutElement(1,3));
            _vbo.AddLayout(layout);
            _ibo = new IndexBufferObject(_indices);
            _vao = new VertexArrayObject(_ibo, _vbo);




            string vertexSource = @"#version 460 core
                layout (location = 0) in vec3 aPos; 
                layout (location = 1) in vec3 aColor;
                
                layout (location = 0) out vec3 color;
                void main()
                {
                    gl_Position = vec4(aPos.x, aPos.y, aPos.z, 1.0);
                    color = aColor;
                }";

            int vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, vertexSource);
            GL.CompileShader(vertexShader);

            string fragmentSource = @"
                #version 460 core
                out vec4 FragColor;
                layout (location = 0) in vec3 color;
                void main()
                {
                    FragColor = vec4(color, 1.0f);
                }";
            int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, fragmentSource);
            GL.CompileShader(fragmentShader);

            _program = GL.CreateProgram();
            GL.AttachShader(_program, vertexShader);
            GL.AttachShader(_program, fragmentShader);
            GL.LinkProgram(_program);
            //GL.UseProgram(_program);

        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.ClearColor(new Color4(0.2f, 0.3f, 0.3f, 1.0f));

            _vao.Bind();
            GL.UseProgram(_program);
            if (_vao.IndexBufferObject == null)
            {
                //GL.DrawArrays(PrimitiveType.Triangles,0,3);
            }
            else
            {
                GL.DrawElements(PrimitiveType.Triangles, _ibo.Length, DrawElementsType.UnsignedInt, 0);
            }
            

            SwapBuffers();
        }


        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            KeyboardState input = KeyboardState;

            if (input.IsKeyDown(Keys.Escape))
            {
                Close();
            }
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            GL.Viewport(0, 0, e.Width, e.Height);
        }
    }
}
