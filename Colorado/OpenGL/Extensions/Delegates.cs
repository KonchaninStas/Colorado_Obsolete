using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorado.OpenGL.Extensions
{
    class Delegates
    {
        public delegate void glActiveTexture(int texid);
        public delegate void glClientActiveTexture(int texid);
        public delegate void glMultiTexCoord1d(int texid, double d);
        public delegate void glMultiTexCoord1dv(int texid, double[] dparams);
        public delegate void glMultiTexCoord1f(int texid, float f);
        public delegate void glMultiTexCoord1fv(int texid, float[] fparams);
        public delegate void glMultiTexCoord1i(int texid, int i);
        public delegate void glMultiTexCoord1iv(int texid, int[] iparams);
        public delegate void glMultiTexCoord1s(int texid, short s);
        public delegate void glMultiTexCoord1sv(int texid, short[] sparams);
        public delegate void glMultiTexCoord2d(int texid, double d1, double d2);
        public delegate void glMultiTexCoord2dv(int texid, double[] dparams);
        public delegate void glMultiTexCoord2f(int texid, float f1, float f2);
        public delegate void glMultiTexCoord2fv(int texid, float[] fparams);
        public delegate void glMultiTexCoord2i(int texid, int i1, int i2);
        public delegate void glMultiTexCoord2iv(int texid, int[] iparams);
        public delegate void glMultiTexCoord2s(int texid, short s1, short s2);
        public delegate void glMultiTexCoord2sv(int texid, short[] sparams);
        public delegate void glMultiTexCoord3d(int texid, double d1, double d2, double d3);
        public delegate void glMultiTexCoord3dv(int texid, double[] dparams);
        public delegate void glMultiTexCoord3f(int texid, float f1, float f2, float f3);
        public delegate void glMultiTexCoord3fv(int texid, float[] fparams);
        public delegate void glMultiTexCoord3i(int texid, int i1, int i2, int i3);
        public delegate void glMultiTexCoord3iv(int texid, int[] iparams);
        public delegate void glMultiTexCoord3s(int texid, short s1, short s2, short s3);
        public delegate void glMultiTexCoord3sv(int texid, short[] sparams);
        public delegate void glMultiTexCoord4d(int texid, double d1, double d2, double d3, double d4);
        public delegate void glMultiTexCoord4dv(int texid, double[] dparams);
        public delegate void glMultiTexCoord4f(int texid, float f1, float f2, float f3, float f4);
        public delegate void glMultiTexCoord4fv(int texid, float[] fparams);
        public delegate void glMultiTexCoord4i(int texid, int i1, int i2, int i3, int i4);
        public delegate void glMultiTexCoord4iv(int texid, int[] iparams);
        public delegate void glMultiTexCoord4s(int texid, short s1, short s2, short s3, short s4);
        public delegate void glMultiTexCoord4sv(int texid, short[] sparams);

        public delegate void glBindBufferARB(int target, int buffer);
        public delegate void glDeleteBuffersARB(int n, int[] buffers);
        public delegate void glGenBuffersARB(int n, int[] buffers);
        public delegate bool glIsBufferARB(int buffer);
        public delegate void glBufferData(int target, int size, IntPtr data, int usage);
        public delegate void glBufferSubData(int target, int offset, int size, IntPtr data);
        public delegate void glGetBufferSubDataARB(int target, int offset, int size, IntPtr data);
        public delegate IntPtr glMapBufferARB(int target, int offset, int size, IntPtr data);
        public delegate bool glUnmapBufferARB(int target);
        public delegate void glGetBufferParameterivARB(int target, int pname, int[] iparams);
        public delegate void glGetBufferPointervARB(int target, int pname, IntPtr[] iparams);

        public delegate void glGenQueriesARB(int n, int[] queries);
        public delegate void glDeleteQueriesARB(int n, int[] queries);
        public delegate bool glIsQueryARB(int target, int id);
        public delegate void glBeginQueryARB(int target, int id);
        public delegate void glEndQueryARB(int target);
        public delegate void glGetQueryivARB(int target, int pname, int[] iparams);
        public delegate void glGetQueryObjectivARB(int target, int pname, int[] iparams);
        public delegate void glGetQueryObjectuivARB(int target, int pname, uint[] iparams);

        public delegate void glVertexAttribPointerARB(int index, int size, int type, bool normalized, int stride, IntPtr pointer);
        public delegate void glEnableVertexAttribArrayARB(int index);
        public delegate void glDisableVertexAttribArrayARB(int index);
        public delegate void glProgramStringARB(int target, int format, int size, string source);
        public delegate void glBindProgramARB(int target, int program);
        public delegate void glDeleteProgramsARB(int size, int[] programs);
        public delegate void glGenProgramsARB(int size, int[] programs);
        public delegate void glProgramLocalParameter4fARB(int target, int index, float x, float y, float z, float w);
        public delegate void glProgramEnvParameter4fARB(int target, int index, float x, float y, float z, float w);
        public delegate void glGetProgramivARB(int target, int pname, int[] iparams);
        public delegate void glGetProgramEnvParameterfvARB(int target, int index, float[] fparams);
        public delegate void glGetProgramLocalParameterfvARB(int target, int index, float[] fparams);
        public delegate void glVertexAttrib2fARB(int index, float x, float y);
        public delegate void glVertexAttrib3fARB(int index, float x, float y, float z);
        public delegate void glVertexAttrib4fARB(int index, float x, float y, float z, float w);

        public delegate void glStencilOpSeparateATI(int face, int sfail, int dpfail, int dppass);
        public delegate void glStencilFuncSeparateATI(int frontfunc, int backfunc, int refval, uint mask);

        public delegate void glGetCompressedTexImageARB(int target, int lod, IntPtr img);
        public delegate void glCompressedTexImage2DARB(int target, int level, int internalformat, int width, int height, int border, int imagesize, IntPtr data);

        public delegate void glTexImage3D(int target, int level, int internalformat, int width, int height, int depth, int border, int format, int type, IntPtr pixels);

        public delegate void glBlendEquation(int mode);
        public delegate void glWindowPos2f(float x, float y);

        public delegate int glCreateShaderObjectARB(int shaderType);
        public delegate void glShaderSourceARB(int shaderobj, int nstrings, string[] source, int[] lengths);
        public delegate bool glCompileShaderARB(int shaderobj);
        public delegate void glDeleteObjectARB(int obj);
        public delegate int glGetHandleARB(int pname);
        public delegate void glDetachObjectARB(int container, int attached);
        public delegate int glCreateProgramObjectARB();
        public delegate void glAttachObjectARB(int programObj, int shaderObj);
        public delegate void glLinkProgramARB(int programObj);
        public delegate void glUseProgramObjectARB(int programObj);
        public delegate void glValidateProgramARB(int programObj);
        public delegate void glGetObjectParameterfvARB(int obj, int pname, float[] fparams);
        public delegate void glGetObjectParameterivARB(int obj, int pname, int[] iparams);
        public delegate void glGetActiveAttribARB(int programObj, int index, int maxLength, ref int length, ref int size, ref int type, string name);
        public delegate void glGetActiveUniformARB(int programObj, int index, int maxLength, ref int length, ref int size, ref int type, string name);
        public delegate void glGetAttachedObjectsARB(int programObj, int maxCount, ref int count, int[] objects);
        public delegate int glGetAttribLocationARB(int programObj, string name);
        public delegate void glGetShaderSourceARB(int shader, int maxLength, ref int length, ref string source);
        public delegate void glGetUniformfvARB(int programObj, int location, float[] fparams);
        public delegate void glGetUniformivARB(int programObj, int location, float[] iparams);
        public delegate int glGetUniformLocationARB(int programObj, string name);
        public delegate void glGetInfoLogARB(int shader, int maxLength, ref int length, StringBuilder infoLog);
        public delegate void glBindAttribLocationARB(int programObj, int index, string name);
        public delegate void glUniform1fARB(int location, float x);
        public delegate void glUniform2fARB(int location, float x, float y);
        public delegate void glUniform3fARB(int location, float x, float y, float z);
        public delegate void glUniform4fARB(int location, float x, float y, float z, float w);
        public delegate void glUniform1iARB(int location, int x);
        public delegate void glUniform2iARB(int location, int x, int y);
        public delegate void glUniform3iARB(int location, int x, int y, int z);
        public delegate void glUniform4iARB(int location, int x, int y, int z, int w);
        public delegate void glUniform1fvARB(int location, float[] value);
        public delegate void glUniform2fvARB(int location, float[] value);
        public delegate void glUniform3fvARB(int location, float[] value);
        public delegate void glUniform4fvARB(int location, float[] value);
        public delegate void glUniform1ivARB(int location, int[] value);
        public delegate void glUniform2ivARB(int location, int[] value);
        public delegate void glUniform3ivARB(int location, int[] value);
        public delegate void glUniform4ivARB(int location, int[] value);
        public delegate void glUniformMatrix2fvARB(int location, int count, bool transpose, float[] matrix);
        public delegate void glUniformMatrix3fvARB(int location, int count, bool transpose, float[] matrix);
        public delegate void glUniformMatrix4fvARB(int location, int count, bool transpose, float[] matrix);

        public delegate bool glIsRenderbufferEXT(int renderbuffer);
        public delegate void glBindRenderbufferEXT(int target, int renderbuffer);
        public delegate void glDeleteRenderbuffersEXT(int n, int[] renderbuffers);
        public delegate void glGenRenderbuffersEXT(int n, int[] renderbuffers);
        public delegate void glRenderbufferStorageEXT(int target, int internalformat, int width, int height);
        public delegate void glGetRenderbufferParameterivEXT(int target, int pname, int[] paramlist);
        public delegate bool glIsFramebufferEXT(int framebuffer);
        public delegate void glBindFramebufferEXT(int target, int framebuffer);
        public delegate void glDeleteFramebuffersEXT(int n, int[] framebuffers);
        public delegate void glGenFramebuffersEXT(int n, int[] framebuffers);
        public delegate int glCheckFramebufferStatusEXT(int target);
        public delegate void glFramebufferTexture1DEXT(int target, int attachment, int textarget, int texture, int level);
        public delegate void glFramebufferTexture2DEXT(int target, int attachment, int textarget, int texture, int level);
        public delegate void glFramebufferTexture3DEXT(int target, int attachment, int textarget, int texture, int level, int zoffset);
        public delegate void glFramebufferRenderbufferEXT(int target, int attachment, int renderbuffertarget, int renderbuffer);
        public delegate void glGetFramebufferAttachmentParameterivEXT(int target, int attachment, int pname, int[] paramlist);
        public delegate void glGenerateMipmapEXT(int target);
    }
}
