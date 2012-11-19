﻿using System.Collections.Generic;
using dot10.PE;

namespace dot10.DotNet.Emit {
	/// <summary>
	/// Method body base class
	/// </summary>
	public abstract class MethodBody {
	}

	/// <summary>
	/// A native method body
	/// </summary>
	public sealed class NativeMethodBody : MethodBody {
		RVA rva;

		/// <summary>
		/// Gets/sets the RVA of the native method body
		/// </summary>
		public RVA RVA {
			get { return rva; }
			set { rva = value; }
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		public NativeMethodBody() {
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="rva">RVA of method body</param>
		public NativeMethodBody(RVA rva) {
			this.rva = rva;
		}
	}

	/// <summary>
	/// CIL (managed code) body
	/// </summary>
	public sealed class CilBody : MethodBody {
		bool initLocals;
		ushort maxStack;
		uint localVarSigTok;
		IList<Instruction> instructions;
		IList<ExceptionHandler> exceptionHandlers;
		LocalList localList;

		/// <summary>
		/// Gets/sets the init locals flag. This is only valid if the method has any locals.
		/// </summary>
		public bool InitLocals {
			get { return initLocals; }
			set { initLocals = value; }
		}

		/// <summary>
		/// Gets/sets max stack value from the fat method header.
		/// </summary>
		public ushort MaxStack {
			get { return maxStack; }
			set { maxStack = value; }
		}

		/// <summary>
		/// Gets/sets the locals metadata token
		/// </summary>
		public uint LocalVarSigTok {
			get { return localVarSigTok; }
			set { localVarSigTok = value; }
		}

		/// <summary>
		/// <c>true</c> if <see cref="Instructions"/> is not empty
		/// </summary>
		public bool HasInstructions {
			get { return instructions.Count > 0; }
		}

		/// <summary>
		/// Gets the instructions
		/// </summary>
		public IList<Instruction> Instructions {
			get { return instructions; }
			set { instructions = value; }
		}

		/// <summary>
		/// <c>true</c> if <see cref="ExceptionHandlers"/> is not empty
		/// </summary>
		public bool HasExceptionHandlers {
			get { return exceptionHandlers.Count > 0; }
		}

		/// <summary>
		/// Gets the exception handlers
		/// </summary>
		public IList<ExceptionHandler> ExceptionHandlers {
			get { return exceptionHandlers; }
		}

		/// <summary>
		/// <c>true</c> if <see cref="Variables"/> is not empty
		/// </summary>
		public bool HasVariables {
			get { return localList.Count > 0; }
		}

		/// <summary>
		/// Gets the locals
		/// </summary>
		public LocalList Variables {// Only called Variables for compat w/ older code. Locals is a better and more accurate name
			get { return localList; }
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		public CilBody() {
			this.initLocals = true;
			this.instructions = new List<Instruction>();
			this.exceptionHandlers = new List<ExceptionHandler>();
			this.localList = new LocalList();
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="initLocals">Init locals flag</param>
		/// <param name="instructions">All instructions. This instance will own the list.</param>
		/// <param name="exceptionHandlers">All exception handlers. This instance will own the list.</param>
		/// <param name="locals">All locals. This instance will own the locals in the list.</param>
		public CilBody(bool initLocals, IList<Instruction> instructions, IList<ExceptionHandler> exceptionHandlers, IList<Local> locals) {
			this.initLocals = initLocals;
			this.instructions = instructions;
			this.exceptionHandlers = exceptionHandlers;
			this.localList = new LocalList(locals);
		}
	}
}
