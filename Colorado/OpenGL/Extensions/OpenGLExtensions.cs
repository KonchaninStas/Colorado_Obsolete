﻿using Colorado.OpenGL.OpenGLLibrariesAPI;
using Colorado.OpenGL.OpenGLWrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static Colorado.OpenGL.Extensions.Delegates;

namespace Colorado.OpenGL.Extensions
{
    internal class OpenGLExtensions
    {
        public static glActiveTexture ActiveTexture;
        public static glClientActiveTexture ClientActiveTexture;
        public static glMultiTexCoord1d MultiTexCoord1d;
        public static glMultiTexCoord1dv MultiTexCoord1dv;
        public static glMultiTexCoord1f MultiTexCoord1f;
        public static glMultiTexCoord1fv MultiTexCoord1fv;
        public static glMultiTexCoord1i MultiTexCoord1i;
        public static glMultiTexCoord1iv MultiTexCoord1iv;
        public static glMultiTexCoord1s MultiTexCoord1s;
        public static glMultiTexCoord1sv MultiTexCoord1sv;
        public static glMultiTexCoord2d MultiTexCoord2d;
        public static glMultiTexCoord2dv MultiTexCoord2dv;
        public static glMultiTexCoord2f MultiTexCoord2f;
        public static glMultiTexCoord2fv MultiTexCoord2fv;
        public static glMultiTexCoord2i MultiTexCoord2i;
        public static glMultiTexCoord2iv MultiTexCoord2iv;
        public static glMultiTexCoord2s MultiTexCoord2s;
        public static glMultiTexCoord2sv MultiTexCoord2sv;
        public static glMultiTexCoord3d MultiTexCoord3d;
        public static glMultiTexCoord3dv MultiTexCoord3dv;
        public static glMultiTexCoord3f MultiTexCoord3f;
        public static glMultiTexCoord3fv MultiTexCoord3fv;
        public static glMultiTexCoord3i MultiTexCoord3i;
        public static glMultiTexCoord3iv MultiTexCoord3iv;
        public static glMultiTexCoord3s MultiTexCoord3s;
        public static glMultiTexCoord3sv MultiTexCoord3sv;
        public static glMultiTexCoord4d MultiTexCoord4d;
        public static glMultiTexCoord4dv MultiTexCoord4dv;
        public static glMultiTexCoord4f MultiTexCoord4f;
        public static glMultiTexCoord4fv MultiTexCoord4fv;
        public static glMultiTexCoord4i MultiTexCoord4i;
        public static glMultiTexCoord4iv MultiTexCoord4iv;
        public static glMultiTexCoord4s MultiTexCoord4s;
        public static glMultiTexCoord4sv MultiTexCoord4sv;

        public static glBindBufferARB BindBufferARB;
        public static glDeleteBuffersARB DeleteBuffersARB_;
        public static glGenBuffersARB GenBuffersARB_;
        public static glIsBufferARB IsBufferARB;
        private static glBufferData BufferData_;
        private static glBufferSubData BufferSubData_;
        public static glGetBufferSubDataARB GetBufferSubDataARB;
        public static glMapBufferARB MapBufferARB;
        public static glUnmapBufferARB UnmapBufferARB;
        public static glGetBufferParameterivARB GetBufferParameterivARB;
        public static glGetBufferPointervARB GetBufferPointervARB;

        public static glGenQueriesARB GenQueriesARB_;
        public static glDeleteQueriesARB DeleteQueriesARB_;
        public static glIsQueryARB IsQueryARB;
        public static glBeginQueryARB BeginQueryARB;
        public static glEndQueryARB EndQueryARB;
        public static glGetQueryivARB GetQueryivARB;
        public static glGetQueryObjectivARB GetQueryObjectivARB;
        public static glGetQueryObjectuivARB GetQueryObjectuivARB;

        public static glVertexAttribPointerARB VertexAttribPointerARB;
        public static glEnableVertexAttribArrayARB EnableVertexAttribArrayARB;
        public static glDisableVertexAttribArrayARB DisableVertexAttribArrayARB;
        private static glProgramStringARB ProgramStringARB_;
        public static glBindProgramARB BindProgramARB;
        public static glDeleteProgramsARB DeleteProgramsARB_;
        public static glGenProgramsARB GenProgramsARB_;
        public static glProgramLocalParameter4fARB ProgramLocalParameter4fARB;
        public static glProgramEnvParameter4fARB ProgramEnvParameter4fARB;
        public static glGetProgramivARB _GetProgramivARB;
        public static glGetProgramEnvParameterfvARB GetProgramEnvParameterfvARB;
        public static glGetProgramLocalParameterfvARB GetProgramLocalParameterfvARB;
        public static glVertexAttrib2fARB VertexAttrib2fARB;
        public static glVertexAttrib3fARB VertexAttrib3fARB;
        public static glVertexAttrib4fARB VertexAttrib4fARB;

        public static glStencilOpSeparateATI StencilOpSeparateATI;
        public static glStencilFuncSeparateATI StencilFuncSeparateATI;

        public static glGetCompressedTexImageARB GetCompressedTexImageARB;
        public static glCompressedTexImage2DARB CompressedTexImage2DARB;

        public static glTexImage3D _TexImage3D;
        public static glBlendEquation BlendEquation;
        public static glWindowPos2f WindowPos2f;

        public static glCreateShaderObjectARB CreateShaderObjectARB;
        private static glShaderSourceARB ShaderSourceARB_;
        public static glCompileShaderARB CompileShaderARB;
        public static glDeleteObjectARB DeleteObjectARB;
        public static glGetHandleARB GetHandleARB;
        public static glDetachObjectARB DetachObjectARB;
        public static glCreateProgramObjectARB CreateProgramObjectARB;
        public static glAttachObjectARB AttachObjectARB;
        public static glLinkProgramARB LinkProgramARB;
        public static glUseProgramObjectARB UseProgramObjectARB;
        public static glValidateProgramARB ValidateProgramARB;
        public static glGetObjectParameterfvARB GetObjectParameterfvARB;
        public static glGetObjectParameterivARB GetObjectParameterivARB;
        public static glGetActiveAttribARB GetActiveAttribARB;
        public static glGetActiveUniformARB GetActiveUniformARB;
        public static glGetAttachedObjectsARB GetAttachedObjectsARB;
        public static glGetAttribLocationARB GetAttribLocationARB;
        public static glGetShaderSourceARB GetShaderSourceARB;
        public static glGetUniformfvARB GetUniformfvARB;
        public static glGetUniformivARB GetUniformivARB;
        public static glGetUniformLocationARB GetUniformLocationARB;
        private static glGetInfoLogARB GetInfoLogARB_;
        public static glBindAttribLocationARB BindAttribLocationARB;
        public static glUniform1fARB Uniform1fARB;
        public static glUniform2fARB Uniform2fARB;
        public static glUniform3fARB Uniform3fARB;
        public static glUniform4fARB Uniform4fARB;
        public static glUniform1iARB Uniform1iARB;
        public static glUniform2iARB Uniform2iARB;
        public static glUniform3iARB Uniform3iARB;
        public static glUniform4iARB Uniform4iARB;
        public static glUniform1fvARB Uniform1fvARB;
        public static glUniform2fvARB Uniform2fvARB;
        public static glUniform3fvARB Uniform3fvARB;
        public static glUniform4fvARB Uniform4fvARB;
        public static glUniform1ivARB Uniform1ivARB;
        public static glUniform2ivARB Uniform2ivARB;
        public static glUniform3ivARB Uniform3ivARB;
        public static glUniform4ivARB Uniform4ivARB;
        public static glUniformMatrix2fvARB UniformMatrix2fvARB;
        public static glUniformMatrix3fvARB UniformMatrix3fvARB;
        public static glUniformMatrix4fvARB UniformMatrix4fvARB;

        public static glIsRenderbufferEXT IsRenderbufferEXT;
        public static glBindRenderbufferEXT BindRenderbufferEXT;
        public static glDeleteRenderbuffersEXT DeleteRenderbuffersEXT;
        private static glGenRenderbuffersEXT _GenRenderbuffersEXT;
        public static glRenderbufferStorageEXT RenderbufferStorageEXT;
        public static glGetRenderbufferParameterivEXT GetRenderbufferParameterivEXT;
        public static glIsFramebufferEXT IsFramebufferEXT;
        public static glBindFramebufferEXT BindFramebufferEXT;
        public static glDeleteFramebuffersEXT DeleteFramebuffersEXT;
        private static glGenFramebuffersEXT _GenFramebuffersEXT;
        public static glCheckFramebufferStatusEXT CheckFramebufferStatusEXT;
        public static glFramebufferTexture1DEXT FramebufferTexture1DEXT;
        public static glFramebufferTexture2DEXT FramebufferTexture2DEXT;
        public static glFramebufferTexture3DEXT FramebufferTexture3DEXT;
        public static glFramebufferRenderbufferEXT FramebufferRenderbufferEXT;
        public static glGetFramebufferAttachmentParameterivEXT GetFramebufferAttachmentParameterivEXT;
        public static glGenerateMipmapEXT GenerateMipmapEXT;

        public static void LoadExtensions()
        {
            ActiveTexture = (glActiveTexture)GetAddress("glActiveTexture", typeof(glActiveTexture));
            ClientActiveTexture = (glClientActiveTexture)GetAddress("glClientActiveTexture", typeof(glClientActiveTexture));
            MultiTexCoord1d = (glMultiTexCoord1d)GetAddress("glMultiTexCoord1d", typeof(glMultiTexCoord1d));
            MultiTexCoord1dv = (glMultiTexCoord1dv)GetAddress("glMultiTexCoord1dv", typeof(glMultiTexCoord1dv));
            MultiTexCoord1f = (glMultiTexCoord1f)GetAddress("glMultiTexCoord1f", typeof(glMultiTexCoord1f));
            MultiTexCoord1fv = (glMultiTexCoord1fv)GetAddress("glMultiTexCoord1fv", typeof(glMultiTexCoord1fv));
            MultiTexCoord1i = (glMultiTexCoord1i)GetAddress("glMultiTexCoord1i", typeof(glMultiTexCoord1i));
            MultiTexCoord1iv = (glMultiTexCoord1iv)GetAddress("glMultiTexCoord1iv", typeof(glMultiTexCoord1iv));
            MultiTexCoord1s = (glMultiTexCoord1s)GetAddress("glMultiTexCoord1s", typeof(glMultiTexCoord1s));
            MultiTexCoord1sv = (glMultiTexCoord1sv)GetAddress("glMultiTexCoord1sv", typeof(glMultiTexCoord1sv));
            MultiTexCoord2d = (glMultiTexCoord2d)GetAddress("glMultiTexCoord2d", typeof(glMultiTexCoord2d));
            MultiTexCoord2dv = (glMultiTexCoord2dv)GetAddress("glMultiTexCoord2dv", typeof(glMultiTexCoord2dv));
            MultiTexCoord2f = (glMultiTexCoord2f)GetAddress("glMultiTexCoord2f", typeof(glMultiTexCoord2f));
            MultiTexCoord2fv = (glMultiTexCoord2fv)GetAddress("glMultiTexCoord2fv", typeof(glMultiTexCoord2fv));
            MultiTexCoord2i = (glMultiTexCoord2i)GetAddress("glMultiTexCoord2i", typeof(glMultiTexCoord2i));
            MultiTexCoord2iv = (glMultiTexCoord2iv)GetAddress("glMultiTexCoord2iv", typeof(glMultiTexCoord2iv));
            MultiTexCoord2s = (glMultiTexCoord2s)GetAddress("glMultiTexCoord2s", typeof(glMultiTexCoord2s));
            MultiTexCoord2sv = (glMultiTexCoord2sv)GetAddress("glMultiTexCoord2sv", typeof(glMultiTexCoord2sv));
            MultiTexCoord3d = (glMultiTexCoord3d)GetAddress("glMultiTexCoord3d", typeof(glMultiTexCoord3d));
            MultiTexCoord3dv = (glMultiTexCoord3dv)GetAddress("glMultiTexCoord3dv", typeof(glMultiTexCoord3dv));
            MultiTexCoord3f = (glMultiTexCoord3f)GetAddress("glMultiTexCoord3f", typeof(glMultiTexCoord3f));
            MultiTexCoord3fv = (glMultiTexCoord3fv)GetAddress("glMultiTexCoord3fv", typeof(glMultiTexCoord3fv));
            MultiTexCoord3i = (glMultiTexCoord3i)GetAddress("glMultiTexCoord3i", typeof(glMultiTexCoord3i));
            MultiTexCoord3iv = (glMultiTexCoord3iv)GetAddress("glMultiTexCoord3iv", typeof(glMultiTexCoord3iv));
            MultiTexCoord3s = (glMultiTexCoord3s)GetAddress("glMultiTexCoord3s", typeof(glMultiTexCoord3s));
            MultiTexCoord3sv = (glMultiTexCoord3sv)GetAddress("glMultiTexCoord3sv", typeof(glMultiTexCoord3sv));
            MultiTexCoord4d = (glMultiTexCoord4d)GetAddress("glMultiTexCoord4d", typeof(glMultiTexCoord4d));
            MultiTexCoord4dv = (glMultiTexCoord4dv)GetAddress("glMultiTexCoord4dv", typeof(glMultiTexCoord4dv));
            MultiTexCoord4f = (glMultiTexCoord4f)GetAddress("glMultiTexCoord4f", typeof(glMultiTexCoord4f));
            MultiTexCoord4fv = (glMultiTexCoord4fv)GetAddress("glMultiTexCoord4fv", typeof(glMultiTexCoord4fv));
            MultiTexCoord4i = (glMultiTexCoord4i)GetAddress("glMultiTexCoord4i", typeof(glMultiTexCoord4i));
            MultiTexCoord4iv = (glMultiTexCoord4iv)GetAddress("glMultiTexCoord4iv", typeof(glMultiTexCoord4iv));
            MultiTexCoord4s = (glMultiTexCoord4s)GetAddress("glMultiTexCoord4s", typeof(glMultiTexCoord4s));
            MultiTexCoord4sv = (glMultiTexCoord4sv)GetAddress("glMultiTexCoord4sv", typeof(glMultiTexCoord4sv));
            BindBufferARB = (glBindBufferARB)GetAddress("glBindBufferARB", typeof(glBindBufferARB));
            DeleteBuffersARB_ = (glDeleteBuffersARB)GetAddress("glDeleteBuffersARB", typeof(glDeleteBuffersARB));
            GenBuffersARB_ = (glGenBuffersARB)GetAddress("glGenBuffersARB", typeof(glGenBuffersARB));
            IsBufferARB = (glIsBufferARB)GetAddress("glIsBufferARB", typeof(glIsBufferARB));
            BufferData_ = (glBufferData)GetAddress("glBufferData", typeof(glBufferData));
            BufferSubData_ = (glBufferSubData)GetAddress("glBufferSubData", typeof(glBufferSubData));
            GetBufferSubDataARB = (glGetBufferSubDataARB)GetAddress("glGetBufferSubDataARB", typeof(glGetBufferSubDataARB));
            MapBufferARB = (glMapBufferARB)GetAddress("glMapBufferARB", typeof(glMapBufferARB));
            UnmapBufferARB = (glUnmapBufferARB)GetAddress("glUnmapBufferARB", typeof(glUnmapBufferARB));
            GetBufferParameterivARB = (glGetBufferParameterivARB)GetAddress("glGetBufferParameterivARB", typeof(glGetBufferParameterivARB));
            GetBufferPointervARB = (glGetBufferPointervARB)GetAddress("glGetBufferPointervARB", typeof(glGetBufferPointervARB));
            GenQueriesARB_ = (glGenQueriesARB)GetAddress("glGenQueriesARB", typeof(glGenQueriesARB));
            DeleteQueriesARB_ = (glDeleteQueriesARB)GetAddress("glDeleteQueriesARB", typeof(glDeleteQueriesARB));
            IsQueryARB = (glIsQueryARB)GetAddress("glIsQueryARB", typeof(glIsQueryARB));
            BeginQueryARB = (glBeginQueryARB)GetAddress("glBeginQueryARB", typeof(glBeginQueryARB));
            EndQueryARB = (glEndQueryARB)GetAddress("glEndQueryARB", typeof(glEndQueryARB));
            GetQueryivARB = (glGetQueryivARB)GetAddress("glGetQueryivARB", typeof(glGetQueryivARB));
            GetQueryObjectivARB = (glGetQueryObjectivARB)GetAddress("glGetQueryObjectivARB", typeof(glGetQueryObjectivARB));
            GetQueryObjectuivARB = (glGetQueryObjectuivARB)GetAddress("glGetQueryObjectuivARB", typeof(glGetQueryObjectuivARB));
            VertexAttribPointerARB = (glVertexAttribPointerARB)GetAddress("glVertexAttribPointerARB", typeof(glVertexAttribPointerARB));
            EnableVertexAttribArrayARB = (glEnableVertexAttribArrayARB)GetAddress("glEnableVertexAttribArrayARB", typeof(glEnableVertexAttribArrayARB));
            DisableVertexAttribArrayARB = (glDisableVertexAttribArrayARB)GetAddress("glDisableVertexAttribArrayARB", typeof(glDisableVertexAttribArrayARB));
            ProgramStringARB_ = (glProgramStringARB)GetAddress("glProgramStringARB", typeof(glProgramStringARB));
            BindProgramARB = (glBindProgramARB)GetAddress("glBindProgramARB", typeof(glBindProgramARB));
            DeleteProgramsARB_ = (glDeleteProgramsARB)GetAddress("glDeleteProgramsARB", typeof(glDeleteProgramsARB));
            GenProgramsARB_ = (glGenProgramsARB)GetAddress("glGenProgramsARB", typeof(glGenProgramsARB));
            ProgramLocalParameter4fARB = (glProgramLocalParameter4fARB)GetAddress("glProgramLocalParameter4fARB", typeof(glProgramLocalParameter4fARB));
            ProgramEnvParameter4fARB = (glProgramEnvParameter4fARB)GetAddress("glProgramEnvParameter4fARB", typeof(glProgramEnvParameter4fARB));
            _GetProgramivARB = (glGetProgramivARB)GetAddress("glGetProgramivARB", typeof(glGetProgramivARB));
            GetProgramEnvParameterfvARB = (glGetProgramEnvParameterfvARB)GetAddress("glGetProgramEnvParameterfvARB", typeof(glGetProgramEnvParameterfvARB));
            GetProgramLocalParameterfvARB = (glGetProgramLocalParameterfvARB)GetAddress("glGetProgramLocalParameterfvARB", typeof(glGetProgramLocalParameterfvARB));
            VertexAttrib2fARB = (glVertexAttrib2fARB)GetAddress("glVertexAttrib2fARB", typeof(glVertexAttrib2fARB));
            VertexAttrib3fARB = (glVertexAttrib3fARB)GetAddress("glVertexAttrib3fARB", typeof(glVertexAttrib3fARB));
            VertexAttrib4fARB = (glVertexAttrib4fARB)GetAddress("glVertexAttrib4fARB", typeof(glVertexAttrib4fARB));

            GetCompressedTexImageARB = (glGetCompressedTexImageARB)GetAddress("glGetCompressedTexImageARB", typeof(glGetCompressedTexImageARB));
            CompressedTexImage2DARB = (glCompressedTexImage2DARB)GetAddress("glCompressedTexImage2DARB", typeof(glCompressedTexImage2DARB));
            _TexImage3D = (glTexImage3D)GetAddress("glTexImage3D", typeof(glTexImage3D));
            BlendEquation = (glBlendEquation)GetAddress("glBlendEquation", typeof(glBlendEquation));
            WindowPos2f = (glWindowPos2f)GetAddress("glWindowPos2f", typeof(glWindowPos2f));
            CreateShaderObjectARB = (glCreateShaderObjectARB)GetAddress("glCreateShaderObjectARB", typeof(glCreateShaderObjectARB));
            ShaderSourceARB_ = (glShaderSourceARB)GetAddress("glShaderSourceARB", typeof(glShaderSourceARB));
            CompileShaderARB = (glCompileShaderARB)GetAddress("glCompileShaderARB", typeof(glCompileShaderARB));
            DeleteObjectARB = (glDeleteObjectARB)GetAddress("glDeleteObjectARB", typeof(glDeleteObjectARB));
            GetHandleARB = (glGetHandleARB)GetAddress("glGetHandleARB", typeof(glGetHandleARB));
            DetachObjectARB = (glDetachObjectARB)GetAddress("glDetachObjectARB", typeof(glDetachObjectARB));
            CreateProgramObjectARB = (glCreateProgramObjectARB)GetAddress("glCreateProgramObjectARB", typeof(glCreateProgramObjectARB));
            AttachObjectARB = (glAttachObjectARB)GetAddress("glAttachObjectARB", typeof(glAttachObjectARB));
            LinkProgramARB = (glLinkProgramARB)GetAddress("glLinkProgramARB", typeof(glLinkProgramARB));
            UseProgramObjectARB = (glUseProgramObjectARB)GetAddress("glUseProgramObjectARB", typeof(glUseProgramObjectARB));
            ValidateProgramARB = (glValidateProgramARB)GetAddress("glValidateProgramARB", typeof(glValidateProgramARB));
            GetObjectParameterfvARB = (glGetObjectParameterfvARB)GetAddress("glGetObjectParameterfvARB", typeof(glGetObjectParameterfvARB));
            GetObjectParameterivARB = (glGetObjectParameterivARB)GetAddress("glGetObjectParameterivARB", typeof(glGetObjectParameterivARB));
            GetActiveAttribARB = (glGetActiveAttribARB)GetAddress("glGetActiveAttribARB", typeof(glGetActiveAttribARB));
            GetActiveUniformARB = (glGetActiveUniformARB)GetAddress("glGetActiveUniformARB", typeof(glGetActiveUniformARB));
            GetAttachedObjectsARB = (glGetAttachedObjectsARB)GetAddress("glGetAttachedObjectsARB", typeof(glGetAttachedObjectsARB));
            GetAttribLocationARB = (glGetAttribLocationARB)GetAddress("glGetAttribLocationARB", typeof(glGetAttribLocationARB));
            GetShaderSourceARB = (glGetShaderSourceARB)GetAddress("glGetShaderSourceARB", typeof(glGetShaderSourceARB));
            GetUniformfvARB = (glGetUniformfvARB)GetAddress("glGetUniformfvARB", typeof(glGetUniformfvARB));
            GetUniformivARB = (glGetUniformivARB)GetAddress("glGetUniformivARB", typeof(glGetUniformivARB));
            GetUniformLocationARB = (glGetUniformLocationARB)GetAddress("glGetUniformLocationARB", typeof(glGetUniformLocationARB));
            GetInfoLogARB_ = (glGetInfoLogARB)GetAddress("glGetInfoLogARB", typeof(glGetInfoLogARB));
            BindAttribLocationARB = (glBindAttribLocationARB)GetAddress("glBindAttribLocationARB", typeof(glBindAttribLocationARB));
            Uniform1fARB = (glUniform1fARB)GetAddress("glUniform1fARB", typeof(glUniform1fARB));
            Uniform2fARB = (glUniform2fARB)GetAddress("glUniform2fARB", typeof(glUniform2fARB));
            Uniform3fARB = (glUniform3fARB)GetAddress("glUniform3fARB", typeof(glUniform3fARB));
            Uniform4fARB = (glUniform4fARB)GetAddress("glUniform4fARB", typeof(glUniform4fARB));
            Uniform1iARB = (glUniform1iARB)GetAddress("glUniform1iARB", typeof(glUniform1iARB));
            Uniform2iARB = (glUniform2iARB)GetAddress("glUniform2iARB", typeof(glUniform2iARB));
            Uniform3iARB = (glUniform3iARB)GetAddress("glUniform3iARB", typeof(glUniform3iARB));
            Uniform4iARB = (glUniform4iARB)GetAddress("glUniform4iARB", typeof(glUniform4iARB));
            Uniform1fvARB = (glUniform1fvARB)GetAddress("glUniform1fvARB", typeof(glUniform1fvARB));
            Uniform2fvARB = (glUniform2fvARB)GetAddress("glUniform2fvARB", typeof(glUniform2fvARB));
            Uniform3fvARB = (glUniform3fvARB)GetAddress("glUniform3fvARB", typeof(glUniform3fvARB));
            Uniform4fvARB = (glUniform4fvARB)GetAddress("glUniform4fvARB", typeof(glUniform4fvARB));
            Uniform1ivARB = (glUniform1ivARB)GetAddress("glUniform1ivARB", typeof(glUniform1ivARB));
            Uniform2ivARB = (glUniform2ivARB)GetAddress("glUniform2ivARB", typeof(glUniform2ivARB));
            Uniform3ivARB = (glUniform3ivARB)GetAddress("glUniform3ivARB", typeof(glUniform3ivARB));
            Uniform4ivARB = (glUniform4ivARB)GetAddress("glUniform4ivARB", typeof(glUniform4ivARB));
            UniformMatrix2fvARB = (glUniformMatrix2fvARB)GetAddress("glUniformMatrix2fvARB", typeof(glUniformMatrix2fvARB));
            UniformMatrix3fvARB = (glUniformMatrix3fvARB)GetAddress("glUniformMatrix3fvARB", typeof(glUniformMatrix3fvARB));
            UniformMatrix4fvARB = (glUniformMatrix4fvARB)GetAddress("glUniformMatrix4fvARB", typeof(glUniformMatrix4fvARB));

            IsRenderbufferEXT = (glIsRenderbufferEXT)GetAddress("glIsRenderbufferEXT", typeof(glIsRenderbufferEXT));
            BindRenderbufferEXT = (glBindRenderbufferEXT)GetAddress("glBindRenderbufferEXT", typeof(glBindRenderbufferEXT));
            DeleteRenderbuffersEXT = (glDeleteRenderbuffersEXT)GetAddress("glDeleteRenderbuffersEXT", typeof(glDeleteRenderbuffersEXT));
            _GenRenderbuffersEXT = (glGenRenderbuffersEXT)GetAddress("glGenRenderbuffersEXT", typeof(glGenRenderbuffersEXT));
            RenderbufferStorageEXT = (glRenderbufferStorageEXT)GetAddress("glRenderbufferStorageEXT", typeof(glRenderbufferStorageEXT));
            GetRenderbufferParameterivEXT = (glGetRenderbufferParameterivEXT)GetAddress("glGetRenderbufferParameterivEXT", typeof(glGetRenderbufferParameterivEXT));
            IsFramebufferEXT = (glIsFramebufferEXT)GetAddress("glIsFramebufferEXT", typeof(glIsFramebufferEXT));
            BindFramebufferEXT = (glBindFramebufferEXT)GetAddress("glBindFramebufferEXT", typeof(glBindFramebufferEXT));
            DeleteFramebuffersEXT = (glDeleteFramebuffersEXT)GetAddress("glDeleteFramebuffersEXT", typeof(glDeleteFramebuffersEXT));
            _GenFramebuffersEXT = (glGenFramebuffersEXT)GetAddress("glGenFramebuffersEXT", typeof(glGenFramebuffersEXT));
            CheckFramebufferStatusEXT = (glCheckFramebufferStatusEXT)GetAddress("glCheckFramebufferStatusEXT", typeof(glCheckFramebufferStatusEXT));
            FramebufferTexture1DEXT = (glFramebufferTexture1DEXT)GetAddress("glFramebufferTexture1DEXT", typeof(glFramebufferTexture1DEXT));
            FramebufferTexture2DEXT = (glFramebufferTexture2DEXT)GetAddress("glFramebufferTexture2DEXT", typeof(glFramebufferTexture2DEXT));
            FramebufferTexture3DEXT = (glFramebufferTexture3DEXT)GetAddress("glFramebufferTexture3DEXT", typeof(glFramebufferTexture3DEXT));
            FramebufferRenderbufferEXT = (glFramebufferRenderbufferEXT)GetAddress("glFramebufferRenderbufferEXT", typeof(glFramebufferRenderbufferEXT));
            GetFramebufferAttachmentParameterivEXT = (glGetFramebufferAttachmentParameterivEXT)GetAddress("glGetFramebufferAttachmentParameterivEXT", typeof(glGetFramebufferAttachmentParameterivEXT));
            GenerateMipmapEXT = (glGenerateMipmapEXT)GetAddress("glGenerateMipmapEXT", typeof(glGenerateMipmapEXT));
        }

        private static Delegate GetAddress(string name, Type t)
        {
            try
            {
                int addr = OpenGlWglWrapper.GetExtensionFunctionAddress(name);
                return addr == 0 ? null : Marshal.GetDelegateForFunctionPointer(new IntPtr(addr), t);
            }
            catch
            {
                return null;
            }
        }
    }
}
